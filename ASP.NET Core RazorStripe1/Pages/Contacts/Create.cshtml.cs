using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ASP.NET_Core_RazorStripe1.Data;
using ASP.NET_Core_RazorStripe1.Models;

namespace ASP.NET_Core_RazorStripe1.Pages.Contacts
{
    public class CreateModel : PageModel
    {
        private readonly ASP.NET_Core_RazorStripe1.Data.ApplicationDbContext _context;

        public CreateModel(ASP.NET_Core_RazorStripe1.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Contact Contact { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Contacts.Add(Contact);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}