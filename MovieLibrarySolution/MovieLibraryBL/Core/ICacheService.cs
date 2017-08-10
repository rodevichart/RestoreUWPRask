using System.Collections.Generic;
using System.Threading.Tasks;
using MovieLibraryBL.DTOs;

namespace MovieLibraryBL.Core
{
	public interface ICacheService
	{
		Task ScanDataForClean();
		Task<List<FilmDto>> GetMovies(string url);
	}
}