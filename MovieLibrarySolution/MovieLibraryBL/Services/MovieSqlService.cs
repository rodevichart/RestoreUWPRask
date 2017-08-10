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
		private readonly List<Guid> _movieGuids;

		public MovieSqlService()
		{
			_movieGuids = new List<Guid>();		
		}

		public async Task InitDataBase()
		{
			SqliteEngine.UseWinSqlite3();

			using (var db = new SqliteConnection(MovieSqlConsts.DbFileName))
			{
				db.Open();
				using (SqliteCommand createMoviesTable = new SqliteCommand(MovieSqlConsts.MovieTableCommand, db),
						 createBuffTable = new SqliteCommand(MovieSqlConsts.BuffTableCommand, db),
						 createCacheTable = new SqliteCommand(MovieSqlConsts.CacheTableCommand, db))
				{
					await createMoviesTable.ExecuteReaderAsync();
					await createBuffTable.ExecuteReaderAsync();
					await createCacheTable.ExecuteReaderAsync();
				}
			}
		}

		private async Task AddBuffData(Guid idCache)
		{
			using (var db = new SqliteConnection(MovieSqlConsts.DbFileName))
			{

				db.Open();
				foreach (var id in _movieGuids)
				{
					using (var insertCommand = new SqliteCommand(MovieSqlConsts.InsertBuffCommandText, db))
					{
						insertCommand.Parameters.AddWithValue(MovieSqlConsts.ParamCacheId, idCache.ToString());
						insertCommand.Parameters.AddWithValue(MovieSqlConsts.ParamMovieId, id.ToString());
						await insertCommand.ExecuteReaderAsync();
					}
				}
			}
		}


		private async Task AddCacheData(string url)
		{
			using (SqliteConnection db = new SqliteConnection(MovieSqlConsts.DbFileName))
			{
				db.Open();
				var cacheId = Guid.NewGuid();
				using (var insertCommand = new SqliteCommand(MovieSqlConsts.InsertCacheCommandText, db))
				{
					insertCommand.Parameters.AddWithValue(MovieSqlConsts.ParamCacheId, cacheId.ToString());
					insertCommand.Parameters.AddWithValue(MovieSqlConsts.ParamUrl, url);
					insertCommand.Parameters.AddWithValue(MovieSqlConsts.ParamCreationDate, DateTime.Now);
					await insertCommand.ExecuteReaderAsync();
				}
				await AddBuffData(cacheId);
			}

		}

		public async Task AddMoviesData(List<FilmDto> movieDtos, string url)
		{
			using (var db = new SqliteConnection(MovieSqlConsts.DbFileName))
			{
				db.Open();
				_movieGuids.Clear();

				foreach (var movie in movieDtos)
				{
					using (var insertCommand = new SqliteCommand(MovieSqlConsts.InsertCommandText,db))
					{
						var movieId = Guid.NewGuid();
						_movieGuids.Add(movieId);
						insertCommand.Parameters.AddWithValue(MovieSqlConsts.ParamMovieId, movieId.ToString());
						insertCommand.Parameters.AddWithValue(MovieSqlConsts.ParamName, movie.Title);
						insertCommand.Parameters.AddWithValue(MovieSqlConsts.ParamYear, movie.ReleaseYear);
						insertCommand.Parameters.AddWithValue(MovieSqlConsts.ParamRunTime, movie.Duration);
						insertCommand.Parameters.AddWithValue(MovieSqlConsts.ParamPoster, movie.Poster);
						insertCommand.Parameters.AddWithValue(MovieSqlConsts.ParamDirector, movie.Director);

						await insertCommand.ExecuteNonQueryAsync();

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

				using (var selectCommand = new SqliteCommand(MovieSqlConsts.SelectAllCommand, db))
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
					return dataList;
				}

			}
		}

		public async Task<List<FilmDto>> GetMoviesByDirector(string director)
		{
			var resultList = new List<FilmDto>();

			using (SqliteConnection db = new SqliteConnection(MovieSqlConsts.DbFileName))
			{
				db.Open();


				using (var selectCommand = new SqliteCommand(MovieSqlConsts.SelectByDirectorCommand, db))
				{
					selectCommand.Parameters.AddWithValue(MovieSqlConsts.ParamDirector, director);
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
					return resultList;
				}

			}
		}

		public async Task<List<string>> GetCacheIdsByTimeSpan(double configTimeSpan)
		{
			var list = new List<string>();
			using (SqliteConnection db = new SqliteConnection(MovieSqlConsts.DbFileName))
			{
				db.Open();
				using (var selectCommand = new SqliteCommand(MovieSqlConsts.SelectAllCacheCommand, db))
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
					return list;
				}

			}
		}

		public async Task<List<string>> GetMoviesIdsByCacheId(string cacheId)
		{
			var stringList = new List<string>();
			using (SqliteConnection db = new SqliteConnection(MovieSqlConsts.DbFileName))
			{
				db.Open();
				using (var selectCommand = new SqliteCommand(MovieSqlConsts.SelectAllMovieIdByCacheId, db))
				{
					selectCommand.Parameters.AddWithValue(MovieSqlConsts.ParamCacheId, cacheId);
					var query = await selectCommand.ExecuteReaderAsync();
					while (query.Read())
					{
						stringList.Add(query.GetString(0));
					}
					return stringList;

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
						
						using (var deleteCommand = new SqliteCommand(MovieSqlConsts.DeleteMovieByIdCommand, db))
						{
							deleteCommand.Parameters.AddWithValue(MovieSqlConsts.ParamMovieId, id);
							await deleteCommand.ExecuteReaderAsync();
						}					}

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
					using (var deleteCommand = new SqliteCommand(MovieSqlConsts.DeleteBuffByIdCommand, db))
					{
						deleteCommand.Parameters.AddWithValue(MovieSqlConsts.ParamCacheId, cacheId);
						await deleteCommand.ExecuteReaderAsync();
					}
				}
				catch (SqliteException e)
				{
				}
			}
		}

		public async Task DeleteCacheById(string cacheId)
		{
			using (var db = new SqliteConnection(MovieSqlConsts.DbFileName))
			{
				db.Open();
				try
				{
					using (var deleteCommand = new SqliteCommand(MovieSqlConsts.DeleteCacheByIdCommand, db))
					{
						deleteCommand.Parameters.AddWithValue(MovieSqlConsts.ParamCacheId, cacheId);
						await deleteCommand.ExecuteReaderAsync();

					}
				}
				catch (SqliteException e)
				{
				}
			}
		}

		public async Task<bool> CheckIfExistUrl(string url)
		{
			using (var db = new SqliteConnection(MovieSqlConsts.DbFileName))
			{
				db.Open();
				using (var selectCommand = new SqliteCommand(MovieSqlConsts.CheckIfExistUrlInCache, db))
				{
					selectCommand.Parameters.AddWithValue(MovieSqlConsts.ParamUrl, url);

					var query = await selectCommand.ExecuteReaderAsync();
					while (query.Read())
					{
						return query.GetBoolean(0);
					}

				}
			}
			return false;
		}

		public async Task<List<FilmDto>> GetCacheMovies(string url)
		{
			var dataList = new List<FilmDto>();
			using (var db = new SqliteConnection(MovieSqlConsts.DbFileName))
			{
				db.Open();

				using (var selectCommand = new SqliteCommand(MovieSqlConsts.SelectCachMoviesByUrl, db))
				{
					selectCommand.Parameters.AddWithValue(MovieSqlConsts.ParamUrl, url);

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
			}
			return dataList;
		}
	}
}
