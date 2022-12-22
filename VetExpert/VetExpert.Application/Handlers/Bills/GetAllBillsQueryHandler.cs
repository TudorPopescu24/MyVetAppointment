using AutoMapper;
using MediatR;
using VetExpert.Application.Queries.Bills;
using VetExpert.Application.Response.Bills;
using VetExpert.Domain;
using VetExpert.Infrastructure;

namespace VetExpert.Application.Handlers.Bills
{
	public class GetAllBillsQueryHandler : IRequestHandler<GetAllBillsQuery, List<BillResponse>>
	{
		private readonly IRepository<Bill> _repository;
		private readonly IMapper _mapper;

		public GetAllBillsQueryHandler(IRepository<Bill> repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task<List<BillResponse>> Handle(GetAllBillsQuery request, CancellationToken cancellationToken)
		{
			var result = _mapper.Map<List<BillResponse>>(await _repository.GetAll());

			return result;
		}
	}
}
