namespace MovieLibrary.ApiServices
{
	public interface IAppNavigationService
	{
		void GoToMovieDetailsPage();
		void GoToMovieDetailsPage(object e);
		void GoToMovieCollectionPage();
		void GoToMovieCollectionPage(object e);
		void GoToSearchMovieByDirectorPage();
		void GoToSearchMovieByDirectorPage(object e);
	}
}