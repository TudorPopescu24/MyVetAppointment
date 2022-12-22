using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VetExpert.API.Dto;
using VetExpert.Domain;
using VetExpert.Infrastructure;

namespace VetExpert.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrugStocksController : ControllerBase
    {
        private readonly IRepository<Drug> _drugRepository;
        private readonly IRepository<DrugStock> _drugStockRepository;
        private readonly IMapper _mapper;


        public DrugStocksController(IRepository<Drug> drugRepository,
            IRepository<DrugStock> drugStockRepository, IMapper mapper)
        {
            _drugRepository = drugRepository;
            _drugStockRepository = drugStockRepository;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var drugStocks = await _drugRepository.GetAll();
            return Ok(drugStocks);
        }

        [HttpGet("{drugStockId:guid}")]
        public async Task<IActionResult> Get(Guid drugStockId)
        {
            var drugStock = await _drugStockRepository.Get(drugStockId);
            if (drugStock == null)
            {
                return NotFound();
            }

            return Ok(drugStock);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDrugStockDto drugStockDto)
        {

         var drug = await _drugRepository.Get(drugStockDto.DrugId);

            if (drug == null)
            {
                return NotFound();
            }

            var drugStock = _mapper.Map<DrugStock>(drugStockDto);

            await _drugStockRepository.Add(drugStock);
            await _drugStockRepository.SaveChangesAsync();

            return Created(nameof(Get), drugStock);
        }

        [HttpDelete("{drugStockId:guid}")]
        public async Task<IActionResult> Delete(Guid drugStockId)
        {
            var drugStock = await _drugStockRepository.Get(drugStockId);
            if (drugStock == null)
            {
                return NotFound();
            }

            _drugStockRepository.Delete(drugStock);
            await _drugStockRepository.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{drugStockId:guid}")]
        public async Task<IActionResult> Update(Guid drugStockId,
            [FromBody] CreateDrugStockDto drugStockDto)
        {
            var drugStock = await _drugStockRepository.Get(drugStockId);
            if (drugStock == null)
            {
                return NotFound();
            }

            drugStock.Quantity = drugStockDto.Quantity;
            drugStock.ExpirationDate = drugStockDto.ExpirationDate;

            _drugStockRepository.Update(drugStock);
            await _drugStockRepository.SaveChangesAsync();

            return Ok(drugStock);
        }
    }
}
