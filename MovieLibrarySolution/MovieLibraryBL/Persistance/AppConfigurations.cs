using System;

namespace MovieLibraryBL.Persistance
{
	public class AppConfigurations
	{
		public byte CacheTimeOutDuration { get; set; }
		public DateTime NextCheck { get; set; }


		public static string ConfigFile = "AppConfig.json";
	}
}