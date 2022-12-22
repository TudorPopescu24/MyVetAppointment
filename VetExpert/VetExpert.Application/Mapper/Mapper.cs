using AutoMapper;
using VetExpert.Application.Commands.Bills;
using VetExpert.Application.Response.Bills;
using VetExpert.Domain;

namespace VetExpert.Application.Mapper
{
	public class Mapper : Profile
	{
		public Mapper() 
		{
			CreateMap<Bill, BillResponse>();
			CreateMap<BillResponse, Bill>();
			CreateMap<Bill, CreateBillCommand>();
			CreateMap<CreateBillCommand, Bill>();
		}
	}
}
