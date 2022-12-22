using AutoMapper;
using MediatR;
using VetExpert.Application.Commands.Bills;
using VetExpert.Application.Response.Bills;
using VetExpert.Domain;
using VetExpert.Infrastructure;

namespace VetExpert.Application.Handlers.Bills
{
	public class UpdateBillCommandHandler : IRequestHandler<UpdateBillCommand, BillResponse>
	{
		private readonly IRepository<Bill> _repository;
		private readonly IMapper _mapper;

		public UpdateBillCommandHandler(IRepository<Bill> repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}
		public async Task<BillResponse> Handle(UpdateBillCommand request, CancellationToken cancellationToken)
		{
			var bill = await _repository.Get(request.BillId);

			if (bill == null)
			{
				throw new Exception($"Could not find bill with id {request.BillId}");
			}

			bill.Value = request.Value;
			bill.Currency = request.Currency;
			bill.DateTime = request.DateTime;

			_repository.Update(bill);
			await _repository.SaveChangesAsync();

			return _mapper.Map<BillResponse>(bill);
		}
	}
}
