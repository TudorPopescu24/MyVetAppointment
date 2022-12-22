using MediatR;
using VetExpert.Application.Response.Bills;

namespace VetExpert.Application.Queries.Bills
{
	public class GetAllBillsQuery : IRequest<List<BillResponse>>
	{
	}
}
