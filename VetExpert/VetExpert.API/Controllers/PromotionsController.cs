using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VetExpert.API.Dto;
using VetExpert.Domain;
using VetExpert.Infrastructure;

namespace VetExpert.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionsController : ControllerBase
    {
        private readonly IRepository<Clinic> _clinicRepository;
        private readonly IRepository<Promotion> _promotionRepository;

        public PromotionsController(IRepository<Clinic> clinicRepository, IRepository<Promotion> promotioncRepository)
        {
            _clinicRepository = clinicRepository;
            _promotionRepository = promotioncRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var promotions = await _promotionRepository.GetAll();
            return Ok(promotions);
        }

        [HttpGet("{promotionId:guid}")]
        public async Task<IActionResult> Get(Guid promotionId)
        {
            var promotion = await _promotionRepository.Get(promotionId);
            if (promotion == null)
            {
                return NotFound();
            }

            return Ok(promotion);
        }

        [HttpPost]

        public async Task<IActionResult> Create([FromBody] CreatePromotionDto promotionDto)
        {
            var clinic = await _clinicRepository.Get(promotionDto.ClinicId);
            if (clinic == null)
            {
                return NotFound();
            }

            var promotion = new Promotion
            {
                Name = promotionDto.Name,
                Description = promotionDto.Description
            };

           await _promotionRepository.Add(promotion);
           await _promotionRepository.SaveChangesAsync();

            return Created(nameof(Get), promotion);
        }


        [HttpDelete("{promotionId:guid}")]
        public async Task<IActionResult> Delete(Guid promotionId)
        {
            var promotion = await _promotionRepository.Get(promotionId);
            if (promotion == null)
            {
                return NotFound();
            }

            _promotionRepository.Delete(promotion);
            await _promotionRepository.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{promotionId:guid}")]
        public async Task<IActionResult> Update(Guid promotionId,
            [FromBody] CreatePromotionDto promotionDto)
        {
            var promotion = await _promotionRepository.Get(promotionId);
            if (promotion == null)
            {
                return NotFound();
            }

            promotion.Name = promotionDto.Name;
            promotion.Description = promotionDto.Description;

            _promotionRepository.Update(promotion);
            await _promotionRepository.SaveChangesAsync();

            return Ok(promotion);
        }

    }
}
