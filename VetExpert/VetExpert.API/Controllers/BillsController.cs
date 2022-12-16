using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;
using VetExpert.Infrastructure;
using VetExpert.Domain;
using VetExpert.API.Dto;

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


        public BillsController(IRepository<Bill> billRepository, IRepository<Drug> drugRepository,
                                IRepository<User> userRepository, IRepository<Clinic> clinicRepository)
        {
            _billRepository = billRepository;
            _drugRepository = drugRepository;
            _userRepository = userRepository;
            _clinicRepository = clinicRepository;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var bills = await _billRepository.GetAll();

            return bills;
        }



//Cred ca trebuie din prima id-urile la clinica si user si primul drug 

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBillDto billDto)
        {
            var bill = new Bill
            {
                Value = billDto.Value,
                Currency = billDto.Currency,
                DateTime = billDto.DateTime

            };

            await _billRepository.Add(bill);
            await _billRepository.SaveChanges();

            return Created(nameof(Get), bill);
        }



       [HttpPut]
        public async Task<IActionResult> AddDrugs(Guid billId,
           Guid drugId)
        {
            var bill =await  _billRepository.Get(billId);
            if (bill == null)
            {
                return NotFound();
            }
            var drug =await  _drugRepository.Get(drugId);
            if(drug == null)
            {
                return NotFound();
            }


            return NoContent();
        }


        [HttpDelete("{billId:guid}")]
        public async Task<IActionResult> Delete(Guid billId)
        {
            var bill = _billRepository.Get(billId);
            if (bill == null)
            {
                return NotFound();
            }

            _billRepository.Delete(bill);
           await  _billRepository.SaveChangesAsync();

            return Ok();
        }



    }
}
