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
        private readonly IRepository<ApplicationUser> _appUserRepository;
        private readonly IMapper _mapper;

		public ClinicsController(IRepository<Clinic> clinicRepository,
            IMapper mapper,
            IRepository<ApplicationUser> appUserRepository)
        {
            _clinicRepository = clinicRepository;
            _mapper = mapper;
            _appUserRepository = appUserRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var clinics = await _clinicRepository.GetAll();
            return Ok(clinics);
        }

		[HttpGet("applicationUser/{appUserId:guid}")]
		public async Task<IActionResult> Get(Guid appUserId)
		{
			var clinic = await _clinicRepository.FindEntity(x => x.ApplicationUserId == appUserId);

			return Ok(clinic);
		}

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateClinicDto clinicDto)
        {
            var clinic = _mapper.Map<Clinic>(clinicDto);

            var appUser = await _appUserRepository.Get(clinicDto.ApplicationUserId);

            if (appUser == null)
            {
                return NotFound();
            }

            clinic.ApplicationUser = appUser;
            clinic.ApplicationUserId = appUser.Id;

            await _clinicRepository.Add(clinic);
			await _clinicRepository.SaveChangesAsync();

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
