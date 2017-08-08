using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MovieLibrary.Core;
using MovieLibrary.Model;
using MovieLibraryBL.Core;
using MovieLibraryBL.DTOs;

namespace MovieLibrary.ApiServices
{
	public class MovieSqlHelper : IMovieSqlHelper
	{
		public IMovieSqlService SqlService { get; set; }

		public MovieSqlHelper(IMovieSqlService movieSqlService)
		{
			SqlService = movieSqlService;
			SqlService.InitilizeDataBase();
		}

		public async Task<IList<Film>> GetFilmsDataAsync()
		{
			
			var data = await SqlService.GrabDataAsync();

			return Mapper.Map<IList<FilmDto>, IList<Film>>(data);
		}

		public async Task<IList<Film>> GetFilmsDataByDirectorAsync(string director)
		{
			
			var data = await SqlService.SearchByDirectorAsync(director);

			return Mapper.Map<IList<FilmDto>, IList<Film>>(data);
		}

		public void AddFilmsData(IList<Film> films)
		{
			var resultData = Mapper.Map<IList<Film>, IList<FilmDto>>(films);

			SqlService.AddDataAsync(resultData);
		}
	}
}