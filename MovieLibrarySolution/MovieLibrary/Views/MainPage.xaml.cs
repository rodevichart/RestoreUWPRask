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
using Windows.UI.Core;
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
		
				MainFrame.Navigated += OnNavigated;

				// Register a handler for BackRequested events and set the
				// visibility of the Back button
				SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;
				SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
					MainFrame.CanGoBack ?
					AppViewBackButtonVisibility.Visible :
					AppViewBackButtonVisibility.Collapsed;
			
			//SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
		}

		private void OnBackRequested(object sender, BackRequestedEventArgs e)
		{

			if (MainFrame.CanGoBack)
			{
				e.Handled = true;
				MainFrame.GoBack();
			}
		}

		private void HamburgerButton_OnClick(object sender, RoutedEventArgs e)
		{
			SplitView.IsPaneOpen = !SplitView.IsPaneOpen;
		}


		private void IconsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var sendFrom = sender as ListBox;

			if(sendFrom.Name == "IconsListBox")
			{
				BottomIconsListBox.SelectionChanged -= IconsListBox_SelectionChanged;
				BottomIconsListBox.SelectedIndex = -1;

				if (MenuButton1.IsSelected)
				{
					MainFrame.Navigate(typeof(Page1));
					RelativePanTitle.Text = Share.Text;
				}
				else if (MenuButton2.IsSelected)
				{
					MainFrame.Navigate(typeof(Page2));
					RelativePanTitle.Text = Movie.Text;
				}
				else if (MenuButton3.IsSelected)
				{
					MainFrame.Navigate(typeof(Page3));
					RelativePanTitle.Text = Cortana.Text;
				}
				BottomIconsListBox.SelectionChanged += IconsListBox_SelectionChanged;
			}
			else
			{
				IconsListBox.SelectionChanged -= IconsListBox_SelectionChanged;
				IconsListBox.SelectedIndex = -1;

				if (SettingsBtn.IsSelected)
				{
					MainFrame.Navigate(typeof(Page3));
					RelativePanTitle.Text = Settings.Text;
				}
				else if (SignInBtn.IsSelected)
				{
					MainFrame.Navigate(typeof(Page3));
					RelativePanTitle.Text = SignIn.Text;
				}
				IconsListBox.SelectionChanged += IconsListBox_SelectionChanged;
			}

			
			
		}


		private void OnNavigated(object sender, NavigationEventArgs e)
		{
			SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
				((Frame)sender).CanGoBack ?
				AppViewBackButtonVisibility.Visible :
				AppViewBackButtonVisibility.Collapsed;
		}

	}

}
