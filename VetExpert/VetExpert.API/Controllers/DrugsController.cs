using AutoMapper;
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
        private readonly IMapper _mapper;


        public DrugsController(IRepository<Drug> drugRepository, IRepository<DrugStock> drugStockRepository, IMapper mapper)
        {
            _drugRepository = drugRepository;
            _drugStockRepository = drugStockRepository;
            _mapper = mapper;
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
            var drug = _mapper.Map<Drug>(drugDto);

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
