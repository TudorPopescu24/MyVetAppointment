using AutoMapper;
using VetExpert.API.Dto;
using VetExpert.Application.Commands.Bills;
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
			CreateMap<UpdateBillCommand, CreateBillDto>();
			CreateMap<CreateBillDto, UpdateBillCommand>();

			CreateMap<Doctor, CreateDoctorDto>();
            CreateMap<CreateDoctorDto, Doctor>();

            CreateMap<Drug, CreateDrugDto>();
            CreateMap<CreateDrugDto, Drug>();

            CreateMap<DrugStock, CreateDrugStockDto>();
            CreateMap<CreateDrugStockDto, DrugStock>();

            CreateMap<Promotion, CreatePromotionDto>();
            CreateMap<CreatePromotionDto, Promotion>();

            CreateMap<Specialization, CreateSpecializationDto>();
            CreateMap<CreateSpecializationDto, Specialization>();

            CreateMap<User, CreateUserDto>();
            CreateMap<CreateUserDto, User>();




        }
    }
}
