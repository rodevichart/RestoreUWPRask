using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using MovieLibraryBL.Core;
using MovieLibraryBL.Persistance;
using Newtonsoft.Json;

namespace MovieLibraryBL.Services
{
	public class ConfigService : IConfigService
	{
		public async Task<AppConfigurations> GetCacheTimeoutSerializeAsync()
		{
			var sampleFile = await GetStorageFileAsync(AppConfigurations.ConfigFile);
			var text = await FileIO.ReadTextAsync(sampleFile);
			var result = (AppConfigurations)JsonConvert.DeserializeObject(text, typeof(AppConfigurations));
			return result;


		}

		public async void WriteCacheTimeoutSerializeAsync(AppConfigurations appConfigurations)
		{
			var sampleFile = await GetStorageFileAsync(AppConfigurations.ConfigFile);
			var stream = await sampleFile.OpenAsync(Windows.Storage.FileAccessMode.ReadWrite);
			using (var outputStream = stream.GetOutputStreamAt(0))
			{
				using (var dataWriter = new Windows.Storage.Streams.DataWriter(outputStream))
				{ 
					var json = JsonConvert.SerializeObject(appConfigurations);
					dataWriter.WriteString(json);
					await dataWriter.StoreAsync();
					await outputStream.FlushAsync();
				}

			}
			stream.Dispose(); 
		}

		private async Task<StorageFile> GetStorageFileAsync(string file)
		{
			StorageFolder storageFolder =
				ApplicationData.Current.LocalFolder;
			
			return	await storageFolder.GetFileAsync(file);
		}


	}
}