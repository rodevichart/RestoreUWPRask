using Microsoft.Practices.Unity;
using MovieLibrary.ApiServices;
using MovieLibrary.Core;
using MovieLibrary.ViewModels;
using MovieLibraryBL.Core;
using MovieLibraryBL.HttpService;
using MovieLibraryBL.Services;

namespace MovieLibrary.RoutingConfigurations
{
	public class DiDependencies
	{
		public UnityContainer UnityContainer { get; set; }

		public UnityContainer RegistrTypes()
		{
			UnityContainer = new UnityContainer();
			UnityContainer.RegisterType<IFilmService, FilmService>();
			UnityContainer.RegisterType<IMoviesApiService, MoviesApiService>();
			UnityContainer.RegisterType<IHttpService, HttpService>();
			UnityContainer.RegisterType<IAppNavigationService, AppNavigationService>();
			UnityContainer.RegisterType<INavigationService, NavigationService>();
			UnityContainer.RegisterType<FilmCollectionViewModel>();
			UnityContainer.RegisterType<FilmViewModel>();

			return UnityContainer;
		}
	}
}