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


        public BillsController(IRepository<Bill> _billRepository, IRepository<Drug> _drugRepository,
                                IRepository<User> _userRepository, IRepository<Clinic> _clinicRepository)
        {
            _billRepository = _billRepository;
            _drugRepository = _drugRepository;
            _userRepository = _userRepository;
            _clinicRepository = _clinicRepository;

        }


        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_billRepository.GetAll());
        }



//Cred ca trebuie din prima id-urile la clinica si user si primul drug 

        [HttpPost]
        public IActionResult Create([FromBody] CreateBillDto billDto)
        {
            var bill = new Bill
            {
                Value = billDto.Value,
                Currency = billDto.Currency,
                DateTime = billDto.DateTime

            };

            _billRepository.Add(bill);
            _billRepository.SaveChanges();

            return Created(nameof(Get), bill);
        }



       [HttpPut]
        public IActionResult AddDrugs(Guid billId,
           Guid drugId)
        {
            var bill = _billRepository.Get(billId);
            if (bill == null)
            {
                return NotFound();
            }
            var drug = _drugRepository.Get(drugId);
            if(drug == null)
            {
                return NotFound();
            }


            return NoContent();
        }


<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
=======
=======
>>>>>>> 2bed1a66f535b50800bc78fd4d0228c9b7c8ebec
=======
>>>>>>> 2bed1a66f535b50800bc78fd4d0228c9b7c8ebec
        [HttpDelete("{billId:guid}")]
        public IActionResult Delete(Guid billId)
        {
            var bill = _billRepository.Get(billId);
            if (bill == null)
            {
                return NotFound();
            }

            _billRepository.Delete(bill);
            _billRepository.SaveChanges();

            return Ok();
        }


<<<<<<< HEAD
<<<<<<< HEAD
>>>>>>> 2bed1a66f535b50800bc78fd4d0228c9b7c8ebec
=======
>>>>>>> 2bed1a66f535b50800bc78fd4d0228c9b7c8ebec
=======
>>>>>>> 2bed1a66f535b50800bc78fd4d0228c9b7c8ebec

    }
}
