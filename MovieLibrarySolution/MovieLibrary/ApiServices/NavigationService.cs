using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using MovieLibrary.Core;

namespace MovieLibrary.ApiServices
{
	public class NavigationService : INavigationService
	{

		protected static Frame MainFrame
		{
			get { return (Window.Current.Content as Frame); }

		}

		public event NavigatingCancelEventHandler Navigating;
		
		public void Navigate(Type type)
		{
			MainFrame.Navigate(type);
		}

		public void Navigate(Type type, object parameter)
		{
			MainFrame.Navigate(type, parameter);
		}

		public void Navigate(string type, object parameter)
		{
			MainFrame.Navigate(Type.GetType(type), parameter);
		}

		public void Navigate(string type)
		{
			MainFrame.Navigate(Type.GetType(type));
		}

		public void GoBack()
		{
			if (MainFrame.CanGoBack)
			{
				MainFrame.GoBack();
			}
		}
	}
}