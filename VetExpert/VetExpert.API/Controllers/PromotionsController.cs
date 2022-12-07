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
        public IActionResult Get()
        {
            return Ok(_promotionRepository.GetAll());
        }

        [HttpGet("{promotionId:guid}")]
        public IActionResult Get(Guid promotionId)
        {
            var promotion = _promotionRepository.Get(promotionId);
            if (promotion == null)
            {
                return NotFound();
            }

            return Ok(promotion);
        }

        [HttpPost]

        public IActionResult Create([FromBody] CreatePromotionDto promotionDto)
        {
            var clinic = _clinicRepository.Get(promotionDto.ClinicId);
            if (clinic == null)
            {
                return NotFound();
            }

            var promotion = new Promotion
            {
                Name = promotionDto.Name,
                Description = promotionDto.Description
            };

            _promotionRepository.Add(promotion);
            _promotionRepository.SaveChanges();

            return Created(nameof(Get), promotion);
        }


        [HttpDelete("{promotionId:guid}")]
        public IActionResult Delete(Guid promotionId)
        {
            var promotion = _promotionRepository.Get(promotionId);
            if (promotion == null)
            {
                return NotFound();
            }

            _promotionRepository.Delete(promotion);
            _promotionRepository.SaveChanges();

            return Ok();
        }

        [HttpPut("{promotionId:guid}")]
        public IActionResult Update(Guid promotionId,
            [FromBody] CreatePromotionDto promotionDto)
        {
            var promotion = _promotionRepository.Get(promotionId);
            if (promotion == null)
            {
                return NotFound();
            }

            promotion.Name = promotionDto.Name;
            promotion.Description = promotionDto.Description;

            _promotionRepository.Update(promotion);
            _promotionRepository.SaveChanges();

            return Ok(promotion);
        }

    }
}
