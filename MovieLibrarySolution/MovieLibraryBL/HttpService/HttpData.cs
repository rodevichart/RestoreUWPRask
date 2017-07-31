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
		public string Url { get; set; }

		public HttpData(string url)
		{
			Url = url;
		}

		public RootObject GetHttpResponeData()
		{
			return  HttpRequest(Url);
		}

		private RootObject HttpRequest(string url)
		{
			
			try
			{
				using (var client = new HttpClient())
				{
					var stream =  client.GetStreamAsync(url).Result;

					
						var serializer = new DataContractJsonSerializer(typeof(RootObject));
						var data = (RootObject)serializer.ReadObject(stream);
						return data;

				}
			}
			catch (Exception e)
			{
				return null;
			}	

		}

		private async Task<RootObjectList> HttpRequestList(string url)
		{
			try
			{
				using (var client = new HttpClient())
				{
					var stream = await client.GetStreamAsync(url);


					var serializer = new DataContractJsonSerializer(typeof(RootObjectList));
					var data = (RootObjectList)serializer.ReadObject(stream);
					return data;

				}
			}
			catch (Exception e)
			{
				return null;
			}

		}

		public async Task<RootObjectList> GetHttpResponeDataListAsync()
		{
			return await HttpRequestList(Url);
		}
	}
}