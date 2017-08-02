using AutoMapper;
using MovieLibrary.Core;
using MovieLibrary.Model;
using MovieLibraryBL.Core;
using MovieLibraryBL.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.ApiServices
{

	public class MoviesApiService : IMoviesApiService
	{
		public IFilmService FilmService { get; set; }

		public MoviesApiService(IFilmService filmService)
		{
			FilmService = filmService;
		}


		public async Task<IList<Film>> GetFilmsByDirectorAsync(string director)
		{
			var fillData = await FilmService.GetFilmsByDirector(director);
			return Mapper.Map<IList<FilmDto>, IList<Film>>(fillData);
		}

		public async Task<IList<Film>> GetFilmsByNDirectorAsync()
		{
			List<FilmDto> fillData = new List<FilmDto>();

			foreach (var director in GetDirectors())
			{
				fillData.AddRange(await FilmService.GetFilmsByDirector(director));
			}
			
			return Mapper.Map<IList<FilmDto>, IList<Film>>(fillData);
		}

		private IEnumerable<string> GetDirectors()
		{
			return new Collection<string>
			{
				//"Woody Allen",
				"William Wyler",
				"Martin Scorsese",
				"David Lynch",
				"Ridley Scott"
			};
		} 
	}
}
