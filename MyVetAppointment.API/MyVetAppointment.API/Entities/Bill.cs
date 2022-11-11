using Microsoft.Win32;
using System;
using System.Collections.Generic;

namespace MyVetAppointment.API.Entities
{
    public class Bill
    {
        public int Id { get; set; }

        public int Value { get; set; }

        public string Currency { get; set; }

        public ICollection<Drug> Drugs { get; set; }

        public DateTime DateTime { get; set; }

        public User User { get; set; }

        public Clinic Clinic { get; set; }
    }
}
