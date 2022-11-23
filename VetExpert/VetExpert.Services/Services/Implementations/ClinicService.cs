using VetExpert.Domain;
using VetExpert.Infrastructure;
using VetExpert.Services.Services.Interfaces;

namespace VetExpert.Services.Services.Implementations
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
