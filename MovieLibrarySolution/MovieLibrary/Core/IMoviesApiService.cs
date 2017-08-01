using MovieLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Core
{
	public interface IMoviesApiService
	{
		Task<IList<Film>> GetFilmsByDirectorAsync(string director);
	}
}
