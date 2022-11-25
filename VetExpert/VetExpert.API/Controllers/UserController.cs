using Microsoft.AspNetCore.Http;
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
        private readonly IRepository<Pet> _petRepository;

        public UserController(IRepository<User> userRepository, IRepository<Pet> petRepository)
        {
            _userRepository = userRepository;
            _petRepository = petRepository;
           
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_userRepository.GetAll());
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateUserDto userDto)
        {
            var user = new User
            {
                Name = userDto.Name,
                Email = userDto.Email,
                PhoneNumber = userDto.PhoneNumber,
                Address = userDto.Address
            };
            _userRepository.Add(user);
            _userRepository.SaveChanges();

            return Created(nameof(Get),user);
        }

        [HttpPost("{userId:guid}/pets")]
        public IActionResult RegisterPets(Guid userId,
            [FromBody] List<CreatePetDto> petDtos)
        {
            var user = _userRepository.Get(userId);
            if (user == null)
            {
                return NotFound();
            }

            List<Pet> pets = petDtos.Select(d => new Pet
            {
               /* FirstName = d.FirstName,
                LastName = d.LastName,
                Email = d.Email,
                ClinicId = clinicId*/
            }).ToList();
            
            pets.ForEach(x => _petRepository.Add(x));
            _petRepository.SaveChanges();

            return NoContent();
        }


    }
}
