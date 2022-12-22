using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetExpert.Domain;
using VetExpert.Infrastructure;

namespace VetExpert.IntegrationTesting
{
    public class DbSeeding
    {
        public static List<Clinic> clinics= new List<Clinic>();
        public static List<User> users = new List<User>();
        public static List<Pet> pets = new List<Pet>();
        public static List<Appointment> appointments = new List<Appointment>();
        public static List<Doctor> doctors = new List<Doctor>();
        public static List<Specialization> specializations = new List<Specialization>();

        public static void InitializeDbForTests(MainDbContext db)
        {
            
            //usrs
            users = GetSeedingUsers();
            db.Users.AddRange(users);
            //clinics

            //pets
            pets = GetSeedingPets();
            db.Pets.AddRange(pets);
            
            //clinics
            clinics=GetSeedingClinics();
            db.Clinics.AddRange(clinics);

            //doctors
            doctors = GetSeedingDoctors();
            db.Doctors.AddRange(doctors);

            //
            //appointments
            appointments = GetSeedingAppointments();
            db.Appointments.AddRange(appointments);

            //promotions
            db.Promotions.AddRange(GetSeedingPromotions());

            specializations = GetSeedingSpecialization();
            db.Specializations.AddRange(specializations);
            db.SaveChanges();

        
            
        }

        public static void ReinitializeDbForTests(MainDbContext db)
        {
            db.Drugs.RemoveRange(db.Drugs);
            db.DrugStocks.RemoveRange(db.DrugStocks);
            db.Specializations.RemoveRange(db.Specializations);
            db.Users.RemoveRange(db.Users);
            db.Pets.RemoveRange(db.Pets);
            db.Promotions.RemoveRange(db.Promotions);
            db.Clinics.RemoveRange(db.Clinics);
            db.Appointments.RemoveRange(db.Appointments);
            db.Doctors.RemoveRange(db.Doctors);

            InitializeDbForTests(db);
        }

        public static List<Promotion> GetSeedingPromotions()
        {
            return new List<Promotion>()
           {
                new Promotion
                {
                    Name="prom1",
                    Description = "prom des",
                    Clinic = clinics[0],
                    ClinicId= clinics[0].Id
                }
            
            };
        }


        public static List<Doctor> GetSeedingDoctors()
        {
            List<Doctor> doctorList = new List<Doctor>  {
                new Doctor
                {
                   Clinic= clinics[0],
                   ClinicId= clinics[0].Id,
                   LastName= "lname",
                   FirstName = "fname",
                   Email = "email"
                }
            };
            return doctorList;
        }


        public static List<Clinic> GetSeedingClinics()
        {
            List<Clinic> clinicsList= new List<Clinic>  {
                new Clinic
                {
                   Address="ADD",
                   Email = "email",
                   Name= "name",
                   WebsiteUrl= "url"
                }
            };
            return clinicsList;
        }


        public static List<User> GetSeedingUsers()
        {
            List<User> userList = new List<User>  {
                new User
                {
                   Address="ADD",
                   Email = "email",
                   Name= "name",
                  PhoneNumber= "000"
                }
            };
            return userList;
        }


        public static List<Pet> GetSeedingPets()
        {
            List<Pet> userList = new List<Pet>  {
                new Pet
                {
                  DateOfVaccine= DateTime.Now,  
                  Age= 1,
                  IsVaccinated= true,
                  Name= "name", 
                  TypeOfPet =" catel",
                  User= users[0],
                  UserId= users[0].Id,
                  Weight= 1
                  
                }
            };
            return userList;
        }

        public static List<Appointment> GetSeedingAppointments()
        {
            List<Appointment> appointmentsList = new List<Appointment>  {
                new Appointment
                {
                  DateTime = DateTime.Now,
                  Doctor= doctors[0],
                  Pet =pets[0],
                  DoctorId = doctors[0].Id,
                  PetId = pets[0].Id

                }
            };
            return appointmentsList;
        }


        public static List<Specialization> GetSeedingSpecialization()
        {
            List<Specialization> specializationList = new List<Specialization>()
           {
                new Specialization
                {
                    Description ="&&",
                    Name = "^^"
                }

            };
            return specializationList;
        }
    }
}
