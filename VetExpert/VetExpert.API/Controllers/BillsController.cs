using VetExpert.Domain;
using VetExpert.API.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VetExpert.Infrastructure;

namespace VetExpert.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillsController : ControllerBase
    {
        private readonly IRepository<Bill> _billRepository;
        private readonly IRepository<Drug> _drugRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Clinic> _clinicRepository;
        private readonly IMapper _mapper;


        public BillsController(IRepository<Bill> billRepository, 
	        IRepository<Drug> drugRepository, 
            IRepository<User> userRepository,
            IRepository<Clinic> clinicRepository,
	        IMapper mapper)
        {
            _billRepository = billRepository;
            _drugRepository = drugRepository;
            _userRepository = userRepository;
            _clinicRepository = clinicRepository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var bills = await _billRepository.GetAll();

            return Ok(bills);
        }

        [HttpGet("clinic/{clinicId:guid}")]
        public async Task<IActionResult> GetClinicBills(Guid clinicId)
        {
	        var bills = await _billRepository.Find(x => x.ClinicId == clinicId);

	        return Ok(bills);
        }

		[HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBillDto billDto)
        {
	        var user = await _userRepository.Get(billDto.UserId);

	        if (user == null)
	        {
		        return NotFound();
	        }

	        var clinic = await _clinicRepository.Get(billDto.ClinicId);

	        if (clinic == null)
	        {
		        return NotFound();
	        }

			var bill = _mapper.Map<Bill>(billDto);

			bill.User = user;
			bill.UserId = user.Id;
			bill.Clinic = clinic;
			bill.ClinicId = clinic.Id;

			await _billRepository.Add(bill);
            await _billRepository.SaveChangesAsync();

            return Created(nameof(Get), bill);
        }



        [HttpPut]
        public async Task<IActionResult> AddDrugs(Guid billId,
            Guid drugId)
        {
            var bill = await _billRepository.Get(billId);
            if (bill == null)
            {
                return NotFound();
            }

            var drug = await _drugRepository.Get(drugId);
            if (drug == null)
            {
                return NotFound();
            }


            return NoContent();
        }


        [HttpDelete("{billId:guid}")]
        public async Task<IActionResult> Delete(Guid billId)
        {
            var bill = await _billRepository.Get(billId);
            if (bill == null)
            {
                return NotFound();
            }

            _billRepository.Delete(bill);
            await _billRepository.SaveChangesAsync();

            return Ok();
        }
    }
}