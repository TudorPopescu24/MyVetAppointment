using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;
using VetExpert.Infrastructure;
using VetExpert.API.Dto;
using VetExpert.Domain;

namespace VetExpert.API.Dto
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IRepository<Pet> _petRepository;
        private readonly IRepository<Doctor> _doctorRepository;
        private readonly IRepository<Appointment> _appointmentRepository;



        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_appointmentRepository.GetAll());
        }


        

<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
        [HttpPost]
        public IActionResult Create(Guid petId, Guid doctorId)
        {

            var pet = _petRepository.Get(petId);
=======
=======
>>>>>>> 2bed1a66f535b50800bc78fd4d0228c9b7c8ebec
=======
>>>>>>> 2bed1a66f535b50800bc78fd4d0228c9b7c8ebec
=======
>>>>>>> 2bed1a66f535b50800bc78fd4d0228c9b7c8ebec
        [HttpPost("{appointmentId:guid}")]
        public IActionResult Create([FromBody] CreateAppointmentDto appointmentDto)
        {

            var pet = _petRepository.Get(appointmentDto.PetId);
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
>>>>>>> 2bed1a66f535b50800bc78fd4d0228c9b7c8ebec
=======
>>>>>>> 2bed1a66f535b50800bc78fd4d0228c9b7c8ebec
=======
>>>>>>> 2bed1a66f535b50800bc78fd4d0228c9b7c8ebec
=======
>>>>>>> 2bed1a66f535b50800bc78fd4d0228c9b7c8ebec
            if (pet == null)
            {
                return NotFound();
            }
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
            var doctor = _doctorRepository.Get(doctorId);
=======
            var doctor = _doctorRepository.Get(appointmentDto.DoctorId);
>>>>>>> 2bed1a66f535b50800bc78fd4d0228c9b7c8ebec
=======
            var doctor = _doctorRepository.Get(appointmentDto.DoctorId);
>>>>>>> 2bed1a66f535b50800bc78fd4d0228c9b7c8ebec
=======
            var doctor = _doctorRepository.Get(appointmentDto.DoctorId);
>>>>>>> 2bed1a66f535b50800bc78fd4d0228c9b7c8ebec
=======
            var doctor = _doctorRepository.Get(appointmentDto.DoctorId);
>>>>>>> 2bed1a66f535b50800bc78fd4d0228c9b7c8ebec
            if (doctor == null)
            {
                return NotFound();
            }


            Appointment appointment = new Appointment
            {
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
                PetId = petId,
                DoctorId = doctorId
=======
                PetId = appointmentDto.PetId,
                DoctorId = appointmentDto.DoctorId
>>>>>>> 2bed1a66f535b50800bc78fd4d0228c9b7c8ebec
=======
                PetId = appointmentDto.PetId,
                DoctorId = appointmentDto.DoctorId
>>>>>>> 2bed1a66f535b50800bc78fd4d0228c9b7c8ebec
=======
                PetId = appointmentDto.PetId,
                DoctorId = appointmentDto.DoctorId
>>>>>>> 2bed1a66f535b50800bc78fd4d0228c9b7c8ebec
=======
                PetId = appointmentDto.PetId,
                DoctorId = appointmentDto.DoctorId
>>>>>>> 2bed1a66f535b50800bc78fd4d0228c9b7c8ebec

            };

            _appointmentRepository.Add(appointment);
            _appointmentRepository.SaveChanges();

            return Created(nameof(Get), appointment);
        }

<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
=======
=======
>>>>>>> 2bed1a66f535b50800bc78fd4d0228c9b7c8ebec
=======
>>>>>>> 2bed1a66f535b50800bc78fd4d0228c9b7c8ebec
=======
>>>>>>> 2bed1a66f535b50800bc78fd4d0228c9b7c8ebec




        [HttpDelete("{appointmentId:guid}")]
        public IActionResult Delete(Guid appointmentId)
        {
            var appointment = _appointmentRepository.Get(appointmentId);
            if (appointment == null)
            {
                return NotFound();
            }

            _appointmentRepository.Delete(appointment);
            _appointmentRepository.SaveChanges();

            return Ok();
        }


<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
>>>>>>> 2bed1a66f535b50800bc78fd4d0228c9b7c8ebec
=======
>>>>>>> 2bed1a66f535b50800bc78fd4d0228c9b7c8ebec
=======
>>>>>>> 2bed1a66f535b50800bc78fd4d0228c9b7c8ebec
=======
>>>>>>> 2bed1a66f535b50800bc78fd4d0228c9b7c8ebec
    }
}
