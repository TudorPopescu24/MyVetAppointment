using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetExpert.Domain
{
    public class DoctorSpecialization
    {
        public Guid DoctorId { get; set; } = Guid.Empty;

        public virtual Doctor Doctor { get; set; } = new Doctor();

        public Guid SpecializationId { get; set; } = Guid.Empty;

        public virtual Specialization Specialization { get; set; } = new Specialization();
    }
}
