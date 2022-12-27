using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VetExpert.API.Dto;
using VetExpert.API.Mapping;
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
        private readonly IMapper _mapper;


        public PetsController(IRepository<User> userRepository, IRepository<Pet> petRepository,
            IMapper mapper)
        {
            _petRepository = petRepository;
            _userRepository = userRepository;
            _mapper = mapper;

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

            var pet = _mapper.Map<Pet>(petDto);

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

            pet.Name = petDto.Name;
            pet.TypeOfPet = petDto.TypeOfPet;
            pet.Age = petDto.Age;
            pet.Weight = petDto.Weight;
            pet.IsVaccinated = petDto.IsVaccinated;
            pet.DateOfVaccine = petDto.DateOfVaccine;

            _petRepository.Update(pet);
            await _petRepository.SaveChangesAsync();

            return Ok(pet);
        }
    }
}
