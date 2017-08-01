using System;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using MovieLibraryBL.Core;
using MovieLibraryBL.Persistance;

namespace MovieLibraryBL.HttpService
{
	public class HttpData
	{
		public HttpRequestMessage Url { get; set; }

		public HttpData(HttpRequestMessage url)
		{
			Url = url;
		}

		//public RootObject GetHttpResponeData()
		//{
		//	return  HttpRequest(Url);
		//}

		//private RootObject HttpRequest(string url)
		//{
			
		//	try
		//	{
		//		using (var client = new HttpClient())
		//		{
		//			var stream =  client.GetStreamAsync(url).Result;

					
		//				var serializer = new DataContractJsonSerializer(typeof(RootObject));
		//				var data = (RootObject)serializer.ReadObject(stream);
		//				return data;

		//		}
		//	}
		//	catch (Exception e)
		//	{
		//		return null;
		//	}	

		//}

		private async Task<HttpResponseMessage> HttpRequestList(HttpRequestMessage url)
		{
			try
			{
				using (var client = new HttpClient())
				{
					var stream = await client.SendAsync(url);
					return stream;
				}
			}
			catch (Exception e)
			{
				return null;
			}

		}

		public async Task<HttpResponseMessage> GetHttpResponeDataListAsync()
		{
			return await HttpRequestList(Url);
		}
	}
}