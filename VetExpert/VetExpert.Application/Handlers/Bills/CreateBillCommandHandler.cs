using AutoMapper;
using MediatR;
using VetExpert.Application.Commands.Bills;
using VetExpert.Application.Response.Bills;
using VetExpert.Domain;
using VetExpert.Infrastructure;

namespace VetExpert.Application.Handlers.Bills
{
	public class CreateBillCommandHandler : IRequestHandler<CreateBillCommand, BillResponse>
	{
		private readonly IRepository<Bill> _repository;
		private readonly IMapper _mapper;

		public CreateBillCommandHandler(IRepository<Bill> repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}
		public async Task<BillResponse> Handle(CreateBillCommand request, CancellationToken cancellationToken)
		{
			var bill = _mapper.Map<Bill>(request);

			if (bill == null)
			{
				throw new ApplicationException("Issue with the mapper");
			}

			await _repository.Add(bill);
			await _repository.SaveChangesAsync();

			return _mapper.Map<BillResponse>(bill);
		}
	}
}
