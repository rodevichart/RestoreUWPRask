using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MovieLibraryBL.Services;
using MovieLibraryBL.DTOs;

namespace MovieLibrary.Model
{
	public class FilmCollection 
	{
		public IList<Film> Films { get; set; }

		public FilmCollection()
		{
			Films = new List<Film>();
		}

		public FilmCollection(IList<Film> films)
		{
			Films = films;
		}

		public void Update(Film movie)
		{

		}
	}
}