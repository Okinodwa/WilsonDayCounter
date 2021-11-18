using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WilsonDayCounter.Pages
{
    public class VisitorLogModel : PageModel
    {
        private readonly ILogger<VisitorLogModel> _logger;

        public VisitorLogModel(ILogger<VisitorLogModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
