using Windows.UI.Xaml.Controls;
using MovieLibrary.Core;
using MovieLibrary.Views;

namespace MovieLibrary.ApiServices
{
	public class AppNavigationService : IAppNavigationService
	{
		private INavigationService NavigationService { get; set; }

		public AppNavigationService(INavigationService service)
		{
			NavigationService = service;
		}

		public void Initialize(Frame frame)
		{
			NavigationService.Initialize(frame);
		}

		public void GoToMovieDetailsPage()
		{
			NavigationService.Navigate(typeof(MovieDetails));
		}

		public void GoToMovieDetailsPage(object e)
		{
			NavigationService.Navigate(typeof(MovieDetails), e);
		}

		public void GoToMovieCollectionPage()
		{
			NavigationService.Navigate(typeof(MovieCollection));
		}

		public void GoToMovieCollectionPage(object e)
		{
			NavigationService.Navigate(typeof(MovieCollection), e);
		}

		public void GoToSearchMovieByDirectorPage()
		{
			NavigationService.Navigate(typeof(SearchMovieByDirector));
		}

		public void GoToSearchMovieByDirectorPage(object e)
		{
			NavigationService.Navigate(typeof(SearchMovieByDirector), e);
		}

	}
}