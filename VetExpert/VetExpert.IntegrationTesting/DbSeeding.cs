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
        public static List<Admin> admins = new List<Admin>();        
        public static List<Appointment> appointments = new List<Appointment>();
        public static List<Bill> bills = new List<Bill>();  
        public static List<Clinic> clinics= new List<Clinic>();
        public static List<Doctor> doctors = new List<Doctor>();
        public static List<Drug> drugs = new List<Drug>();
        public static List<DrugStock> drugStocks = new List<DrugStock>();     
        public static List<Pet> pets = new List<Pet>();
        public static List<Specialization> specializations = new List<Specialization>();
        public static List<User> users = new List<User>();

        public static void InitializeDbForTests(MainDbContext db)
        {
            //users
            users = GetSeedingUsers();
            db.Users.AddRange(users);

            //pets
            pets = GetSeedingPets();
            db.Pets.AddRange(pets);

            //clinics
            clinics=GetSeedingClinics();
            db.Clinics.AddRange(clinics);

            //doctors
            doctors = GetSeedingDoctors();
            db.Doctors.AddRange(doctors);

            //appointments
            appointments = GetSeedingAppointments();
            db.Appointments.AddRange(appointments);

            //promotions
            db.Promotions.AddRange(GetSeedingPromotions());

            //specializations
            specializations = GetSeedingSpecialization();
            db.Specializations.AddRange(specializations);

            //admins
            admins = GetSeedingAdmins();
            db.Admins.AddRange(admins);

             //drugs
            drugs = GetSeedingDrugs();
            db.Drugs.AddRange(drugs);

            //bills
            bills = GetSeedingBills();
            db.Bills.AddRange(bills);

            //drugStocks
            drugStocks = GetSeedingDrugStocks();
            db.DrugStocks.AddRange(drugStocks);

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
            db.Admins.RemoveRange(db.Admins);
            db.Bills.RemoveRange(db.Bills);

            InitializeDbForTests(db);
        }

        public static List<Admin> GetSeedingAdmins()
        {
            return new List<Admin>()
            {
                new Admin
                {
                    UserName = "admin1"
                }

            };
        }

        public static List<Drug> GetSeedingDrugs()
        {
            return new List<Drug>()
            {
                new Drug
                {
                    Name = "Vetomune Twist Off",
                    Manufacturer = "VetExpert",
                    Weight = 120,
                    Prospect = "VetoMune 120mg este un produs care sprijina imbunatatirea imunitatii non-specifice la caini si pisici",
                    Price = 130
                }

            };
        }


        public static List<DrugStock> GetSeedingDrugStocks()
        {
            return new List<DrugStock>()
            {
                new DrugStock
                {
                    Quantity = 12,
                    ExpirationDate = new DateTime(2023, 1, 23),
                    DrugId = drugs[0].Id,
                    Drug = drugs[0]
                }

            };
        }

        public static List<Bill> GetSeedingBills()
        {
            return new List<Bill>()
            {
                new Bill
                {
                   Drugs = drugs,
                   UserId = users[0].Id,
                   ClinicId = clinics[0].Id,
                   Clinic = clinics[0]
                }

            };
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
