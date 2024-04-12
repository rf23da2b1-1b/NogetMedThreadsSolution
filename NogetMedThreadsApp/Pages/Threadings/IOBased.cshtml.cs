using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NogetMedThreadLib.model;
using NogetMedThreadLib.services;

namespace NogetMedThreadsApp.Pages.Threadings
{
    public class IOBasedModel : PageModel
    {
        private ISillyHotelRepository _repo;
        private const int NO_OF_HOTELS = 50;

        public IOBasedModel(ISillyHotelRepository repo)
        {
            _repo = repo;
        }


        [BindProperty]
        public int NoOfThreads { get; set; }

        public List<Hotel> Hotels { get; set; }


        public void OnGet()
        {
            Hotels = new List<Hotel>();
        }

        public IActionResult OnPostClear()
        {
            Hotels = new List<Hotel>();
            return Page();
        }

        public IActionResult OnPostUden()
        {
            Hotels = new List<Hotel>();
            for (int i = 1; i <= NO_OF_HOTELS; i++)
            {
                Hotel hotel = _repo.GetById(i);
                Hotels.Add(hotel);
            }

            return Page();
        }

        public IActionResult OnPostMed()
        {
            Hotels = new List<Hotel>();

            // Insert your code here
            int AntalIdPrThread = NO_OF_HOTELS / (NoOfThreads-1);
            int Rest  = NO_OF_HOTELS % (NoOfThreads-1);

            List<Task> tasks = new List<Task>();

            for (int i = 0; i < (NoOfThreads-1); i++)
            {
                int fra = i * AntalIdPrThread + 1;
                int til = i * AntalIdPrThread + AntalIdPrThread;

                Task t = Task.Run(
                    () => FindHotellerIInterval(Hotels, fra, til)
                    );
                tasks.Add(t);
            }

            int fraRs = (NoOfThreads - 1) * AntalIdPrThread +1;
            int tilRs = (NoOfThreads - 1) * AntalIdPrThread + Rest;

            Task tr = Task.Run(
                () => FindHotellerIInterval(Hotels, fraRs, tilRs)
                );
            tasks.Add(tr);
            Task.WaitAll(tasks.ToArray());

            Hotels.Sort();
            return Page();
        }

        private object lockObj = new object();
        private void FindHotellerIInterval(List<Hotel> hotels, int fra, int til)
        {
            for (int i = fra; i <= til; i++)
            {
                Hotel hotel = _repo.GetById(i);
                lock (lockObj)
                {
                    hotels.Add(hotel);
                }
            }
        }
    }
}
