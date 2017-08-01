using AutoMapper;
using MovieLibraryBL.DTOs;
using MovieLibraryBL.Persistance;

namespace MovieLibrary.RoutingSolutionConfigurations
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{	
			CreateMap<RootObject, FilmDto>().ReverseMap();
		}
	}
}