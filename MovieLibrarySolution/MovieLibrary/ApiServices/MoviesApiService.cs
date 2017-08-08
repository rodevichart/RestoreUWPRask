using AutoMapper;
using MovieLibrary.Core;
using MovieLibrary.Model;
using MovieLibraryBL.Core;
using MovieLibraryBL.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.ApiServices
{

	public class MoviesApiService : IMoviesApiService
	{
		public IFilmService FilmService { get; set; }

		public IMovieSqlHelper SqlHelper { get; set; }

		public MoviesApiService(IFilmService filmService, IMovieSqlHelper movieSqlHelper)
		{
			SqlHelper = movieSqlHelper;
			FilmService = filmService;
		}


		public async Task<IList<Film>> GetFilmsByDirectorAsync(string director)
		{
			
			var sqlData = await GetSqlDataByDirector(director);

			if (sqlData.Any())
			{
				return sqlData;
			}

			var fillData = await FilmService.GetFilmsByDirector(director);
			
			var result = Mapper.Map<IList<FilmDto>, IList<Film>>(fillData);

			SqlHelper.AddFilmsData(result);
			return result;
		}

		public async Task<IList<Film>> GetFilmsByNDirectorAsync()
		{
			var sqlData = await GetSqlData();

			if (sqlData.Any())
			{
				return sqlData;
			}

			var fillData = new List<FilmDto>();
			foreach (var director in GetDirectors())
			{
				fillData.AddRange(await FilmService.GetFilmsByDirector(director));
			}

			var result = Mapper.Map<IList<FilmDto>, IList<Film>>(fillData);

			SqlHelper.AddFilmsData(result);

			return result;
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

		private async Task<IList<Film>> GetSqlData()
		{
			return await SqlHelper.GetFilmsDataAsync();
		}


		private async Task<IList<Film>> GetSqlDataByDirector(string director)
		{
			return await SqlHelper.GetFilmsDataByDirectorAsync(director);
		}
	}
}
