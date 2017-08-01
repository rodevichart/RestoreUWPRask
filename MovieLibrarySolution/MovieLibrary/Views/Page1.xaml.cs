using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MovieLibrary.Model;
using MovieLibrary.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MovieLibrary.Views
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class Page1
	{
		public FilmCollectionViewModel FilmCollection { get; set; }
		private FilmViewModel FilmViewModel { get; set; }

		public Page1()
		{
			this.InitializeComponent();

			DataContextChanged += (s, e) =>
			{
				FilmCollection = DataContext as FilmCollectionViewModel;
			};
		}

		private async void DirectorName_OnKeyUp(object sender, KeyRoutedEventArgs e)
		{
			if (e.Key == VirtualKey.Enter)
			{
				await FilmCollection.GetAllFilmsAsync(DirectorName.Text);
			}
		}

		//private void ListViewBase_OnItemClick(object sender, ItemClickEventArgs e)
		//{
		//	throw new NotImplementedException();
		//}

		//protected override void OnNavigatedTo(NavigationEventArgs e)
		//{


		//}

		private void ListViewBase_OnItemClick(object sender, ItemClickEventArgs e)
		{

			Frame.Navigate(typeof(Page3), e.ClickedItem);
		}

		//protected override void OnNavigatedTo(NavigationEventArgs e)
		//{
		//	if (e.Parameter != null)
		//	{
		//		FilmViewModel = e.Parameter;
		//		Movies = new MovieCollectionViewModel(_param);
		//	}
		//}
	}
}
