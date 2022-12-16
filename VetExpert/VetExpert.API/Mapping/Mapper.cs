using AutoMapper;
using VetExpert.API.Dto;
using VetExpert.Domain;

namespace VetExpert.API.Mapping
{
	public class Mapper : Profile
	{
		public Mapper() 
		{
			CreateMap<Clinic, CreateClinicDto>();
			CreateMap<CreateClinicDto, Clinic>();
		}
	}
}
