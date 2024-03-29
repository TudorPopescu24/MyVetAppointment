﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VetExpert.API.Dto;
using VetExpert.Domain;
using VetExpert.Infrastructure;

namespace VetExpert.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IRepository<Admin> _adminRepository;
        private readonly IMapper _mapper;


        public AdminController(IRepository<Admin> adminRepository, IMapper mapper)
        {
            _adminRepository = adminRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var admins = await _adminRepository.GetAll();
            return Ok(admins);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAdminDto adminDto)
        {
            var admin = _mapper.Map<Admin>(adminDto);

            await _adminRepository.Add(admin);
            await _adminRepository.SaveChangesAsync();

            return Created(nameof(Get), admin);
        }

        [HttpDelete("{adminId:guid}")]
        public async Task<IActionResult> Delete(Guid adminId)
        {
            var admin = await _adminRepository.Get(adminId);
            if (admin == null)
            {
                return NotFound();
            }

            _adminRepository.Delete(admin);
            await _adminRepository.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{adminId:guid}")]
        public async Task<IActionResult> Update(Guid adminId,
            [FromBody] CreateAdminDto adminDto)
        {
            var admin = await _adminRepository.Get(adminId);
            if (admin == null)
            {
                return NotFound();
            }

            admin.UserName = adminDto.UserName;

            _adminRepository.Update(admin);
            await _adminRepository.SaveChangesAsync();

            return Ok(admin);
        }
    }
}

