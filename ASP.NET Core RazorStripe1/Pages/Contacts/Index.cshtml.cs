using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ASP.NET_Core_RazorStripe1.Data;
using ASP.NET_Core_RazorStripe1.Models;

namespace ASP.NET_Core_RazorStripe1.Pages.Contacts
{
    public class IndexModel : PageModel
    {
        private readonly ASP.NET_Core_RazorStripe1.Data.ApplicationDbContext _context;

        public IndexModel(ASP.NET_Core_RazorStripe1.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Contact> Contact { get; set; }

        public async Task OnGetAsync()
        {
            Contact = await _context.Contacts.ToListAsync();
        }
    }
}
