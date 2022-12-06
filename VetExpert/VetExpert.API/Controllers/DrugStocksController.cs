using Microsoft.AspNetCore.Http;
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

        public DrugStocksController(IRepository<Drug> drugRepository,
            IRepository<DrugStock> drugStockRepository)
        {
            _drugRepository = drugRepository;
            _drugStockRepository = drugStockRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_drugStockRepository.GetAll());
        }

        [HttpGet("{drugStockId:guid}")]
        public IActionResult Get(Guid drugStockId)
        {
            var drugStock = _drugStockRepository.Get(drugStockId);
            if (drugStock == null)
            {
                return NotFound();
            }

            return Ok(drugStock);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateDrugStockDto drugStockDto)
        {
            var drug = _drugRepository.Get(drugStockDto.DrugId);
            if (drug == null)
            {
                return NotFound();
            }

            var drugStock = new DrugStock
            {
                DrugId = drugStockDto.DrugId,
                Quantity = drugStockDto.Quantity,
                ExpirationDate = drugStockDto.ExpirationDate
            };

            _drugStockRepository.Add(drugStock);
            _drugStockRepository.SaveChanges();

            return Created(nameof(Get), drugStock);
        }

        [HttpDelete("{drugStockId:guid}")]
        public IActionResult Delete(Guid drugStockId)
        {
            var drugStock = _drugStockRepository.Get(drugStockId);
            if (drugStock == null)
            {
                return NotFound();
            }

            _drugStockRepository.Delete(drugStock);
            _drugStockRepository.SaveChanges();

            return Ok();
        }

        [HttpPut("{drugStockId:guid}")]
        public IActionResult Update(Guid drugStockId,
            [FromBody] CreateDrugStockDto drugStockDto)
        {
            var drugStock = _drugStockRepository.Get(drugStockId);
            if (drugStock == null)
            {
                return NotFound();
            }

            drugStock.Quantity = drugStockDto.Quantity;
            drugStock.ExpirationDate = drugStockDto.ExpirationDate;

            _drugStockRepository.Update(drugStock);
            _drugStockRepository.SaveChanges();

            return Ok(drugStock);
        }
    }
}
