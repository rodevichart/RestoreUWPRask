using MovieLibraryBL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieLibraryBL.Services
{
	public interface IFilmService
	{
		Task<IList<FilmDto>> GetFilmsByDirector(string director);
		FilmDto GetFilmsByTitleNYear(string title, string year);
	}
}