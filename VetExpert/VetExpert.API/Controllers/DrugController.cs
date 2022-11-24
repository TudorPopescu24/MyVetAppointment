using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VetExpert.API.Dto;
using VetExpert.Domain;
using VetExpert.Infrastructure;

namespace VetExpert.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrugController : ControllerBase
    {
        private readonly IRepository<Drug> _drugRepository;
        private readonly IRepository<DrugStock> _drugStockRepository;

        public DrugController(IRepository<Drug> drugRepository, IRepository<DrugStock> drugStockRepository)
        {
            _drugRepository = drugRepository;
            _drugStockRepository = drugStockRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_drugRepository.GetAll());
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateDrugDto drugDto)
        {
            var drug = new Drug
            {
                Name = drugDto.Name,
                Manufacturer = drugDto.Manufacturer,
                Weight = drugDto.Weight,
                Prospect = drugDto.Prospect,
                Price = drugDto.Price
            };

            _drugRepository.Add(drug);
            _drugRepository.SaveChanges();

            return Created(nameof(Get), drug);
        }

        [HttpPost("{drugId:guid}/drugStocks")]
        public IActionResult AddDrugStocks(Guid drugId,
            [FromBody] List<CreateDrugStockDto> drugStockDtos)
        {
            var drug = _drugRepository.Get(drugId);
            if (drug == null)
            {
                return NotFound();
            }

            List<DrugStock> drugStocks = drugStockDtos.Select(ds => new DrugStock
            {
                Quantity = ds.Quantity,
                ExpirationDate = ds.ExpirationDate,
            }).ToList();

            drugStocks.ForEach(x => _drugStockRepository.Add(x));
            _drugStockRepository.SaveChanges();

            return NoContent();
        }

        //Delete

        //Update

        //Get by Id
    }
}
