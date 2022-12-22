using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VetExpert.API.Dto;
using VetExpert.Application.Commands.Bills;
using VetExpert.Application.Queries.Bills;
using VetExpert.Application.Response.Bills;

namespace VetExpert.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public BillsController(
            IMapper mapper,
            IMediator mediator)
        {
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
