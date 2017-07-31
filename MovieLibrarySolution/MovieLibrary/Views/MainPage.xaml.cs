using MovieLibrary.Views;
using MovieLibraryBL;
using MovieLibraryBL.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MovieLibrary
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainPage
	{

		public MainPage()
		{
			this.InitializeComponent();
			MainFrame.Navigate(typeof(Page1));
		}

		private void HamburgerButton_OnClick(object sender, RoutedEventArgs e)
		{
			SplitView.IsPaneOpen = !SplitView.IsPaneOpen;
		}

		//private void MenuButton1_Click(object sender, RoutedEventArgs e)
		//{
		//	MainFrame.Navigate(typeof(Page1));
		//}

		//private void MenuButton2_Click(object sender, RoutedEventArgs e)
		//{
		//	MainFrame.Navigate(typeof(Page2));
		//}

		//private void MenuButton3_Click(object sender, RoutedEventArgs e)
		//{
		//	MainFrame.Navigate(typeof(Page3));
		//}


		private void IconsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (MenuButton1.IsSelected) {
				MainFrame.Navigate(typeof(Page1));
			}
			else if (MenuButton2.IsSelected) { MainFrame.Navigate(typeof(Page2)); }
			else if (MenuButton3.IsSelected) { MainFrame.Navigate(typeof(Page3)); }
		}

		private void BackButton_Click(object sender, RoutedEventArgs e)
		{
			if (MainFrame.CanGoBack)
			{
				MainFrame.GoBack();
			}
		}

		private void ForwordButton_Click(object sender, RoutedEventArgs e)
		{
			if (MainFrame.CanGoForward)
			{
				MainFrame.GoForward();
			}
		}
	}

}
