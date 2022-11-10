using Microsoft.Win32;
using System;
using System.Collections.Generic;

namespace MyVetAppointment.API.Entities
{
	public class Appointment
	{
		public int Id { get; set; }

		public DateTime DateTime { get; set; }

		public Pet Pet { get; set; }

		public Doctor Doctor { get; set; }

		public Clinic Clinic { get; set; }
	}
}