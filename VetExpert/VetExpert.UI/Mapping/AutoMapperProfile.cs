﻿using AutoMapper;
using VetExpert.Domain;
using VetExpert.UI.Dto;

namespace VetExpert.UI.Mapping
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile() 
		{
			CreateMap<CreateUserDto, User>();
			CreateMap<User, CreateUserDto>();
        }
    }
}