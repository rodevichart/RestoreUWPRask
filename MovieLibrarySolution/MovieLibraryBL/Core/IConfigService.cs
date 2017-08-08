using System.Threading.Tasks;
using MovieLibraryBL.Persistance;

namespace MovieLibraryBL.Core
{
	public interface IConfigService
	{
		Task<AppConfigurations> GetCacheTimeoutSerializeAsync();
		void WriteCacheTimeoutSerializeAsync(AppConfigurations appConfigurations);
	}
}