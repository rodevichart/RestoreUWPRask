namespace MovieLibraryBL.Services
{
	public class MovieSqlConsts
	{
		public static readonly string DbName = "sqlCache.db";
		public static readonly string TableName = "Movies";


		public static readonly string SqliteConnection = $"Filename={DbName}";


		public static readonly string CreateTableScript = $"CREATE TABLE {TableName} (Id INTEGER PRIMARY KEY AUTOINCREMENT," +
		                                                  " Title TEXT NOT NULL, ReleaseYear TEXT NULL," +
		                                                  " RunTime TEXT NULL, Poster TEXT NULL," +
		                                                  " EditTime DATETIME," +
		                                                  " Director TEXT NULL)";



		public static readonly string CheckIfExistTableScriptParam = "@name";
		public static readonly string CheckIfExistTableScript = $"SELECT * FROM sqlite_master WHERE type = 'table' AND name = {CheckIfExistTableScriptParam}";


		public static readonly string AddDataScriptScriptParamId = "NULL";
		public static readonly string AddDataScriptScriptParamTitle = "@Title";
		public static readonly string AddDataScriptScriptParamReleaseYear = "@ReleaseYear";
		public static readonly string AddDataScriptScriptParamRunTime = "@RunTime";
		public static readonly string AddDataScriptScriptParamPoster = "@Poster";
		public static readonly string AddDataScriptScriptParamDirector = "@Director";
		public static readonly string AddDataScriptScriptParamEditTime = "@EditTime";
		public static readonly string AddDataScript = $"INSERT INTO {TableName} VALUES ({AddDataScriptScriptParamId}, {AddDataScriptScriptParamTitle}, {AddDataScriptScriptParamReleaseYear}, {AddDataScriptScriptParamRunTime},"+
														$" {AddDataScriptScriptParamPoster}, {AddDataScriptScriptParamDirector}, {AddDataScriptScriptParamEditTime});";


		public static readonly string ClearCacheScriptParam = "@EditTime";
		public static readonly string ClearCacheScript = $"DELETE FROM {TableName} WHERE EditTime>={ClearCacheScriptParam};";


		public static readonly string GrabDataScript = $"SELECT * from {TableName}";



		public static readonly string SearchByDirectorScriptParam = "@Director";
		public static readonly string SearchByDirectorScript = $"SELECT * from {TableName} WHERE Director=={SearchByDirectorScriptParam}";
	}
}