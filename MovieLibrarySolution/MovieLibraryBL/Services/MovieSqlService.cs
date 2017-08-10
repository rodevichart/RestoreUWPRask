using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.Data.Sqlite.Internal;
using MovieLibraryBL.Core;
using MovieLibraryBL.DTOs;
using MovieLibraryBL.Persistance;


namespace MovieLibraryBL.Services
{
	public class MovieSqlService : IMovieSqlService
	{
		//private readonly IMovieSqlService _cacheService;
		private readonly List<Guid> _movieGuids;

		public MovieSqlService()
		{
			//_cacheService = cacheService;
			_movieGuids = new List<Guid>();
			
		}

		public async Task InitDataBase()
		{
			SqliteEngine.UseWinSqlite3();

			using (var db = new SqliteConnection(MovieSqlConsts.DbFileName))
			{
				db.Open();
				var createMoviesTable = new SqliteCommand(MovieSqlConsts.MovieTableCommand, db);
				var createBuffTable = new SqliteCommand(MovieSqlConsts.BuffTableCommand, db);
				var createCacheTable = new SqliteCommand(MovieSqlConsts.CacheTableCommand, db);

				try
				{
					await createMoviesTable.ExecuteReaderAsync();
					await createBuffTable.ExecuteReaderAsync();
					await createCacheTable.ExecuteReaderAsync();
				}
				catch (SqliteException e)
				{					
				}
			}
		}

		private async Task AddBuffData(Guid idCache)
		{
			using (SqliteConnection db = new SqliteConnection(MovieSqlConsts.DbFileName))
			{

				db.Open();
				foreach (var id in _movieGuids)
				{
					SqliteCommand insertCommand = new SqliteCommand(MovieSqlConsts.InsertBuffCommandText, db);
					insertCommand.Parameters.AddWithValue("@CacheId", idCache.ToString());
					insertCommand.Parameters.AddWithValue("@MovieId", id.ToString());
					await insertCommand.ExecuteReaderAsync();
					insertCommand.Dispose();
				}
			}
		}


		private async Task AddCacheData(string url)
		{
			using (SqliteConnection db = new SqliteConnection(MovieSqlConsts.DbFileName))
			{
				db.Open();
				var cacheId = Guid.NewGuid();
				SqliteCommand insertCommand = new SqliteCommand(MovieSqlConsts.InsertCacheCommandText, db);
				insertCommand.Parameters.AddWithValue("@CacheId", cacheId.ToString());
				insertCommand.Parameters.AddWithValue("@Url", url);
				insertCommand.Parameters.AddWithValue("@CreationData", DateTime.Now);
				await insertCommand.ExecuteReaderAsync();
				insertCommand.Dispose();
				await AddBuffData(cacheId);
			}

		}

		public async Task AddMoviesData(List<FilmDto> movieDtos, string url)
		{
			using (SqliteConnection db = new SqliteConnection(MovieSqlConsts.DbFileName))
			{
				db.Open();
				_movieGuids.Clear();

				foreach (var movie in movieDtos)
				{
					SqliteCommand selectCommand = new SqliteCommand(MovieSqlConsts.SelectByNameCommand, db);
					selectCommand.Parameters.AddWithValue(MovieSqlConsts.ParamName, movie.Title);
					SqliteCommand insertCommand = new SqliteCommand
					{
						Connection = db,
						CommandText = MovieSqlConsts.InsertCommandText
					};

					var movieId = Guid.NewGuid();
					_movieGuids.Add(movieId);
					insertCommand.Parameters.AddWithValue(MovieSqlConsts.ParamMovieId, movieId.ToString());
					insertCommand.Parameters.AddWithValue(MovieSqlConsts.ParamName, movie.Title);
					insertCommand.Parameters.AddWithValue(MovieSqlConsts.ParamYear, movie.ReleaseYear);
					insertCommand.Parameters.AddWithValue(MovieSqlConsts.ParamRunTime, movie.Duration);
					insertCommand.Parameters.AddWithValue(MovieSqlConsts.ParamPoster, movie.Poster);
					insertCommand.Parameters.AddWithValue(MovieSqlConsts.ParamDirector, movie.Director);

					try
					{
						var query = await selectCommand.ExecuteReaderAsync();
						var stringList = new List<string>();

						while (query.Read())
						{
							stringList.Add(query.GetString(0));
						}

						if (stringList.Count == 0)
						{
							await insertCommand.ExecuteReaderAsync();
						}

					}
					catch (Exception e)
					{
					}
					finally
					{
						insertCommand.Dispose();
						selectCommand.Dispose();
					}
				}

				await AddCacheData(url);


			}
		}

		public async Task<List<FilmDto>> GrabAllData()
		{
			var dataList = new List<FilmDto>();

			using (SqliteConnection db = new SqliteConnection(MovieSqlConsts.DbFileName))
			{
				db.Open();

				SqliteCommand selectCommand = new SqliteCommand(MovieSqlConsts.SelectAllCommand, db);

				try
				{
					var query = await selectCommand.ExecuteReaderAsync();
					while (query.Read())
					{
						var a = new FilmDto
						{
							Title = query.GetString(1),
							ReleaseYear = query.GetString(2),
							Duration = query.GetString(3),
							Poster = query.GetString(4),
							Director = query.GetString(5)
						};

						dataList.Add(a);
					}
				}
				catch (Exception e)
				{
				}
				finally
				{
					selectCommand.Dispose();
					db.Close();
				}
			}
			return dataList;
		}

		public async Task<List<FilmDto>> GetMoviesByDirector(string director)
		{
			var resultList = new List<FilmDto>();

			using (SqliteConnection db = new SqliteConnection(MovieSqlConsts.DbFileName))
			{
				db.Open();

				var selectCommand = new SqliteCommand
				{
					Connection = db,
					CommandText = MovieSqlConsts.SelectByDirectorCommand
				};

				selectCommand.Parameters.AddWithValue(MovieSqlConsts.ParamDirector, director);

				try
				{
					var query = await selectCommand.ExecuteReaderAsync();

					while (query.Read())
					{
						resultList.Add(
							new FilmDto()
							{
								Title = query.GetString(1),
								ReleaseYear = query.GetString(2),
								Duration = query.GetString(3),
								Poster = query.GetString(4),
								Director = query.GetString(5)
							});

					}

				}
				catch (Exception e)
				{
				}

				return resultList;
			}
		}

		public async Task<List<string>> GetCacheIdsByTimeSpan(double configTimeSpan)
		{
			var list = new List<string>();
			using (SqliteConnection db = new SqliteConnection(MovieSqlConsts.DbFileName))
			{
				db.Open();
				var selectCommand = new SqliteCommand(MovieSqlConsts.SelectAllCacheCommand, db);
				try
				{
					var query = await selectCommand.ExecuteReaderAsync();
					while (query.Read())
					{
						var timeSpan = DateTime.Now - query.GetDateTime(2);
						if (timeSpan.TotalSeconds >= configTimeSpan)
						{
							list.Add(query.GetString(0));
						}
					}
				}
				catch (SqliteException e)
				{
				}
				finally
				{
					selectCommand.Dispose();
				}
			}

			return list;
		}

		public async Task<List<string>> GetMoviesIdsByCacheId(string cacheId)
		{
			var stringList = new List<string>();
			using (SqliteConnection db = new SqliteConnection(MovieSqlConsts.DbFileName))
			{
				db.Open();
				var selectCommand = new SqliteCommand(MovieSqlConsts.SelectAllMovieIdByCacheId, db);
				selectCommand.Parameters.AddWithValue(MovieSqlConsts.ParamCacheId, cacheId);
				try
				{
					var query = await selectCommand.ExecuteReaderAsync();
					while (query.Read())
					{
						stringList.Add(query.GetString(0));
					}
					return stringList;
				}
				catch (SqliteException e)
				{
					return null;
				}
			}
		}

		public async Task DeleteMoviesById(List<string> idsList)
		{
			using (SqliteConnection db = new SqliteConnection(MovieSqlConsts.DbFileName))
			{
				db.Open();
				try
				{
					foreach (var id in idsList)
					{
						var deleteCommand = new SqliteCommand(MovieSqlConsts.DeleteMovieByIdCommand, db);
						deleteCommand.Parameters.AddWithValue(MovieSqlConsts.ParamMovieId, id);
						await deleteCommand.ExecuteReaderAsync();
						deleteCommand.Dispose();
					}

				}
				catch (SqliteException e)
				{

				}
			}
		}

		public async Task DeleteBuffById(string cacheId)
		{
			using (SqliteConnection db = new SqliteConnection(MovieSqlConsts.DbFileName))
			{
				db.Open();
				try
				{
					var deleteCommand = new SqliteCommand(MovieSqlConsts.DeleteBuffByIdCommand, db);
					deleteCommand.Parameters.AddWithValue(MovieSqlConsts.ParamCacheId, cacheId);
					await deleteCommand.ExecuteReaderAsync();
					deleteCommand.Dispose();
				}
				catch (SqliteException e)
				{
				}
			}
		}

		public async Task DeleteCacheById(string cacheId)
		{
			using (SqliteConnection db = new SqliteConnection(MovieSqlConsts.DbFileName))
			{
				db.Open();
				try
				{
					var deleteCommand = new SqliteCommand(MovieSqlConsts.DeleteCacheByIdCommand, db);
					deleteCommand.Parameters.AddWithValue(MovieSqlConsts.ParamCacheId, cacheId);
					await deleteCommand.ExecuteReaderAsync();
					deleteCommand.Dispose();
				}
				catch (SqliteException e)
				{
				}
			}
		}

		public async Task<List<string>> GetCacheIdByUrl(string url)
		{
			var stringList = new List<string>();
			using (SqliteConnection db = new SqliteConnection(MovieSqlConsts.DbFileName))
			{
				db.Open();
				var selectCommand = new SqliteCommand(MovieSqlConsts.SelectCacheIdByUrl, db);
				selectCommand.Parameters.AddWithValue(MovieSqlConsts.ParamUrl, url);
				try
				{
					var query = await selectCommand.ExecuteReaderAsync();
					while (query.Read())
					{
						stringList.Add(query.GetString(0));
					}
					return stringList;
				}
				catch (SqliteException e)
				{
					return null;
				}
			}
		}

		public async Task<List<FilmDto>> GetCacheMovies(string url)
		{
			var dataList = new List<FilmDto>();
			using (SqliteConnection db = new SqliteConnection(MovieSqlConsts.DbFileName))
			{
				db.Open();
				var selectCommand = new SqliteCommand(MovieSqlConsts.SelectCachMoviesByUrl, db);
				selectCommand.Parameters.AddWithValue(MovieSqlConsts.ParamUrl, url);
				try
				{
					var query = await selectCommand.ExecuteReaderAsync();
					while (query.Read())
					{
						var a = new FilmDto
						{
							Title = query.GetString(0),
							ReleaseYear = query.GetString(1),
							Duration = query.GetString(2),
							Poster = query.GetString(3),
							Director = query.GetString(4)
						};

						dataList.Add(a);
					}
				}
				catch (SqliteException e)
				{
					return null;
				}
			}
			return dataList;
		}

		////		private async void CheckForClearCache()
		////		{
		////			var appConfig = await ConfigService.GetCacheTimeoutSerializeAsync();
		////			if (appConfig.NextCheck == DateTime.Now)
		////			{
		////				ClearCache();
		////			}
		////		}
		////
		////		private async Task<AppConfigurations> GetCachePeriodAsync()
		////		{
		////			var cacheTimeOut = await ConfigService.GetCacheTimeoutSerializeAsync();
		////			return cacheTimeOut;
		////
		////		}
	}
}
