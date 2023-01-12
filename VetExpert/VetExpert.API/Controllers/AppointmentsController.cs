using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;
using VetExpert.Infrastructure;
using VetExpert.API.Dto;
using VetExpert.Domain;
using AutoMapper;

namespace VetExpert.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IRepository<Pet> _petRepository;
        private readonly IRepository<Clinic> _clinicRepository;
        private readonly IRepository<Appointment> _appointmentRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;


        public AppointmentsController(IRepository<Pet> petRepository, IRepository<Clinic> clinicRepository,
             IRepository<Appointment> appointmentRepository, IRepository<User> userRepository, IMapper mapper)
        {
            _petRepository = petRepository;
            _clinicRepository = clinicRepository;
            _appointmentRepository = appointmentRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var app = await _appointmentRepository.GetAll();

            return Ok(app);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserAppointments(Guid userId)
        {
	        var app = await _appointmentRepository.Find(x => x.UserId == userId);

	        return Ok(app);
        }


		[HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAppointmentDto appointmentDto)
        {

            var pet = await _petRepository.Get(appointmentDto.PetId);

            if (pet == null)
            {
                return NotFound();
            }

            var clinic = await _clinicRepository.Get(appointmentDto.ClinicId);

            if (clinic == null)
            {
                return NotFound();
            }

            var user = await _userRepository.Get(appointmentDto.UserId);

            if (user == null)
            {
	            return NotFound();
            }

			Appointment appointment = _mapper.Map<Appointment>(appointmentDto);

			appointment.User = user;
            appointment.UserId = user.Id;
            appointment.Pet = pet;
            appointment.PetId = pet.Id;
            appointment.Clinic = clinic;
            appointment.ClinicId = clinic.Id;

			await _appointmentRepository.Add(appointment);
            await _appointmentRepository.SaveChangesAsync();

            return Created(nameof(Get), appointment);
        }

        [HttpPut("{appointmentId:guid}")]
        public async Task<IActionResult> Update(Guid appointmentId,
	        [FromBody] CreateAppointmentDto appointmentDto)
        {
	        var appointment = await _appointmentRepository.Get(appointmentId);

	        if (appointment == null)
	        {
		        return NotFound();
	        }

	        var pet = await _petRepository.Get(appointmentDto.PetId);

	        if (pet == null)
	        {
		        return NotFound();
	        }

	        appointment.Pet = pet;
	        appointment.PetId = pet.Id;
	        appointment.DateTime = appointmentDto.DateTime;
	        
	        _appointmentRepository.Update(appointment);
	        await _appointmentRepository.SaveChangesAsync();

	        return Ok(appointment);
        }


		[HttpDelete("{appointmentId:guid}")]
        public async Task<IActionResult> Delete(Guid appointmentId)
        {
            var appointment = await _appointmentRepository.Get(appointmentId);
            if (appointment == null)
            {
                return NotFound();
            }

            _appointmentRepository.Delete(appointment);
            await _appointmentRepository.SaveChangesAsync();

            return Ok();
        }


    }
}
