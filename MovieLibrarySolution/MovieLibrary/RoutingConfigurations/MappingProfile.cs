﻿using AutoMapper;
using MovieLibraryBL.DTOs;
using MovieLibraryBL.Persistance;

namespace MovieLibrary.RoutingConfigurations
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{	
			CreateMap<MovieData, FilmDto>().ReverseMap();
		}
	}
}