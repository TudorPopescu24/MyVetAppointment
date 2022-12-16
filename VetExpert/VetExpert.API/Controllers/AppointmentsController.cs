using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;
using VetExpert.Infrastructure;
using VetExpert.API.Dto;
using VetExpert.Domain;

namespace VetExpert.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IRepository<Pet> _petRepository;
        private readonly IRepository<Doctor> _doctorRepository;
        private readonly IRepository<Appointment> _appointmentRepository;

        public AppointmentsController(IRepository<Pet> petRepository, IRepository<Doctor> doctorRepository,
             IRepository<Appointment> appointmentRepository)
        {
            _petRepository = petRepository;
            _doctorRepository = doctorRepository;
            _appointmentRepository = appointmentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var app = await _appointmentRepository.GetAll();

            return Ok(app);
        }




        [HttpPost("{appointmentId:guid}")]
        public async Task<IActionResult> Create([FromBody] CreateAppointmentDto appointmentDto)
        {

            var pet = _petRepository.Get(appointmentDto.PetId);
            if (pet == null)
            {
                return NotFound();
            }
            var doctor = _doctorRepository.Get(appointmentDto.DoctorId);
            if (doctor == null)
            {
                return NotFound();
            }


            Appointment appointment = new Appointment
            {
                PetId = appointmentDto.PetId,
                DoctorId = appointmentDto.DoctorId

            };

            await _appointmentRepository.Add(appointment);
            await _appointmentRepository.SaveChangesAsync();

            return Created(nameof(Get), appointment);
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
