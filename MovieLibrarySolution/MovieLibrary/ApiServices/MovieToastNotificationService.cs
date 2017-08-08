using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using MovieLibrary.Core;
using MovieLibrary.ViewModels;

namespace MovieLibrary.ApiServices
{
	public class MovieToastNotificationService : IMovieToastNotificationService
	{
		public void GetToastNitification(FilmViewModel filmViewModel)
		{
			var toastTemtpate = ToastTemplateType.ToastImageAndText02;
			var toastXml = ToastNotificationManager.GetTemplateContent(toastTemtpate);

			var toasTextElements = toastXml.GetElementsByTagName("text");
			toasTextElements[0].AppendChild(toastXml.CreateTextNode(filmViewModel.Title));
			toasTextElements[1].AppendChild(toastXml.CreateTextNode(filmViewModel.Director + " " + filmViewModel.ReleaseYear));
				var toastImageElement = toastXml.GetElementsByTagName("image");
			((Windows.Data.Xml.Dom.XmlElement)toastImageElement[0]).SetAttribute("src", filmViewModel.Poster);

			IXmlNode toastNode = toastXml.SelectSingleNode("/toast");
			((Windows.Data.Xml.Dom.XmlElement) toastNode)?.SetAttribute("duration", "long");

			var toast = new ToastNotification(toastXml);
			ToastNotificationManager.CreateToastNotifier().Show(toast);

		}
	}
}