using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VetExpert.API.Dto;
using VetExpert.Domain;
using VetExpert.Infrastructure;

namespace VetExpert.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicsController : ControllerBase
    {
        private readonly IRepository<Clinic> _clinicRepository;
        private readonly IRepository<Doctor> _doctorRepository;

        public ClinicsController(IRepository<Clinic> clinicRepository, IRepository<Doctor> doctorRepository)
        {
            _clinicRepository = clinicRepository;
            _doctorRepository = doctorRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_clinicRepository.GetAll());
        }

        [HttpGet("{clinicId:guid}")]
        public IActionResult Get(Guid clinicId)
        {
            var clinic = _clinicRepository.Get(clinicId);
            if (clinic == null)
            {
                return NotFound();
            }

            return Ok(clinic);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateClinicDto clinicDto)
        {
            var clinic = new Clinic
            {
                Name = clinicDto.Name,
                Email = clinicDto.Email,
                WebsiteUrl = clinicDto.WebsiteUrl,
                Address = clinicDto.Address
            };

            _clinicRepository.Add(clinic);
            _clinicRepository.SaveChanges();

            return Created(nameof(Get), clinic);
        }

        [HttpPost("{clinicId:guid}/doctors")]
        public IActionResult RegisterDoctors(Guid clinicId,
            [FromBody] List<CreateDoctorDto> doctorDtos)
        {
            var clinic = _clinicRepository.Get(clinicId);
            if (clinic == null)
            {
                return NotFound();
            }

            List<Doctor> doctors = doctorDtos.Select(d => new Doctor
            {
                FirstName = d.FirstName,
                LastName = d.LastName,
                Email = d.Email,
                ClinicId = clinicId
            }).ToList();

            doctors.ForEach(x => _doctorRepository.Add(x));
            _doctorRepository.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{clinicId:guid}")]
        public IActionResult Delete(Guid clinicId)
        {
            var clinic = _clinicRepository.Get(clinicId);
            if (clinic == null)
            {
                return NotFound();
            }

            _clinicRepository.Delete(clinic);
            _clinicRepository.SaveChanges();

            return Ok();
        }

        [HttpPut("{clinicId:guid}")]
        public IActionResult Update(Guid clinicId,
            [FromBody] CreateClinicDto clinicDto)
        {
            var clinic = _clinicRepository.Get(clinicId);
            if (clinic == null)
            {
                return NotFound();
            }

            clinic.Name = clinicDto.Name;
            clinic.Address = clinicDto.Address;
            clinic.Email = clinicDto.Email;
            clinic.WebsiteUrl = clinicDto.WebsiteUrl;

            _clinicRepository.Update(clinic);
            _clinicRepository.SaveChanges();

            return Ok(clinic);
        }
    }
}
