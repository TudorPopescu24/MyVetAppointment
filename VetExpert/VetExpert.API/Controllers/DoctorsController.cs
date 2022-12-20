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
    public class DoctorsController : ControllerBase
    {
        private readonly IRepository<Clinic> _clinicRepository;
        private readonly IRepository<Doctor> _doctorRepository;
        private readonly IRepository<Specialization> _specializationRepository;
        private readonly IRepository<DoctorSpecialization> _doctorSpecializationRepository;
        private readonly IMapper _mapper;


        public DoctorsController(IRepository<Clinic> clinicRepository,
            IRepository<Doctor> doctorRepository,
            IRepository<Specialization> specializationRepository,
            IRepository<DoctorSpecialization> doctorSpecializationRepository,
            IMapper mapper)
        {
            _clinicRepository = clinicRepository;
            _doctorRepository = doctorRepository;
            _specializationRepository = specializationRepository;
            _doctorSpecializationRepository = doctorSpecializationRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var doctors = await _doctorRepository.GetAll();

            return Ok(doctors);
        }

        [HttpGet("{doctorId:guid}")]
        public async Task<IActionResult> Get(Guid doctorId)
        {
            var doctor = await _doctorRepository.Get(doctorId);

            if (doctor == null)
            {
                return NotFound();
            }

            return Ok(doctor);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDoctorDto doctorDto)
        {
            var clinic = await _clinicRepository.Get(doctorDto.ClinicId);

            if (clinic == null)
            {
                return NotFound();
            }

            var doctor = _mapper.Map<Doctor>(doctorDto);

            await _doctorRepository.Add(doctor);
            await _doctorRepository.SaveChangesAsync();

            return Created(nameof(Get), doctor);
        }

        [HttpDelete("{doctorId:guid}")]
        public async Task<IActionResult> Delete(Guid doctorId)
        {
            var doctor = await _doctorRepository.Get(doctorId);

            if (doctor == null)
            {
                return NotFound();
            }

            _doctorRepository.Delete(doctor);
            await _doctorRepository.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{doctorId:guid}")]
        public async Task<IActionResult> Update(Guid doctorId,
            [FromBody] CreateDoctorDto doctorDto)
        {
            var doctor = await _doctorRepository.Get(doctorId);

            if (doctor == null)
            {
                return NotFound();
            }

            doctor.FirstName = doctorDto.FirstName;
            doctor.LastName = doctorDto.LastName;
            doctor.Email = doctorDto.Email;

            _doctorRepository.Update(doctor);
            await _doctorRepository.SaveChangesAsync();

            return Ok(doctor);
        }

        [HttpPost("{doctorId:guid}/specializations/{specializationId:guid}")]
        public async Task<IActionResult> RegisterSpecialization(Guid doctorId, Guid specialiationId)
        {
            var doctor = await _doctorRepository.Get(doctorId);

            if (doctor == null)
            {
                return NotFound();
            }

            var specialization = await _specializationRepository.Get(specialiationId);

            if (specialization == null)
            {
                return NotFound();
            }

            DoctorSpecialization doctorSpecialization = new DoctorSpecialization
            {
                DoctorId = doctor.Id,
                SpecializationId = specialization.Id
            };

            await _doctorSpecializationRepository.Add(doctorSpecialization);
            await _doctorSpecializationRepository.SaveChangesAsync();

            return Ok(doctorSpecialization);
        }
    }
}
