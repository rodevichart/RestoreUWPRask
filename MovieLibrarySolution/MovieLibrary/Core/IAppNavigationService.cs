using Windows.UI.Xaml.Controls;

namespace MovieLibrary.Core
{
	public interface IAppNavigationService
	{
		void Initialize(Frame frame);
		void GoToMovieDetailsPage();
		void GoToMovieDetailsPage(object e);
		void GoToMovieCollectionPage();
		void GoToMovieCollectionPage(object e);
		void GoToSearchMovieByDirectorPage();
		void GoToSearchMovieByDirectorPage(object e);

	}
}