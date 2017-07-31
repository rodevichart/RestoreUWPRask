namespace MovieLibraryBL.Core
{
	public interface ILogger
	{
		void WriteError(string message);
		void WriteInfo(string message);
		void WriteWarning(string message);
	}
}