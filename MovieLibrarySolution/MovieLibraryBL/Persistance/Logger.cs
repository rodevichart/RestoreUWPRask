using System;
using Windows.Storage;
using MovieLibraryBL.Core;

namespace MovieLibraryBL.Persistance
{
	public class Logger:ILogger
	{
		private const string Path = "Log.txt";

		public void WriteError(string message)
		{
			WriteToFile($"Error {message}");
		}

		public void WriteInfo(string message)
		{
			WriteToFile($"Info {message}");
		}

		public void WriteWarning(string message)
		{
			WriteToFile($"Warning {message}");
		}

		private async void WriteToFile(string writeMsg)
		{
			StorageFolder localFolder = ApplicationData.Current.LocalFolder;
			StorageFile errorFile = await localFolder.CreateFileAsync(Path,
				CreationCollisionOption.OpenIfExists);
			await FileIO.AppendTextAsync(errorFile, writeMsg);
		}
	}
}