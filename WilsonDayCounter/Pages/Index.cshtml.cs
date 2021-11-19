using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WilsonDayCounter.Business.DTO;

namespace WilsonDayCounter.Pages
{
    public class VisitorLogModel : PageModel
    {
        private readonly ILogger<VisitorLogModel> _logger;

        public VisitorLogModel(ILogger<VisitorLogModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        public VisitorLog VisitorLog { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await VisitorLog.Create();

            return Page();
        }
    }
}
