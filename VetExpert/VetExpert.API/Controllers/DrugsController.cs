using AutoMapper;
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
		private readonly IRepository<Clinic> _clinicRepository;
		private readonly IMapper _mapper;


        public DrugsController(IRepository<Clinic> clinicRepository, 
            IRepository<Drug> drugRepository, 
            IMapper mapper)
        {
			_clinicRepository = clinicRepository;
			_drugRepository = drugRepository;
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

		[HttpGet("clinic/{clinicId:guid}")]
		public async Task<IActionResult> GetClinicDrugs(Guid clinicId)
		{
			var drugs = await _drugRepository.Find(x => x.ClinicId == clinicId);

			return Ok(drugs);
		}

		[HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDrugDto drugDto)
        {
			var clinic = await _clinicRepository.Get(drugDto.ClinicId);

			if (clinic == null)
			{
				return NotFound();
			}

			var drug = _mapper.Map<Drug>(drugDto);

            drug.Clinic = clinic;
            drug.ClinicId = clinic.Id;

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
