using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetExpert.Application.Commands.Bills;
using VetExpert.Application.Response.Bills;
using VetExpert.Domain;
using VetExpert.Infrastructure;

namespace VetExpert.Application.Handlers.Bills
{
	public class DeleteBillCommandHandler : IRequestHandler<DeleteBillCommand>
	{
		private readonly IRepository<Bill> _repository;

		public DeleteBillCommandHandler(IRepository<Bill> repository)
		{
			_repository = repository;
		}

		public async Task<Unit> Handle(DeleteBillCommand request, CancellationToken cancellationToken)
		{
			var bill = await _repository.Get(request.BillId);

			if (bill == null)
			{
				throw new Exception($"Could not find bill with id {request.BillId}");
			}

			_repository.Delete(bill);
			await _repository.SaveChangesAsync();

			return Unit.Value;
		}
	}
}
