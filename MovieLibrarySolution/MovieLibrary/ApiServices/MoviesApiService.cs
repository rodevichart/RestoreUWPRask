using AutoMapper;
using MovieLibrary.Core;
using MovieLibrary.Model;
using MovieLibraryBL.Core;
using MovieLibraryBL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
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
	}
}
