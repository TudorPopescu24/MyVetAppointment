using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VetExpert.API.Dto;
using VetExpert.Domain;
using VetExpert.Infrastructure;

namespace VetExpert.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetsController : ControllerBase
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Pet> _petRepository;

        public PetsController(IRepository<User> userRepository, IRepository<Pet> petRepository)
        {
            _petRepository = petRepository;
            _userRepository = userRepository;
        }


        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_petRepository.GetAll());
        }

        [HttpGet("{petId:guid}")]
        public IActionResult Get(Guid petId)
        {
            var pet = _petRepository.Get(petId);
            if (pet == null)
            {
                return NotFound();
            }

            return Ok(pet);
        }

        [HttpPost]

        public IActionResult Create([FromBody] CreatePetDto petDto)
        {
            var user = _userRepository.Get(petDto.UserId);
            if (user == null)
            {
                return NotFound();
            }

            var pet = new Pet
            {
                Name = petDto.Name,
                TypeOfPet = petDto.TypeOfPet,
                Age = petDto.Age,
                Weight = petDto.Weight,
                IsVaccinated = petDto.IsVaccinated,
                DateOfVaccine = petDto.DateOfVaccine,
            };

            _petRepository.Add(pet);
            _petRepository.SaveChanges();

            return Created(nameof(Get), pet);
        }


        [HttpDelete("{petId:guid}")]
        public IActionResult Delete(Guid petId)
        {
            var pet = _petRepository.Get(petId);
            if (pet == null)
            {
                return NotFound();
            }

            _petRepository.Delete(pet);
            _petRepository.SaveChanges();

            return Ok();
        }

        [HttpPut("{petId:guid}")]
        public IActionResult Update(Guid petId,
            [FromBody] CreatePetDto petDto)
        {
            var pet = _petRepository.Get(petId);
            if (pet == null)
            {
                return NotFound();
            }

            petDto.Name = petDto.Name;
            petDto.TypeOfPet = petDto.TypeOfPet;
            petDto.Age = petDto.Age;
            petDto.Weight = petDto.Weight;
            petDto.IsVaccinated = petDto.IsVaccinated;
            petDto.DateOfVaccine = petDto.DateOfVaccine;

            _petRepository.Update(pet);
            _petRepository.SaveChanges();

            return Ok(pet);
        }
    }
}
