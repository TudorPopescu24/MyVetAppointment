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

        public DrugsController(IRepository<Drug> drugRepository)
        {
            _drugRepository = drugRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var drugs = await _drugRepository.GetAll();
            return Ok(drugs);
        }

        [HttpGet("{drugId:guid}")]
        public async Task<IActionResult> Get(Guid drugId)
        {
            var drug = await _drugRepository.Get(drugId);

            if (drug == null)
            {
                return NotFound();
            }

            return Ok(drug);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDrugDto drugDto)
        {
            var drug = new Drug
            {
                Name = drugDto.Name,
                Manufacturer = drugDto.Manufacturer,
                Weight = drugDto.Weight,
                Prospect = drugDto.Prospect,
                Price = drugDto.Price
            };

            await _drugRepository.Add(drug);
            await _drugRepository.SaveChangesAsync();

            return Created(nameof(Get), drug);
        }


        [HttpDelete("{drugId:guid}")]
        public async Task<IActionResult> Delete(Guid drugId)
        {
            var drug = await _drugRepository.Get(drugId);
            if (drug == null)
            {
                return NotFound();
            }

            _drugRepository.Delete(drug);
            await _drugRepository.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{drugId:guid}")]
        public async Task<IActionResult> Update(Guid drugId,
           [FromBody] CreateDrugDto drugDto)
        {
            var drug = await _drugRepository.Get(drugId);
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
            await _drugRepository.SaveChangesAsync();

            return Ok(drug);
        }
    }
}
