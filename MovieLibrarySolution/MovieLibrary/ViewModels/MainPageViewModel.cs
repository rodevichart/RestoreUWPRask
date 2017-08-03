using MovieLibrary.ApiServices;
using MovieLibrary.Model;

namespace MovieLibrary.ViewModels
{
	public class MainPageViewModel : PropetyChangeBase
	{
		public IAppNavigationService NavigationService { get; set; }

		public MainPageViewModel(IAppNavigationService navigationService)
		{
			NavigationService = navigationService;
		}
	}
}