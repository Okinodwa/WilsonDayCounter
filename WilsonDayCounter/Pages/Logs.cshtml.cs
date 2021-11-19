using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WilsonDayCounter.Business.Models;

namespace WilsonDayCounter.Pages.Shared
{
    public class LogsModel : PageModel
    {

        [BindProperty]
        public IEnumerable<VisitorLog> VisitorLogs { get; set; }

        public void OnGet()
        {
            VisitorLogs = VisitorLog.Read();
        }
    }
}
