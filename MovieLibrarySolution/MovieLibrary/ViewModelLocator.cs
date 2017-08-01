using Microsoft.Practices.Unity;
using MovieLibrary.ApiServices;
using MovieLibrary.Core;
using MovieLibrary.ViewModels;
using MovieLibraryBL.Core;
using MovieLibraryBL.Services;

namespace MovieLibrary
{
    public class ViewModelLocator
    {
        private static readonly UnityContainer _unityContainer;

        static ViewModelLocator()
        {
	        _unityContainer = new DiDependencies().RegistrTypes();
        }

        public FilmCollectionViewModel FilmCollectionViewModel => _unityContainer.Resolve<FilmCollectionViewModel>();
    }
}