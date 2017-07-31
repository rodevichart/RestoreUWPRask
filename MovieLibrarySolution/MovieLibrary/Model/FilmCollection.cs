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
			//Films = FakeData.GetData();
			Films = new List<Film>();
		}

		public FilmCollection(IList<Film> films)
		{
			Films = films;
		}

		//public async Task<ICollection<Film>> GetFilmsByDirectorAsync(string director)
		//{
		//	var service = new FilmService();
		//	var fillData = await service.GetFilmsByDirector(director);
		//	return Mapper.Map<ICollection<FilmDto>,ICollection<Film>>(fillData);
		//}
		

		public void Update(Film movie)
		{

		}
	}
}