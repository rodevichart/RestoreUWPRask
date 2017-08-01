using AutoMapper;
using MovieLibraryBL.DTOs;
using MovieLibraryBL.Persistance;

namespace MovieLibrary.RouteConfigurations
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{	
			CreateMap<RootObject, FilmDto>().ReverseMap();
		}
	}
}