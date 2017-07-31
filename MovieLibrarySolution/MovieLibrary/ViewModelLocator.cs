using Microsoft.Practices.Unity;
using MovieLibrary.ViewModels;
using MovieLibraryBL.Services;

namespace MovieLibrary
{
    public class ViewModelLocator
    {
        private static readonly UnityContainer _unityContainer;

        static ViewModelLocator()
        {
            _unityContainer = new UnityContainer();
            _unityContainer.RegisterType<IFilmService, FilmService>();
            _unityContainer.RegisterType<FilmCollectionViewModel>();
            _unityContainer.RegisterType<FilmViewModel>();
        }

        public FilmCollectionViewModel FilmCollectionViewModel => _unityContainer.Resolve<FilmCollectionViewModel>();
    }
}