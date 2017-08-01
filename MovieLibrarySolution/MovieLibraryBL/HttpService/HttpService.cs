using System;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using MovieLibraryBL.Core;
using MovieLibraryBL.Persistance;

namespace MovieLibraryBL.HttpService
{
	public class HttpService : IHttpService
	{

		//public MovieData GetHttpResponeData()
		//{
		//	return  HttpRequest(Url);
		//}

		//private MovieData HttpRequest(string url)
		//{
			
		//	try
		//	{
		//		using (var client = new HttpClient())
		//		{
		//			var stream =  client.GetStreamAsync(url).Result;

					
		//				var serializer = new DataContractJsonSerializer(typeof(MovieData));
		//				var data = (MovieData)serializer.ReadObject(stream);
		//				return data;

		//		}
		//	}
		//	catch (Exception e)
		//	{
		//		return null;
		//	}	

		//}

		public async Task<HttpResponseMessage> GetHttpResponeDataListAsync(HttpRequestMessage url)
		{
			try
			{
				using (var client = new HttpClient())
				{
					var response = await client.SendAsync(url);
					return response;
				}
			}
			catch (Exception e)
			{
				return null;
			}

		}
	}
}