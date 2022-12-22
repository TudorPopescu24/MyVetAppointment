using MediatR;

namespace VetExpert.Application.Commands.Bills
{
	public class DeleteBillCommand : IRequest
	{
		public Guid BillId { get; set; }
	}
}
