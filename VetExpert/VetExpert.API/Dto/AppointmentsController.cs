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


        

        [HttpPost]
        public IActionResult Create(Guid petId, Guid doctorId)
        {

            var pet = _petRepository.Get(petId);
            if (pet == null)
            {
                return NotFound();
            }
            var doctor = _doctorRepository.Get(doctorId);
            if (doctor == null)
            {
                return NotFound();
            }


            Appointment appointment = new Appointment
            {
                PetId = petId,
                DoctorId = doctorId

            };

            _appointmentRepository.Add(appointment);
            _appointmentRepository.SaveChanges();

            return Created(nameof(Get), appointment);
        }

    }
}
