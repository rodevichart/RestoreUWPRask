using AutoMapper;
using MovieLibraryBL.DTOs;
using MovieLibraryBL.Persistance;

namespace MovieLibraryBL
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{	
			CreateMap<RootObject, FilmDto>().ReverseMap();
		}
	}
}