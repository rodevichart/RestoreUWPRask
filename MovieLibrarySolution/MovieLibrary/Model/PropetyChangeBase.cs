using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MovieLibrary.Model
{
	public class PropetyChangeBase : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] String property = null)
		{
			if (EqualityComparer<T>.Default.Equals(field, value)) return false;
			field = value;
			RaisePropertyChanged(property);
			return true;
		}

		protected bool SetProperty<T>(T currentValue, T newValue, Action doSet,
			[CallerMemberName] String property = null)
		{
			if (EqualityComparer<T>.Default.Equals(currentValue, newValue)) return false;
			doSet.Invoke();
			RaisePropertyChanged(property);
			return true;
		}

		protected void RaisePropertyChanged(string property)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
		}

	}

}