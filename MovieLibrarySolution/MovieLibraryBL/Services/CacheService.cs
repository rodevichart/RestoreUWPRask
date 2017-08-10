using System.Collections.Generic;
using System.Threading.Tasks;
using MovieLibraryBL.Core;
using MovieLibraryBL.DTOs;
using MovieLibraryBL.Persistance;

namespace MovieLibraryBL.Services
{
	public class CacheService : ICacheService
	{
		private readonly IMovieSqlService _movieSqlService;
		private readonly IConfigService _configService;


		private readonly double _configTimeSpan;

		public CacheService(IMovieSqlService sqlService, IConfigService configService)
		{
			_movieSqlService = sqlService;
			_configService = configService;
			_configTimeSpan = _configService.GetCacheTimeoutSerializeAsync().Result.CacheTimeOutDuration;
		}

		public async Task ScanDataForClean()
		{
			
			var ids = await _movieSqlService.GetCacheIdsByTimeSpan(_configTimeSpan);
			if (ids.Count != 0)
			{

				foreach (var id in ids)
				{
					var moviesId = await _movieSqlService.GetMoviesIdsByCacheId(id);
					await _movieSqlService.DeleteMoviesById(moviesId);
					await _movieSqlService.DeleteBuffById(id);
					await _movieSqlService.DeleteCacheById(id);

				}
			}

		}

		public async Task<List<FilmDto>> GetMovies(string url)
		{
			return await _movieSqlService.GetCacheMovies(url);
		}


		public async Task<bool> CheckExistMovieByUrl(string url)
		{
			return await _movieSqlService.CheckIfExistUrl(url);
		}

	}
}