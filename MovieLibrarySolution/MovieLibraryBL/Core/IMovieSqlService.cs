using System.Collections.Generic;
using System.Threading.Tasks;
using MovieLibraryBL.DTOs;

namespace MovieLibraryBL.Core
{
	public interface IMovieSqlService
	{
		void InitilizeDataBase();
		void AddDataAsync(IList<FilmDto> filmListDto);
		Task<IList<FilmDto>> GrabDataAsync();
		Task<IList<FilmDto>> SearchByDirectorAsync(string director);
	}
}