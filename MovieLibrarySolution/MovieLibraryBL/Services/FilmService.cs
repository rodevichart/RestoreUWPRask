using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MovieLibraryBL.DTOs;
using MovieLibraryBL.HttpService;
using MovieLibraryBL.Persistance;

namespace MovieLibraryBL.Services
{
	public class FilmService : IFilmService
	{
		private const string Url = "https://netflixroulette.net/api/api.php?";

		public FilmDto GetFilmsByTitleNYear(string title, string year)
		{
			var convYear = Convert.ToInt32(year);

			var stringBuilder = new StringBuilder(Url).AppendFormat(@"title={0}", title.Replace(" ", "%20"));
			var url = (convYear > 0 ? stringBuilder.AppendFormat("&year={0}", year) : stringBuilder).ToString();

			var data = new HttpData(url).GetHttpResponeData();

			var result = Mapper.Map<RootObject, FilmDto>(data);

			return result;

		}

		public async Task<IList<FilmDto>> GetFilmsByDirector(string director)
		{
			var result = new List<FilmDto>();

			var url = new StringBuilder(Url).AppendFormat(@"director={0}", director.Replace(" ", "%20")).ToString();

			var data = await new HttpData(url).GetHttpResponeDataListAsync();

			foreach (var d in data)
			{
				var dto = Mapper.Map<RootObject, FilmDto>(d);
				result.Add(dto);
			}


			return result;
		}



	}


}
