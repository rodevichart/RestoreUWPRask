using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Notifications;
using MovieLibrary.ApiServices;
using MovieLibrary.Model;
using MovieLibrary.Core;

namespace MovieLibrary.ViewModels
{
	public class FilmCollectionViewModel: PropetyChangeBase
	{		
			private FilmCollection _filmCollection;
			private ObservableCollection<FilmViewModel> _films = new ObservableCollection<FilmViewModel>();
			private int _selectedIndex;
			
			public IMoviesApiService MovieApiService { get; set; }
			public IAppNavigationService NavigationService { get; set; }

			public ObservableCollection<FilmViewModel> Films
			{
				get { return _films; }
				set { SetProperty(ref _films, value); }
			}


		public int SelectedIndex
		{
			get { return _selectedIndex; }
			set
			{
				if (SetProperty(ref _selectedIndex, value))
				{
					RaisePropertyChanged(nameof(SelectedFilm));
				}
			}
		}

		public FilmViewModel SelectedFilm
		{
			get { return _selectedIndex >= 0 ? _films[_selectedIndex] : null; }
		}


		public FilmCollectionViewModel(IMoviesApiService filmService, IAppNavigationService navigationService)
		{
				NavigationService = navigationService;
				MovieApiService = filmService;
				_filmCollection = new FilmCollection();
				_selectedIndex = -1;


	    }

			

		public async Task GetFilmsByDirectorAsync(string director)
	        {
				_filmCollection = new FilmCollection(await MovieApiService.GetFilmsByDirectorAsync(director));
				_films.Clear();
	            foreach (var movie in _filmCollection.Films)
	            {
	                var nm = new FilmViewModel(movie);
	                nm.PropertyChanged += Movies_OnNotifyPropertyChanged;
	                _films.Add(nm);

	            }
	        }

		public async Task GetFilmsByNDirectorAsync()
		{
			_filmCollection = new FilmCollection(await MovieApiService.GetFilmsByNDirectorAsync());
			foreach (var movie in _filmCollection.Films)
			{
				var nm = new FilmViewModel(movie);
				nm.PropertyChanged += Movies_OnNotifyPropertyChanged;
				_films.Add(nm);

			}
			UpdateLiveTile();
		}

		private void UpdateLiveTile()
		{
			var tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare150x150Text03);
			var tileTextAttributes = tileXml.GetElementsByTagName("test");

			try
			{
				var film = (_films.ElementAt(0));
				tileTextAttributes[0].InnerText = film.Title;
				film = (_films.ElementAt(1));
				tileTextAttributes[1].InnerText = film.Title;



				var wideTileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150BlockAndText02);
				var wideTileTextAttributes = tileXml.GetElementsByTagName("test");

				film = (_films.ElementAt(0));
				wideTileTextAttributes[0].InnerText = film.Title;
				film = (_films.ElementAt(1));
				wideTileTextAttributes[1].InnerText = film.Title;

				var node = tileXml.ImportNode(wideTileXml.GetElementsByTagName("binding").Item(0), true);
				tileXml.GetElementsByTagName("visual").Item(0).AppendChild(node);

				var tileNotification = new TileNotification(tileXml);
				tileNotification.ExpirationTime = DateTimeOffset.Now.AddDays(3);
				TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);
			}
			catch (Exception e)
			{
			}
			
		}


		private void Movies_OnNotifyPropertyChanged(Object sender, PropertyChangedEventArgs e)
			{
				_filmCollection.Update((FilmViewModel)sender);
			}
		}
	}
	