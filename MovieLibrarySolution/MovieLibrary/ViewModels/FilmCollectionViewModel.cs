using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using MovieLibrary.Model;
using MovieLibraryBL.Services;
using AutoMapper;
using MovieLibraryBL.DTOs;

namespace MovieLibrary.ViewModels
{
	public class FilmCollectionViewModel: PropetyChangeBase
	{		
			private FilmCollection _filmCollection;
			private ObservableCollection<FilmViewModel> _films = new ObservableCollection<FilmViewModel>();
			private int _selectedIndex;
			
			public IFilmService FilmService { get; set; }

			public ObservableCollection<FilmViewModel> Films
			{
				get => _films;
				set => SetProperty(ref _films, value);
			}

			

			public FilmCollectionViewModel(IFilmService filmService)
			{
				FilmService = filmService;
				_filmCollection = new FilmCollection();
				_selectedIndex = -1;
				
			}

			public async Task<IList<Film>> GetFilmsByDirectorAsync(string director)
			{
				//var service = new FilmService();
				var fillData = await FilmService.GetFilmsByDirector(director);
				return Mapper.Map<IList<FilmDto>, IList<Film>>(fillData);
			}

		public async Task GetAllFilmsAsync(string director)
	        {
				_filmCollection = new FilmCollection(await GetFilmsByDirectorAsync(director));
				_films.Clear();
	            foreach (var movie in _filmCollection.Films)
	            {
	                var nm = new FilmViewModel(movie);
	                nm.PropertyChanged += Movies_OnNotifyPropertyChanged;
	                _films.Add(nm);

	            }
	        }
			

			public int SelectedIndex
			{
				get => _selectedIndex;
				set
				{
					if (SetProperty(ref _selectedIndex, value))
					{
						RaisePropertyChanged(nameof(SelectedFilm));
					}
				}
			}

			public FilmViewModel SelectedFilm => _selectedIndex >= 0 ? _films[_selectedIndex] : null;

			void Movies_OnNotifyPropertyChanged(Object sender, PropertyChangedEventArgs e)
			{
				_filmCollection.Update((FilmViewModel)sender);
			}
		}
	}
	