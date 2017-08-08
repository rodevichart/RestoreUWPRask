using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using MovieLibrary.Model;
using MovieLibrary.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MovieLibrary.Views
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MovieDetails : Page
	{
		private FilmViewModel FilmViewModel { get; set; }

		public MovieDetails()
		{
			this.InitializeComponent();
			//FilmViewModel = new FilmViewModel();

		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			FilmViewModel = e.Parameter as FilmViewModel;
		}

	}
}
