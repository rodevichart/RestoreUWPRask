using MovieLibrary.Model;
using System.Collections.Generic;

namespace FilmLibrary.Model
{
	public class FakeData
	{
				public static List<Film> GetData()
			{
				return new List<Film>()
				{
					new Film{Title = "bbb", Poster = "sdsd", Duration = "123", ReleaseYear = "1223"},
					new Film{Title = "qqq", Poster = "qad", Duration = "123", ReleaseYear = "1223"},
					new Film{Title = "qwe", Poster = "sdf", Duration = "123", ReleaseYear = "1223"},
					new Film{Title = "rtw", Poster = "msd", Duration = "123", ReleaseYear = "1223"}

				};
			}
		}
}