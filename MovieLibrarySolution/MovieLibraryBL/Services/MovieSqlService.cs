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
		private IConfigService ConfigService { get; }
		private static byte CachePeriod { get; set; }



		public MovieSqlService(IConfigService configService)
		{
			ConfigService = configService;		
		}

		public async void InitilizeDataBase()
		{
			SqliteEngine.UseWinSqlite3();

			var appConfig = await GetCachePeriodAsync();
			CachePeriod = appConfig.CacheTimeOutDuration;


			using (var db = new SqliteConnection(MovieSqlConsts.SqliteConnection))
				{

					db.Open();
					if (!TableExists(db))
					{

						var createTable = new SqliteCommand(MovieSqlConsts.CreateTableScript, db);
						try
						{
							createTable.ExecuteReader();
							ConfigService.WriteCacheTimeoutSerializeAsync(new AppConfigurations { CacheTimeOutDuration = CachePeriod, NextCheck = DateTime.Now.AddDays(CachePeriod) });
						}
						catch (SqliteException e)
						{
							throw new Exception(e.Message);
						}
					}
					
				}
			}



		public static bool TableExists(SqliteConnection connection)
		{
			using (var cmd = new SqliteCommand())
			{
				cmd.CommandType = CommandType.Text;
				cmd.Connection = connection;
				cmd.CommandText = MovieSqlConsts.CheckIfExistTableScript;
				cmd.Parameters.AddWithValue(MovieSqlConsts.CheckIfExistTableScriptParam, MovieSqlConsts.TableName);

				using (var sqlDataReader = cmd.ExecuteReader())
				{

					if (sqlDataReader.Read())
					{
						return true;
					}

					return false;
				}
			}
		}


		public async void AddDataAsync(IList<FilmDto> filmListDto)
		{
			using (var db = new SqliteConnection(MovieSqlConsts.SqliteConnection))
			{
				db.Open();

				foreach (var movie in filmListDto)
				{
					var insertCommand = new SqliteCommand
					{
						Connection = db,
						CommandText = MovieSqlConsts.AddDataScript
					};

					insertCommand.Parameters.AddWithValue(MovieSqlConsts.AddDataScriptScriptParamTitle, movie.Title);
					insertCommand.Parameters.AddWithValue(MovieSqlConsts.AddDataScriptScriptParamReleaseYear, movie.ReleaseYear);
					insertCommand.Parameters.AddWithValue(MovieSqlConsts.AddDataScriptScriptParamRunTime, movie.Duration);
					insertCommand.Parameters.AddWithValue(MovieSqlConsts.AddDataScriptScriptParamPoster, movie.Poster);
					insertCommand.Parameters.AddWithValue(MovieSqlConsts.AddDataScriptScriptParamDirector, movie.Director);
					insertCommand.Parameters.AddWithValue(MovieSqlConsts.AddDataScriptScriptParamEditTime, DateTime.Now.Date.AddDays(CachePeriod));
					try
					{
						await insertCommand.ExecuteReaderAsync();
					}
					catch (Exception)
					{

						throw new InvalidDataException();
					}
					finally
					{
						insertCommand.Dispose();
					}
				}

			}
		}

		public async Task<IList<FilmDto>> GrabDataAsync()
		{
			CheckForClearCache();

			var films = new List<FilmDto>();
			using (var db = new SqliteConnection(MovieSqlConsts.SqliteConnection))

			{
						

				db.Open();

				var selectCommand = new SqliteCommand(MovieSqlConsts.GrabDataScript, db);

				SqliteDataReader query;

				try
				{
					query = await selectCommand.ExecuteReaderAsync();
				}
				catch (Exception e)
				{
					throw new Exception(e.Message);
				}
				while (query.Read())
				{
					var film = new FilmDto
					{
						Title = query.GetString(1),
						ReleaseYear = query.GetString(2),
						Duration = query.GetString(3),
						Poster = query.GetString(4),
						Director = query.GetString(5)
					};
					films.Add(film);
				}
				db.Close();
			}

			return films;
		}


		public async Task<IList<FilmDto>> SearchByDirectorAsync(string director)
		{
			CheckForClearCache();


			var films = new List<FilmDto>();
			using (var db = new SqliteConnection(MovieSqlConsts.SqliteConnection))

			{
				db.Open();

				var selectCommand = new SqliteCommand(MovieSqlConsts.SearchByDirectorScript, db);
				selectCommand.Parameters.AddWithValue(MovieSqlConsts.SearchByDirectorScriptParam, director);
				SqliteDataReader query;

				try
				{
					query = await selectCommand.ExecuteReaderAsync();
				}
				catch (Exception e)
				{
					throw new Exception(e.Message);
				}
				while (query.Read())
				{
					var film = new FilmDto
					{
						Title = query.GetString(1),
						ReleaseYear = query.GetString(2),
						Duration = query.GetString(3),
						Poster = query.GetString(4),
						Director = query.GetString(5)
					};
					films.Add(film);
				}
				db.Close();
			}

			return films;
		}

		private async void ClearCache()
		{
			using (var db = new SqliteConnection(MovieSqlConsts.SqliteConnection))
			{
				db.Open();

					var command = new SqliteCommand
					{
						Connection = db,
						CommandText = MovieSqlConsts.ClearCacheScript
					};

				var appConfig = await GetCachePeriodAsync();
					command.Parameters.AddWithValue(MovieSqlConsts.ClearCacheScriptParam, DateTime.Now.Date.AddDays(appConfig.CacheTimeOutDuration));
					try
					{
						await command.ExecuteReaderAsync();
					}
					catch (Exception)
					{

						throw new InvalidDataException();
					}
					finally
					{
						command.Dispose();
						ConfigService.WriteCacheTimeoutSerializeAsync(new AppConfigurations{ CacheTimeOutDuration = CachePeriod, NextCheck = DateTime.Now.AddDays(appConfig.CacheTimeOutDuration) });
					}
				

			}
		}

		private async void CheckForClearCache()
		{
			var appConfig = await ConfigService.GetCacheTimeoutSerializeAsync();
			if (appConfig.NextCheck == DateTime.Now)
			{
				ClearCache();
			}
		}

		private async Task<AppConfigurations> GetCachePeriodAsync()
		{
			var cacheTimeOut = await ConfigService.GetCacheTimeoutSerializeAsync();
			return cacheTimeOut;

		}
	}
}
