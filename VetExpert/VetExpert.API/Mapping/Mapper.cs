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

            CreateMap<Admin, CreateAdminDto>();
            CreateMap<CreateAdminDto, Admin>();

            CreateMap<Appointment, CreateAppointmentDto>();
            CreateMap<CreateAppointmentDto, Appointment>();

            CreateMap<Bill, CreateBillDto>();
            CreateMap<CreateBillDto, Bill>();

            CreateMap<Doctor, CreateDoctorDto>();
            CreateMap<CreateDoctorDto, Doctor>();

            CreateMap<Drug, CreateDrugDto>();
            CreateMap<CreateDrugDto, Drug>();
        }
	}
}
