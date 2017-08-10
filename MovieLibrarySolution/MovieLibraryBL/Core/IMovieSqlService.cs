using System.Collections.Generic;
using System.Threading.Tasks;
using MovieLibraryBL.DTOs;

namespace MovieLibraryBL.Core
{
	public interface IMovieSqlService
	{
		Task InitDataBase();
		Task AddMoviesData(List<FilmDto> movieDtos, string url);
		Task<List<FilmDto>> GrabAllData();
		Task<List<FilmDto>> GetMoviesByDirector(string director);
		Task<List<string>> GetCacheIdsByTimeSpan(double configTimeSpan);
		Task<List<string>> GetMoviesIdsByCacheId(string cacheId);
		Task DeleteMoviesById(List<string> idsList);
		Task DeleteBuffById(string cacheId);
		Task DeleteCacheById(string cacheId);
		Task<List<string>> GetCacheIdByUrl(string url);
		Task<List<FilmDto>> GetCacheMovies(string url);
	}
}