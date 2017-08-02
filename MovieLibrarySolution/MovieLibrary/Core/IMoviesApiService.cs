using MovieLibrary.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieLibrary.Core
{
	public interface IMoviesApiService
	{
		Task<IList<Film>> GetFilmsByDirectorAsync(string director);
		Task<IList<Film>> GetFilmsByNDirectorAsync();
	}
}
