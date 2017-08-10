namespace MovieLibraryBL.Services
{
	public class MovieSqlConsts
	{
		public static string MovieTableCommand =>
			"CREATE TABLE IF NOT EXISTS MoviesTable (Movie_Id TEXT PRIMARY KEY," +
			" Name TEXT NOT NULL, Year TEXT NULL," +
			" RunTime TEXT NULL," +
			" Poster TEXT NULL," +
			" Director TEXT NULL)";


		public static string BuffTableCommand =>
			"CREATE TABLE IF NOT EXISTS BuffTable (Buff_Id INTEGER PRIMARY KEY AUTOINCREMENT," +
			" Movie_Id TEXT NOT NULL, Cache_Id TEXT NOT NULL)";


		public static string CacheTableCommand =>
			"CREATE TABLE IF NOT EXISTS CacheTable (Cache_Id TEXT PRIMARY KEY," +
			" URL TEXT NOT NULL, CreationData INTEGER NOT NULL)";



		public static string DbFileName => "Filename=sqliteSample.db";

		public static string InsertCommandText =>
			"INSERT INTO MoviesTable VALUES (@MovieId, @Name, @Year, @RunTime, @Poster, @Director);";




		public static string InsertCacheCommandText => "INSERT INTO CacheTable VALUES (@CacheId, @Url, @CreationData);";
		public static string InsertBuffCommandText => "INSERT INTO BuffTable VALUES (NULL, @MovieId, @CacheId);";

		public static string SelectByNameCommand => "SELECT Name FROM MoviesTable WHERE Name == @Name";

		public static string SelectByDirectorCommand => "SELECT * FROM MoviesTable WHERE Director == @Director";

		public static string SelectAllCommand => "SELECT * from MoviesTable";

		public static string DeleteByNameCommand => "DELETE from MoviesTable WHERE Name == @Name";

		public static string ParamMovieId => "@MovieId";

		public static string ParamCacheId => "@CacheId";

		public static string ParamUrl => "@Url";

		public static string ParamName => "@Name";

		public static string ParamYear => "@Year";

		public static string ParamRunTime => "@RunTime";

		public static string ParamPoster => "@Poster";

		public static string ParamDirector => "@Director";

		public static string ParamCreationDate => "@CreationData";

		public static string SelectAllMoviesCommand => "SELECT * from MoviesTable;";

		public static string SelectAllCacheCommand => "SELECT * from CacheTable;";

		public static string SelectAllMovieIdByCacheId => "SELECT Movie_Id from BuffTable WHERE Cache_Id == @CacheId;";

		public static string DeleteMovieByIdCommand => "DELETE from MoviesTable WHERE Movie_Id == @MovieId;";

		public static string DeleteBuffByIdCommand => "DELETE from BuffTable WHERE Cache_Id == @CacheId;";

		public static string DeleteCacheByIdCommand => "DELETE from CacheTable WHERE Cache_Id == @CacheId";

		//public static string SelectCacheIdByUrl => "SELECT Cache_Id from CacheTable WHERE URL == @Url;";

		public static string CheckIfExistUrlInCache => "SELECT EXISTS(SELECT 1 FROM CacheTable WHERE URL=@Url LIMIT 1);";

		public static string SelectCachMoviesByUrl => "SELECT Name, Year, RunTime, Poster, Director from MoviesTable " +
		                                              "Left join CacheTable on BuffTable.Cache_Id = CacheTable.Cache_Id " +
		                                              "Join BuffTable on MoviesTable.Movie_Id = BuffTable.Movie_Id " +
		                                              "Where URL == @Url";

	}
}