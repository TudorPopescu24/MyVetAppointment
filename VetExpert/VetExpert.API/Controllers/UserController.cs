﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VetExpert.API.Dto;
using VetExpert.Domain;
using VetExpert.Infrastructure;

namespace VetExpert.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepository<User> _userRepository;

        public UserController(IRepository<User> userRepository)
        {
            _userRepository = userRepository;

        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _userRepository.GetAll();
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserDto userDto)
        {
            var user = new User
            {
                Name = userDto.Name,
                Email = userDto.Email,
                PhoneNumber = userDto.PhoneNumber,
                Address = userDto.Address
            };
            await _userRepository.Add(user);
            await _userRepository.SaveChangesAsync();

            return Created(nameof(Get), user);
        }

        [HttpDelete("{userId:guid}")]
        public async Task<IActionResult> Delete(Guid userId)
        {
            var user = await _userRepository.Get(userId);
            if (user == null)
            {
                return NotFound();
            }

            _userRepository.Delete(user);
            await _userRepository.SaveChangesAsync();

            return Ok();
        }


        [HttpPut("{userId:guid}")]
        public async Task<IActionResult> Update(Guid userId,
            [FromBody] CreateUserDto userDto)
        {
            var user = await _userRepository.Get(userId);
            if (user == null)
            {
                return NotFound();
            }

            user.Name = userDto.Name;
            user.Address = userDto.Address;
            user.Email = userDto.Email;
            user.PhoneNumber = userDto.PhoneNumber;

            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();

            return Ok(user);
        }
    }
}
