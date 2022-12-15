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
        public async Task<IActionResult> Get()
        {
            var pets = await _petRepository.GetAll();
            return Ok(pets);
        }

        [HttpGet("{petId:guid}")]
        public async Task<IActionResult> Get(Guid petId)
        {
            var pet = await _petRepository.Get(petId);

            if (pet == null)
            {
                return NotFound();
            }

            return Ok(pet);
        }

        [HttpPost]

        public async Task<IActionResult> Create([FromBody] CreatePetDto petDto)
        {
            var user = await _userRepository.Get(petDto.UserId);
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

            await _petRepository.Add(pet);
            await _petRepository.SaveChangesAsync();

            return Created(nameof(Get), pet);
        }


        [HttpDelete("{petId:guid}")]
        public async Task<IActionResult> Delete(Guid petId)
        {
            var pet = await _petRepository.Get(petId);
            if (pet == null)
            {
                return NotFound();
            }

            _petRepository.Delete(pet);
            await _petRepository.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{petId:guid}")]
        public async Task< IActionResult> Update(Guid petId,
            [FromBody] CreatePetDto petDto)
        {
            var pet = await _petRepository.Get(petId);
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
            await _petRepository.SaveChangesAsync();

            return Ok(pet);
        }
    }
}
