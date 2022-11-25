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

        public AdminController(IRepository<Admin> adminRepository)
        {
            _adminRepository = adminRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_adminRepository.GetAll());
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateAdminDto adminDto)
        {
            var admin = new Admin
            {
                UserName = adminDto.UserName
            };
            _adminRepository.Add(admin);
            _adminRepository.SaveChanges();

            return Created(nameof(Get), admin);
        }
    }
}
