using MediatR;
using VetExpert.Application.Response.Bills;

namespace VetExpert.Application.Commands.Bills
{
	public class CreateBillCommand : IRequest<BillResponse>
	{
		public int Value { get; set; } = 0;

		public string Currency { get; set; } = string.Empty;

		public DateTime DateTime { get; set; } = DateTime.Now;

		public Guid UserId { get; set; }

		public Guid ClinicId { get; set; }
	}
}
