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
        public IActionResult Get()
        {
            return Ok(_specializationRepository.GetAll());
        }

        [HttpGet("{specializationId:guid}")]
        public IActionResult Get(Guid specializationId)
        {
            var specialization = _specializationRepository.Get(specializationId);
            if (specialization == null)
            {
                return NotFound();
            }

            return Ok(specialization);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateSpecializationDto specializationDto)
        {
            var specialization = new Specialization
            {
                Name = specializationDto.Name,
                Description = specializationDto.Description,
            };

            _specializationRepository.Add(specialization);
            _specializationRepository.SaveChanges();

            return Created(nameof(Get), specialization);
        }

        [HttpDelete("{specializationId:guid}")]
        public IActionResult Delete(Guid specializationId)
        {
            var specialization = _specializationRepository.Get(specializationId);
            if (specialization == null)
            {
                return NotFound();
            }

            _specializationRepository.Delete(specialization);
            _specializationRepository.SaveChanges();

            return Ok();
        }

        [HttpPut("{specializationId:guid}")]
        public IActionResult Update(Guid specializationId,
            [FromBody] CreateSpecializationDto specializationDto)
        {
            var specialization = _specializationRepository.Get(specializationId);
            if (specialization == null)
            {
                return NotFound();
            }

            specialization.Description = specializationDto.Description;
            specialization.Name = specializationDto.Name;

            _specializationRepository.Update(specialization);
            _specializationRepository.SaveChanges();

            return Ok(specialization);
        }
    }
}
