using MovieLibraryBL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieLibraryBL.Core
{
	public interface IFilmService
	{
		Task<IList<FilmDto>> GetFilmsByDirector(string director);
	}
}