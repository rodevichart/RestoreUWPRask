using System;
using Windows.System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using MovieLibrary.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MovieLibrary.Views
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MovieCollection
	{
		public FilmCollectionViewModel FilmCollection { get; set; }
		

		public MovieCollection()
		{
			DataContextChanged += (s, e) =>
			{
				FilmCollection = DataContext as FilmCollectionViewModel;
			};

	
			this.InitializeComponent();

			
		}

		protected override async void OnNavigatedTo(NavigationEventArgs e)
		{
			await FilmCollection.GetFilmsByNDirectorAsync();
		}

		

		private void ListViewBase_OnItemClick(object sender, ItemClickEventArgs e)
		{

			//Frame.Navigate(typeof(MovieDetails), e.ClickedItem);
			FilmCollection.NavigationService.GoToMovieDetailsPage(e.ClickedItem);
			//FilmCollection.NavigationService.Navigate(typeof(MovieDetails));
		}


	}
}
