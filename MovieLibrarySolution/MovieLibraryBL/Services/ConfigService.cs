using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Storage;
using Windows.UI.Popups;
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
			try
			{
				var sampleFile = await GetStorageFileAsync(ApiRoutingConsts.ConfigFile);
				var text = await FileIO.ReadTextAsync(sampleFile);
				var result = (AppConfigurations)JsonConvert.DeserializeObject(text, typeof(AppConfigurations));
				return result;
			}
			catch (FileNotFoundException)
			{
				var dialog = new MessageDialog("Couldn`t find AppConfig.josn\nApp close.");
				await dialog.ShowAsync();
				Application.Current.Exit();
//				CoreApplication.Exit();
			}
			return null;
		}

//		public async Task WriteCacheSerializeAsync(AppConfigurations appConfigurations)
//		{
//			var sampleFile = await GetStorageFileAsync(ApiRoutingConsts.ConfigFile);
//			var stream = await sampleFile.OpenAsync(FileAccessMode.ReadWrite);
//			using (var outputStream = stream.GetOutputStreamAt(0))
//			{
//				using (var dataWriter = new Windows.Storage.Streams.DataWriter(outputStream))
//				{ 
//					var json = JsonConvert.SerializeObject(appConfigurations);
//					dataWriter.WriteString(json);
//					await dataWriter.StoreAsync();
//					await outputStream.FlushAsync();
//				}
//
//			}
//			stream.Dispose(); 
//		}

		private async Task<StorageFile> GetStorageFileAsync(string path)
		{
			var storageFolder = ApplicationData.Current.LocalFolder;
			
			var file = await storageFolder.GetFileAsync(path);

			return file;
		}


	}
}