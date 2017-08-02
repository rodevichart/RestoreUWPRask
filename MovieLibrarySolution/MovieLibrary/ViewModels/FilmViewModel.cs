using MovieLibrary.Model;

namespace MovieLibrary.ViewModels
{
	#region Simple viewmodel

	//public class FilmViewModel : INotifyPropertyChanged
	//{
	//	public event PropertyChangedEventHandler PropertyChanged;

	//	private string _title;
	//	private string _releaseYear;
	//	private string _director;
	//	private string _poster;
	//	private int _selectedIndex;
	//	private string _duration;


	//	public string Title
	//	{
	//		get { return _title; }
	//		set
	//		{
	//			_title = value;
	//			OnPropertyChanged();
	//		}
	//	}



	//	public string ReleaseYear
	//	{
	//		get { return _releaseYear; }
	//		set
	//		{
	//			_releaseYear = value;
	//			OnPropertyChanged();
	//		}
	//	}



	//	public string Director
	//	{
	//		get { return _director; }
	//		set
	//		{
	//			_director = value;
	//			OnPropertyChanged();
	//		}
	//	}


	//	public string Poster
	//	{
	//		get { return _poster; }
	//		set
	//		{
	//			_poster = value;
	//			OnPropertyChanged();
	//		}
	//	}

	//	public string Duration
	//	{
	//		get { return _poster; }
	//		set
	//		{
	//			_poster = value;
	//			OnPropertyChanged();
	//		}
	//	}



	//	public int SelectedIndex
	//	{
	//		get { return _selectedIndex; }
	//		set
	//		{
	//			_selectedIndex = value;
	//			if ()
	//		}
	//	}




	//	public FakeData filmColl { get; set; }

	//	public FilmViewModel()
	//	{
	//		_selectedIndex = -1;
	//		filmColl = new FakeData();
	//		foreach (var film in filmColl.GetData())
	//		{
	//			Add(new Film
	//			{
	//				Title = "Title " + film.Title,
	//				Duration = "Duration " + film.Duration,
	//				Director = "Director " + film.Director,
	//				ReleaseYear = "ReleaseYear " + film.ReleaseYear
	//			});
	//		}
	//		//Add(new Film
	//		//{
	//		//	Title = "Title",
	//		//	Director = "Director",
	//		//	ReleaseYear = "ReleaseYear"
	//		//});


	//	}

	//	public FilmViewModel GetViewModel(Film film = null)
	//	{
	//		return (film == null) ? new FilmViewModel() : new FilmViewModel
	//		{
	//			Title = film.Title,
	//			Duration = film.Duration,
	//			Director = film.Director,
	//			ReleaseYear = film.ReleaseYear
	//		};
	//	}



	//	protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
	//	{
	//		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	//	}
	//}


	#endregion

	public class FilmViewModel : PropetyChangeBaseGeneric<Film>
	{


		public FilmViewModel(Film film = null)
			:base(film)
		{	
		}

		
		public string Title
		{
			get { return This.Title; }
			set
			{
				SetProperty(This.Title, value,
					() => This.Title = value);
			}
		}



		public string ReleaseYear
		{
			get { return This.ReleaseYear; }
			set
			{
				SetProperty(This.ReleaseYear, value,
					() => This.ReleaseYear = value);
			}
		}



		public string Director
		{
			get { return This.Director; }
			set
			{
				SetProperty(This.Director, value,
					() => This.Director = value);
			}
		}


		public string Poster
		{
			get { return This.Poster; }
			set
			{
				SetProperty(This.Poster, value,
					() => This.Poster = value);
			}
		}

		public string Duration
		{
			get { return This.Duration; }
			set
			{
				SetProperty(This.Duration, value,
					() => This.Duration = value);
			}
		}

	}
}