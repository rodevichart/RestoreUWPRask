using Microsoft.Practices.Unity;
using MovieLibrary.ViewModels;

namespace MovieLibrary.RoutingSolutionConfigurations
{
    public class ViewModelLocator
    {
        private static readonly UnityContainer _unityContainer;

        static ViewModelLocator()
        {
	        _unityContainer = new DiDependencies().RegistrTypes();
        }

        public FilmCollectionViewModel FilmCollectionViewModel => _unityContainer.Resolve<FilmCollectionViewModel>();
		public FilmViewModel FilmViewModel => _unityContainer.Resolve<FilmViewModel>();
    }
}