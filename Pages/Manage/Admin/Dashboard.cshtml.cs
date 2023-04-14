using Capstonep2.Infrastructure.Domain.Security;
using Capstonep2.Infrastructure.Domain;
using Capstonep2.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using Capstonep2.Infrastructure.Domain.Models;
using System;
using Capstonep2.Infrastructure.Domain.Models.Enums;

namespace Capstonep2.Pages.Manage.Admin
{
    [Authorize(Roles = "admin")]
    public class DashboardModel : PageModel
    {
        private ILogger<Index> _logger;
        private DefaultDBContext _context;
        [BindProperty]
        public ViewModel View { get; set; }




        public DashboardModel(DefaultDBContext context, ILogger<Index> logger)
        {
            _logger = logger;
            _context = context;
            View = View ?? new ViewModel();
        }

        public IActionResult OnGet(Guid? id = null, Guid? crid = null)
        {
            Guid? userId = User.Id();
            var user = _context?.Users?.Where(a => a.ID == id).FirstOrDefault();

            







            var profile = _context?.Users?.Where(a => a.ID == userId)
                .Select(a => new ViewModel()
                {

                    Address = a.Address,
                    BirthDate = a.BirthDate,
                    Email = a.Email,
                    FirstName = a.FirstName,
                    Gender = a.Gender,
                    LastName = a.LastName,
                    MiddleName = a.MiddleName,
                }).FirstOrDefault();

            ViewData["address"] = profile.Address;
            ViewData["birthdate"] = profile.BirthDate;
            ViewData["email"] = profile.Email;
            ViewData["firstname"] = profile.FirstName;
            ViewData["middlename"] = profile.MiddleName;
            ViewData["lastname"] = profile.LastName;
            ViewData["gender"] = profile.Gender;

            View = profile;
            var appointments = _context?.Appointments?.Include(a => a.Patient).ToList();
            View.Appointments = appointments;
            var patients = _context?.Patients?.ToList();
            View.Patients = patients;
            var consultations = _context?.ConsultationRecords?.ToList();
            View.ConsultationRecords = consultations;
            var findings = _context?.Findings.ToList();
            var prescriptions = _context?.Prescriptions.ToList();
            View.Findings = findings;
            View.Prescriptions = prescriptions;


            return Page();
        }
        public IActionResult OnPostChangeProfile()
        {
            if (string.IsNullOrEmpty(View.FirstName))
            {
                ModelState.AddModelError("", "Role name cannot be blank.");
                return Page();
            }
            if (string.IsNullOrEmpty(View.MiddleName))
            {
                ModelState.AddModelError("", "Role name cannot be blank.");
                return Page();
            }
            if (string.IsNullOrEmpty(View.LastName))
            {
                ModelState.AddModelError("", "Role name cannot be blank.");
                return Page();
            }

            if (string.IsNullOrEmpty(View.Address))
            {
                ModelState.AddModelError("", "Description cannot be blank.");
                return Page();
            }


            var existingPatient = _context?.Patients?.FirstOrDefault(a =>
                    a.FirstName.ToLower() == View.FirstName.ToLower() &&
                    a.LastName.ToLower() == View.LastName.ToLower() &&
                    a.MiddleName.ToLower() == View.MiddleName.ToLower() &&
                    a.Address.ToLower() == View.Address.ToLower()


            );

            if (existingPatient != null)
            {
                ModelState.AddModelError("", "Patient is already existing.");
                return Page();
            }

            var user = _context?.Users?.FirstOrDefault(a => a.ID == User.Id());


            if (user != null)
            {


                user.FirstName = View.FirstName;
                user.MiddleName = View.MiddleName;
                user.LastName = View.LastName;
                user.Address = View.Address;




                _context?.Users?.Update(user);
                _context?.SaveChanges();

                return RedirectPermanent("~/Manage/Patient/Dashboard");
            }

            return Page();

        }

        public IActionResult OnPostChangePass()
        {

            if (string.IsNullOrEmpty(View.CurrentPass))
            {
                ModelState.AddModelError("", "Field Required");
                return Page();
            }

            if (string.IsNullOrEmpty(View.NewPass))
            {
                ModelState.AddModelError("", "Field Required");
                return Page();
            }

            if (string.IsNullOrEmpty(View.RetypedPassword))
            {
                ModelState.AddModelError("", "Field Required");
                return Page();
            }

            if (View.NewPass != View.RetypedPassword)
            {
                ModelState.AddModelError("", "Passwords are not the same");
                return Page();
            }


            var passwordInfo = _context?.UserLogins?.FirstOrDefault(a => a.UserID == User.Id() && a.Key.ToLower() == "password");

            if (passwordInfo != null)
            {
                if (BCrypt.Net.BCrypt.EnhancedVerify(View.CurrentPass, passwordInfo.Value))
                {
                    var userRole = _context?.UserRoles?.Include(a => a.Role)!.FirstOrDefault(a => a.UserID == User.Id());

                    passwordInfo.Value = BCrypt.Net.BCrypt.EnhancedHashPassword(View.NewPass);
                    _context?.UserLogins?.Update(passwordInfo);
                    _context?.SaveChanges();

                    if (userRole!.Role!.Name.ToLower() == "admin")
                    {
                        return RedirectPermanent("/manage/admin/dashboard");
                    }
                    else
                    {
                        return RedirectPermanent("/dashboard/patient");
                    }
                }
            }
            return RedirectPermanent("/manage/admin/dashboard");
        }

        public IActionResult OnPostEdit()
        {
            if (string.IsNullOrEmpty(View.EditFirstName))
            {
                ModelState.AddModelError("", "First name cannot be blank.");
                return RedirectPermanent("/manage/admin/dashboard");
            }

            if (string.IsNullOrEmpty(View.EditMiddleName))
            {
                ModelState.AddModelError("", "Middle name cannot be blank.");
                return RedirectPermanent("/manage/admin/dashboard");
            }
            if (string.IsNullOrEmpty(View.EditLastName))
            {
                ModelState.AddModelError("", "Last name cannot be blank.");
                return RedirectPermanent("/manage/admin/dashboard");
            }
          

            if (string.IsNullOrEmpty(View.EditAddress))
            {
                ModelState.AddModelError("", "Address name cannot be blank.");
                return RedirectPermanent("/manage/admin/dashboard");
            }



            var patient = _context?.Patients?.FirstOrDefault(a => a.ID == Guid.Parse(View.EditPatientId));

            if (patient != null)
            {

                patient.FirstName = View.EditFirstName;
                patient.MiddleName = View.EditMiddleName;
                patient.LastName = View.EditLastName;
                patient.Address = View.EditAddress;

                _context?.Patients?.Update(patient);

                _context?.SaveChanges();

                return RedirectPermanent("~/manage/admin/dashboard");
            }







            return RedirectPermanent("/manage/admin/dashboard");
        }


        public IActionResult OnPostNewuser()
        {
            if (string.IsNullOrEmpty(View.NewFirstName))
            {
                ModelState.AddModelError("", "First name cannot be blank.");
                return Page();
            }

            if (string.IsNullOrEmpty(View.NewMiddleName))
            {
                ModelState.AddModelError("", "Middle name cannot be blank.");
                return Page();
            }
            if (string.IsNullOrEmpty(View.NewLastName))
            {
                ModelState.AddModelError("", "Last name cannot be blank.");
                return Page();
            }
            if (!Enum.IsDefined(typeof(Gender), View.NewGender))
            {
                ModelState.AddModelError("", "Sex name cannot be blank.");
                return Page();
            }
            if (DateTime.MinValue >= View.NewBirthDate)
            {
                ModelState.AddModelError("", "Birthdate name cannot be blank.");
                return Page();
            }

            if (string.IsNullOrEmpty(View.NewAddress))
            {
                ModelState.AddModelError("", "Address name cannot be blank.");
                return Page();
            }
            if (string.IsNullOrEmpty(View.NewEmail))
            {
                ModelState.AddModelError("", "Email cannot be blank.");
                return Page();
            }
            if (string.IsNullOrEmpty(View.NewPassword))
            {
                ModelState.AddModelError("", "Password cannot be blank.");
                return Page();
            }







            Guid userGuid = Guid.NewGuid();
            User newUser = new User()
            {
                ID = userGuid,
                FirstName = View.NewFirstName,
                MiddleName = View.NewMiddleName,
                LastName = View.NewLastName,
                Gender = View.NewGender,
                BirthDate = View.NewBirthDate,
                Address = View.NewAddress,
                Email = View.NewEmail,
                RoleID = Guid.Parse("54f00f70-72b8-4d34-a953-83321ff6b101")
            };
            List<UserLogin> userLogins = new List<UserLogin>();
            userLogins.AddRange(new List<UserLogin>()
            {
                new UserLogin()
                {
                    ID = Guid.NewGuid(),
                    UserID =userGuid,
                    Type = "General",
                    Key = "Password",
                    Value = BCrypt.Net.BCrypt.EnhancedHashPassword(View.NewPassword)
                },
                new UserLogin()
                {
                    ID = Guid.NewGuid(),
                    UserID =userGuid,
                    Type = "General",
                    Key = "IsActive",
                    Value = "true"
                },
                new UserLogin()
                {
                    ID = Guid.NewGuid(),
                    UserID =userGuid,
                    Type = "General",
                    Key = "LoginRetries",
                    Value = "0"
                }
            });

            UserRole userRole = new UserRole()
            {
                Id = Guid.NewGuid(),
                UserID = userGuid,
                RoleID = Guid.Parse("54f00f70-72b8-4d34-a953-83321ff6b101")

            };


            _context?.Users?.Add(newUser);

            _context?.UserLogins?.AddRange(userLogins);
            _context?.UserRoles?.Add(userRole);
            _context?.SaveChanges();

            View.FirstName = View.FirstName;
            View.LastName = View.LastName;
            View.Email = View.Email;    
            View.Address = View.Address;
            View.BirthDate = View.BirthDate;
            View.MiddleName = View.MiddleName;
            View.Gender = View.Gender;
            

            return Page();
        }


        public class ViewModel : UserViewModel
        {
            public string? CurrentPass { get; set; }
            public string? NewPass { get; set; }
            public string? RetypedPassword { get; set; }
            public Guid? ID { get; set; }

            [ForeignKey("PatientID")]
            public Infrastructure.Domain.Models.Patient? Patient { get; set; }


            public string? NewFirstName { get; set;}
            public string? NewLastName { get; set;} 
            public string? NewMiddleName { get; set; }
            public string? NewEmail { get; set; }

            public string? NewAddress { get; set; }
            public string? NewPassword { get; set; }
            public DateTime NewBirthDate { get; set; }
            public Infrastructure.Domain.Models.Enums.Gender NewGender { get; set; }
            public List<Appointment>?Appointments { get; set; }
            public List<ConsultationRecord>? ConsultationRecords { get; set; }
            public List<Infrastructure.Domain.Models.Patient>? Patients { get; set; }
            public List<Finding>? Findings { get; set; }
            public List<Prescription>? Prescriptions { get; set; }


         
            public string? SymptomID { get; set; }
            public string? StatusId { get; set; }




            public string? EditFirstName { get; set; }
            public string? EditLastName { get; set; }
            public string? EditMiddleName { get; set; }
            public string? EditAddress { get; set; }
            public string? EditPatientId { get; set; }
        }
    }
}
