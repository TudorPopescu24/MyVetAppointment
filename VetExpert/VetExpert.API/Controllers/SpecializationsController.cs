using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VetExpert.API.Dto;
using VetExpert.Domain;
using VetExpert.Infrastructure;

namespace VetExpert.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecializationsController : ControllerBase
    {
        private readonly IRepository<Specialization> _specializationRepository;

        public SpecializationsController(IRepository<Specialization> specializationRepository)
        {
            _specializationRepository = specializationRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var specializations = await _specializationRepository.GetAll();
            return Ok(specializations);
        }

        [HttpGet("{specializationId:guid}")]
        public async Task<IActionResult> Get(Guid specializationId)
        {
            var specialization = await _specializationRepository.Get(specializationId);
            if (specialization == null)
            {
                return NotFound();
            }

            return Ok(specialization);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSpecializationDto specializationDto)
        {
            var specialization = new Specialization
            {
                Name = specializationDto.Name,
                Description = specializationDto.Description,
            };

            await _specializationRepository.Add(specialization);
            await _specializationRepository.SaveChangesAsync();

            return Created(nameof(Get), specialization);
        }

        [HttpDelete("{specializationId:guid}")]
        public async Task<IActionResult> Delete(Guid specializationId)
        {
            var specialization = await _specializationRepository.Get(specializationId);
            if (specialization == null)
            {
                return NotFound();
            }

            _specializationRepository.Delete(specialization);
            await _specializationRepository.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{specializationId:guid}")]
        public async Task<IActionResult> Update(Guid specializationId,
            [FromBody] CreateSpecializationDto specializationDto)
        {
            var specialization = await _specializationRepository.Get(specializationId);
            if (specialization == null)
            {
                return NotFound();
            }

            specialization.Description = specializationDto.Description;
            specialization.Name = specializationDto.Name;

            _specializationRepository.Update(specialization);
            await _specializationRepository.SaveChangesAsync();

            return Ok(specialization);
        }
    }
}
