using AutoMapper;
using MovieLibrary.Model;
using MovieLibraryBL.DTOs;

namespace MovieLibrary.RoutingConfigurations
{
	public class PlMappingProfile : Profile
	{
		public PlMappingProfile()
		{
			CreateMap<FilmDto, Film>().ReverseMap();
		}
	}
}