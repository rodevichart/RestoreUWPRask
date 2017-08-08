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
		private const string DbName = "sqlCache.db";
		private const string TableName = "Movies";

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


			using (var db = new SqliteConnection($"Filename={DbName}"))
				{

					db.Open();
					if (!TableExists(db))
					{
					var tableCommand = $"CREATE TABLE {TableName} (Id INTEGER PRIMARY KEY AUTOINCREMENT," +
					                   " Title TEXT NOT NULL, ReleaseYear TEXT NULL," +
					                   " RunTime TEXT NULL, Poster TEXT NULL," +
					                   " EditTime DATETIME," +
					                   " Director TEXT NULL)";
						var createTable = new SqliteCommand(tableCommand, db);
						try
						{
							createTable.ExecuteReader();
							ConfigService.WriteCacheTimeoutSerializeAsync(new AppConfigurations { CacheTimeOutDuration = CachePeriod, NextCheck = DateTime.Now });
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
				cmd.CommandText = "SELECT * FROM sqlite_master WHERE type = 'table' AND name = @name";
				cmd.Parameters.AddWithValue("@name", TableName);

				using (SqliteDataReader sqlDataReader = cmd.ExecuteReader())
				{
					if (sqlDataReader.Read())
						return true;
					else
						return false;
				}
			}
		}


		public async void AddDataAsync(IList<FilmDto> filmListDto)
		{
			using (SqliteConnection db = new SqliteConnection($"Filename={DbName}"))
			{
				db.Open();

				foreach (var movie in filmListDto)
				{
					var insertCommand = new SqliteCommand
					{
						Connection = db,
						CommandText = $"INSERT INTO {TableName} VALUES (NULL, @Title, @ReleaseYear, @RunTime, @Poster, @Director, @EditTime);"
					};

					insertCommand.Parameters.AddWithValue("@Title", movie.Title);
					insertCommand.Parameters.AddWithValue("@ReleaseYear", movie.ReleaseYear);
					insertCommand.Parameters.AddWithValue("@RunTime", movie.Duration);
					insertCommand.Parameters.AddWithValue("@Poster", movie.Poster);
					insertCommand.Parameters.AddWithValue("@Director", movie.Director);
					insertCommand.Parameters.AddWithValue("@EditTime", DateTime.Now.Date.AddDays(CachePeriod));
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
			using (var db = new SqliteConnection($"Filename={DbName}"))

			{
						

				db.Open();

				var selectCommand = new SqliteCommand($"SELECT * from {TableName}", db);

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
			using (var db = new SqliteConnection($"Filename={DbName}"))

			{
				db.Open();

				var selectCommand = new SqliteCommand($"SELECT * from {TableName} WHERE Director==@Director", db);
				selectCommand.Parameters.AddWithValue("@Director", director);
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
			using (SqliteConnection db = new SqliteConnection($"Filename={DbName}"))
			{
				db.Open();

					var command = new SqliteCommand
					{
						Connection = db,
						CommandText = $"DELETE FROM {TableName} WHERE EditTime>=@EditTime;"
					};

				var appConfig = await GetCachePeriodAsync();
					command.Parameters.AddWithValue("@EditTime", DateTime.Now.Date.AddDays(appConfig.CacheTimeOutDuration));
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
						ConfigService.WriteCacheTimeoutSerializeAsync(new AppConfigurations{ CacheTimeOutDuration = CachePeriod, NextCheck = DateTime.Now});
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
