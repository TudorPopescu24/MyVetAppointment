using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VetExpert.API.Dto;
using VetExpert.Domain;
using VetExpert.Infrastructure;

namespace VetExpert.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IRepository<Clinic> _clinicRepository;
        private readonly IRepository<Doctor> _doctorRepository;
        private readonly IRepository<Specialization> _specializationRepository;
        private readonly IRepository<DoctorSpecialization> _doctorSpecializationRepository;

        public DoctorsController(IRepository<Clinic> clinicRepository, 
            IRepository<Doctor> doctorRepository, 
            IRepository<Specialization> specializationRepository,
            IRepository<DoctorSpecialization> doctorSpecializationRepository)
        {
            _clinicRepository = clinicRepository;
            _doctorRepository = doctorRepository;
            _specializationRepository = specializationRepository;
            _doctorSpecializationRepository = doctorSpecializationRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_doctorRepository.GetAll());
        }

        [HttpGet("{doctorId:guid}")]
        public IActionResult Get(Guid doctorId)
        {
            var doctor = _doctorRepository.Get(doctorId);
            if (doctor == null)
            {
                return NotFound();
            }

            return Ok(doctor);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateDoctorDto doctorDto)
        {
            var clinic = _clinicRepository.Get(doctorDto.ClinicId);
            if (clinic == null)
            {
                return NotFound();
            }

            var doctor = new Doctor
            {
                ClinicId = doctorDto.ClinicId,
                FirstName = doctorDto.FirstName,
                LastName = doctorDto.LastName,
                Email = doctorDto.Email
            };

            _doctorRepository.Add(doctor);
            _doctorRepository.SaveChanges();

            return Created(nameof(Get), doctor);
        }

        [HttpDelete("{doctorId:guid}")]
        public IActionResult Delete(Guid doctorId)
        {
            var doctor = _doctorRepository.Get(doctorId);
            if (doctor == null)
            {
                return NotFound();
            }

            _doctorRepository.Delete(doctor);
            _doctorRepository.SaveChanges();

            return Ok();
        }

        [HttpPut("{doctorId:guid}")]
        public IActionResult Update(Guid doctorId,
            [FromBody] CreateDoctorDto doctorDto)
        {
            var doctor = _doctorRepository.Get(doctorId);
            if (doctor == null)
            {
                return NotFound();
            }

            doctor.FirstName = doctorDto.FirstName;
            doctor.LastName = doctorDto.LastName;
            doctor.Email = doctorDto.Email;

            _doctorRepository.Update(doctor);
            _doctorRepository.SaveChanges();

            return Ok(doctor);
        }

        [HttpPost("{doctorId:guid}/specializations/{specializationId:guid}")]
        public IActionResult RegisterSpecialization(Guid doctorId, Guid specialiationId)
        {
            var doctor = _doctorRepository.Get(doctorId);
            if (doctor == null)
            {
                return NotFound();
            }

            var specialization = _specializationRepository.Get(specialiationId);
            if (specialization == null)
            {
                return NotFound();
            }

            DoctorSpecialization doctorSpecialization = new DoctorSpecialization
            {
                DoctorId = doctor.Id,
                SpecializationId = specialization.Id
            };

            _doctorSpecializationRepository.Add(doctorSpecialization);
            _doctorSpecializationRepository.SaveChanges();

            return Ok(doctorSpecialization);
        }
    }
}
