using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NogetMedThreadLib.services;
using System.ComponentModel.DataAnnotations;

namespace NogetMedThreadsApp.Pages.Threadings
{
    public class CPUBasedModel : PageModel
    {
        private readonly IComputingService _computing;


        public CPUBasedModel(IComputingService cs)
        {
            _computing = cs;
        }


        public long ResultCSnoThread { get; set; }
        public long ResultCSWithThread { get; set; }


        [BindProperty]
        [Range(10, 40, ErrorMessage = "husk kun mellem 10-40 ellers tager det for lang tid")]
        public int Times { get; set; }

        
        [BindProperty]
        public int NoThreads { get; set; }

        public void OnGet()
        {
            ResultCSnoThread = 0;
            ResultCSWithThread = 0;
            NoThreads = 1;
            Times = 10;
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                ResultCSnoThread = _computing.NoThreading(Times);
                ResultCSWithThread = _computing.WithThreading(Times, NoThreads);
            }

            return Page();
        }
    }
}
