using MyVetAppointment.API.Entities;
using MyVetAppointment.API.Repositories.Interfaces;
using MyVetAppointment.API.Services.Interfaces;

namespace MyVetAppointment.API.Services.Implementations
{
    public class ClinicService : IClinicService
    {
        private readonly IRepository<Clinic> _repository;

        public ClinicService(IRepository<Clinic> repository)
        {
            _repository = repository;
        }

        public void InsertClinic(Clinic clinic)
        {
            _repository.Add(clinic);
        }
    }
}
