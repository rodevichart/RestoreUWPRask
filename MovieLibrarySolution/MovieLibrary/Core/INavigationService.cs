using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace MovieLibrary.Core
{
	public interface INavigationService
	{
		void Initialize(Frame frame);
		void Navigate(Type type);
		void Navigate(Type type, object parameter);
		void Navigate(string type);
		void Navigate(string type, object parameter);
		void GoBack();
	}
}