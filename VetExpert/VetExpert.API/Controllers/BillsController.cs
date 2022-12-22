using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;
using VetExpert.Infrastructure;
using VetExpert.Domain;
using VetExpert.API.Dto;
using AutoMapper;
using MediatR;
using VetExpert.Application.Commands.Bills;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using VetExpert.Application.Queries.Bills;
using VetExpert.Application.Response.Bills;

namespace VetExpert.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillsController : ControllerBase
    {

        private readonly IRepository<Bill> _billRepository;
        private readonly IRepository<Drug> _drugRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public BillsController(IRepository<Bill> billRepository,
            IRepository<Drug> drugRepository,
            IMapper mapper,
            IMediator mediator)
        {
            _billRepository = billRepository;
            _drugRepository = drugRepository;
            _mapper = mapper;
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<List<BillResponse>> Get()
        {
            return await _mediator.Send(new GetAllBillsQuery());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBillCommand billCommand)
        {
            var result = await _mediator.Send(billCommand);

            return Ok(result);
        }


        [HttpDelete("{billId:guid}")]
        public async Task<IActionResult> Delete(Guid billId)
        {
            await _mediator.Send(new DeleteBillCommand { BillId = billId });

            return Ok();
        }

        [HttpPut("{billId:guid}")]
        public async Task<IActionResult> Update(Guid billId,
            [FromBody] CreateBillDto billDto)
        {
            var billCommand = _mapper.Map<UpdateBillCommand>(billDto);
            billCommand.BillId = billId;

            var result = await _mediator.Send(billCommand);

            return Ok(result);
        }
    }
}
