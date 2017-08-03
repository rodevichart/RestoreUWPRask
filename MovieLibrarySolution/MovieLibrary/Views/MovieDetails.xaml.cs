using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using MovieLibrary.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MovieLibrary.Views
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class Page3 : Page
	{
		private FilmViewModel FilmViewModel { get; set; }

		public Page3()
		{
			this.InitializeComponent();
			FilmViewModel = new FilmViewModel();

		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			FilmViewModel = e.Parameter as FilmViewModel;
		}

	}
}
