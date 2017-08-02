using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
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


		public FilmCollectionViewModel(IMoviesApiService filmService)
			{
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
		}

		void Movies_OnNotifyPropertyChanged(Object sender, PropertyChangedEventArgs e)
			{
				_filmCollection.Update((FilmViewModel)sender);
			}
		}
	}
	