using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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
        private readonly IMapper _mapper;
		private IValidator<Clinic> _validator;

		public ClinicsController(IRepository<Clinic> clinicRepository, 
            IRepository<Doctor> doctorRepository,
            IMapper mapper,
            IValidator<Clinic> validator)
        {
            _clinicRepository = clinicRepository;
            _doctorRepository = doctorRepository;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var clinics = await _clinicRepository.GetAll();

            return Ok(clinics);
        }

        [HttpGet("{clinicId:guid}")]
        public async Task<IActionResult> Get(Guid clinicId)
        {
            var clinic = await _clinicRepository.Get(clinicId);

            if (clinic == null)
            {
                return NotFound();
            }

            return Ok(clinic);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateClinicDto clinicDto)
        {
            var clinic = _mapper.Map<Clinic>(clinicDto);

            var validationResult = _validator.Validate(clinic);

            if (validationResult.IsValid)
            {
				await _clinicRepository.Add(clinic);
				await _clinicRepository.SaveChangesAsync();
			}

            return Created(nameof(Get), clinic);
        }

        [HttpDelete("{clinicId:guid}")]
        public async Task<IActionResult> Delete(Guid clinicId)
        {
            var clinic = await _clinicRepository.Get(clinicId);

            if (clinic == null)
            {
                return NotFound();
            }

            _clinicRepository.Delete(clinic);
            await _clinicRepository.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{clinicId:guid}")]
        public async Task<IActionResult> Update(Guid clinicId,
            [FromBody] CreateClinicDto clinicDto)
        {
            var clinic = await _clinicRepository.Get(clinicId);

            if (clinic == null)
            {
                return NotFound();
            }

            clinic.Name = clinicDto.Name;
            clinic.Address = clinicDto.Address;
            clinic.Email = clinicDto.Email;
            clinic.WebsiteUrl = clinicDto.WebsiteUrl;

            _clinicRepository.Update(clinic);
            await _clinicRepository.SaveChangesAsync();

            return Ok(clinic);
        }
    }
}
