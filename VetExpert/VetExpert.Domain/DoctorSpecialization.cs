using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetExpert.Domain
{
    public class DoctorSpecialization
    {
        public Guid DoctorId { get; set; }

        public virtual Doctor Doctor { get; set; }

        public Guid SpecializationId { get; set; }

        public virtual Specialization Specialization { get; set; }
    }
}
