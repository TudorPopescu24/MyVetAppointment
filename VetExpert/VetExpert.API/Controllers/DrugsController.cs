using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VetExpert.API.Dto;
using VetExpert.Domain;
using VetExpert.Infrastructure;

namespace VetExpert.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrugsController : ControllerBase
    {
        private readonly IRepository<Drug> _drugRepository;
        private readonly IRepository<DrugStock> _drugStockRepository;

        public DrugsController(IRepository<Drug> drugRepository, IRepository<DrugStock> drugStockRepository)
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


        [HttpDelete("{drugId:guid}")]
        public IActionResult Delete(Guid drugId)
        {
            var drug = _drugRepository.Get(drugId);
            if (drug == null)
            {
                return NotFound();
            }

            _drugRepository.Delete(drug);
            _drugRepository.SaveChanges();

            return Ok();
        }

        [HttpPut("{drugId:guid}")]
        public IActionResult Update(Guid drugId,
           [FromBody] CreateDrugDto drugDto)
        {
            var drug = _drugRepository.Get(drugId);
            if (drug == null)
            {
                return NotFound();
            }

            drug.Name = drugDto.Name;
            drug.Manufacturer = drugDto.Manufacturer;
            drug.Weight = drugDto.Weight;
            drug.Prospect = drugDto.Prospect;
            drug.Price = drugDto.Price;

            _drugRepository.Update(drug);
            _drugRepository.SaveChanges();

            return Ok(drug);
        }

        //Get by Id
    }
}
