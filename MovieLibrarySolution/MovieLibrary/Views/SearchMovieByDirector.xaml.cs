using MovieLibrary.ViewModels;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MovieLibrary.Views
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class Page2 : Page
	{
		public FilmCollectionViewModel FilmCollection { get; set; }

		public Page2()
		{
			InitializeComponent();
			DataContextChanged += (s, e) =>
			{
				FilmCollection = DataContext as FilmCollectionViewModel;
			};
		}


		private async void DirectorName_OnKeyUp(object sender, KeyRoutedEventArgs e)
		{
			if (e.Key == VirtualKey.Enter)
			{
				MovieDataStkPnl.Visibility = Visibility.Visible;
				await FilmCollection.GetFilmsByDirectorAsync(DirectorName.Text);
			}
		}

	}
}
