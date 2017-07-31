using AutoMapper;
using MovieLibrary.Model;
using MovieLibraryBL.DTOs;

namespace MovieLibrary.RouteConfigurations
{
	public class PlMappingProfile : Profile
	{
		public PlMappingProfile()
		{
			CreateMap<FilmDto, Film>().ReverseMap();
		}
	}
}