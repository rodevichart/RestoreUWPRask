using Microsoft.Practices.Unity;
using MovieLibrary.ViewModels;
using MovieLibraryBL.Core;
using MovieLibraryBL.Services;

namespace MovieLibrary
{
    public class UnitiDIConfig
    {
        private IUnityContainer _container;
        

        private void RegesterDI()
        {
            _container = new UnityContainer();
        }

        public void Setup()
        {
            RegisterViewModels();
            RegisterServices();
        }

        private void RegisterServices()
        {
//            _container.RegisterType<ILogger, Logger>();
            _container.RegisterType<IFilmService, FilmService>();

        }


        private void RegisterViewModels()
        {
            _container.RegisterType<FilmCollectionViewModel>();
            _container.RegisterType<FilmViewModel>();
        }

        //		public void GetLogger()
        //		{
        //			 _container.Resolve<ILogger>();
        //		}

        public ILogger GetLoggerInstance()
        {
            RegesterDI();
            return _container.Resolve<ILogger>();
        }
    }
}