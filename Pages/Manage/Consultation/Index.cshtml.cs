using Capstonep2.Infrastructure.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using Capstonep2.ViewModel;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Capstonep2.Infrastructure.Domain;

namespace Capstonep2.Pages.Manage.Consultation
{
    [Authorize(Roles = "admin")]
    public class IndexModel : PageModel
    {
        private ILogger<System.Index> _logger;
        private DefaultDBContext _context;

        [BindProperty]
        public ViewModel View { get; set; }

        public IndexModel(DefaultDBContext context, ILogger<System.Index> logger)
        {
            _logger = logger;
            _context = context;
            View = View ?? new ViewModel();
        }
        public void OnGet(Guid? id = null)

        {

            var appts = _context?.Appointments?.Where(a => a.ID == id).Include(a => a.Patient).ToList();
            View.Appointments = appts;



        }
        public IActionResult OnPost()
        {
            return RedirectPermanent("~/Manage/Admin/Dashboard");
        }




        public class ViewModel : CRViewModel
        {
            public List<Appointment>? Appointments { get; set; }
        }
    }
}


