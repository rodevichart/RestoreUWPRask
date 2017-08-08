using MovieLibrary.ViewModels;

namespace MovieLibrary.Core
{
	public interface IMovieToastNotificationService
	{
		void GetToastNitification(FilmViewModel filmViewModel);
	}
}