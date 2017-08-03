using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MovieLibraryBL.DTOs;
using MovieLibraryBL.Persistance;
using MovieLibraryBL.Core;
using System.Net.Http;
using System.Runtime.Serialization.Json;

namespace MovieLibraryBL.Services
{
	public class FilmService : IFilmService
	{
		private const string Url = "https://netflixroulette.net/api/api.php?";

		private IHttpService HttpService { get; }

		public FilmService(IHttpService httpService)
		{
			HttpService = httpService;
		}

		//public FilmDto GetFilmsByTitleNYear(string title, string year)
		//{
		//	var convYear = Convert.ToInt32(year);

		//	var stringBuilder = new StringBuilder(Url).AppendFormat(@"title={0}", title.Replace(" ", "%20"));
		//	var url = (convYear > 0 ? stringBuilder.AppendFormat("&year={0}", year) : stringBuilder).ToString();

		//	var data = new HttpService(url).GetHttpResponeData();

		//	var result = Mapper.Map<MovieData, FilmDto>(data);

		//	return result;

		//}

		public async Task<IList<FilmDto>> GetFilmsByDirector(string director)
		{
			var result = new List<FilmDto>();

			var url = GetUrl(Url, ApiRoutingConsts.Director, director);

			var stream = await GetStream(url);

			var data = (MovieDataList) new DataContractJsonSerializer(typeof(MovieDataList)).ReadObject(stream);

			foreach (var d in data)
			{
				var dto = Mapper.Map<MovieData, FilmDto>(d);
				result.Add(dto);
			}


			return result;
		}

		private string GetUrl(string urlApi, string apiRoutingConsts, string param)
		{
			return Uri.EscapeUriString($"{urlApi}{apiRoutingConsts}={param}");
		}

		private async Task<Stream> GetStream(string url)
		{
			var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, url);

			var respone = await HttpService.GetHttpResponeDataListAsync(httpRequestMessage);

			var stream = await respone.Content.ReadAsStreamAsync();

			return stream;
		}

	}
}
