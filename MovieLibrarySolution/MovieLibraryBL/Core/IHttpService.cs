using System.Net.Http;
using System.Threading.Tasks;

namespace MovieLibraryBL.Core
{
	public interface IHttpService
	{
		Task<HttpResponseMessage> GetHttpResponeDataListAsync(HttpRequestMessage url);
	}

}