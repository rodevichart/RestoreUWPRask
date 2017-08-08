using System.Collections.Generic;
using System.Threading.Tasks;
using MovieLibrary.Model;

namespace MovieLibrary.Core
{
	public interface IMovieSqlHelper
	{
		Task<IList<Film>> GetFilmsDataAsync();
		void AddFilmsData(IList<Film> films);
		Task<IList<Film>> GetFilmsDataByDirectorAsync(string director);
	}
}