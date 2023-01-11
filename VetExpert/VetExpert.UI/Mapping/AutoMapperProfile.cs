using AutoMapper;
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

            CreateMap<CreateClinicDto, Clinic>();
            CreateMap<Clinic, CreateClinicDto>();

			CreateMap<CreateDoctorDto, Doctor>();
			CreateMap<Doctor, CreateDoctorDto>();
		}
    }
}
