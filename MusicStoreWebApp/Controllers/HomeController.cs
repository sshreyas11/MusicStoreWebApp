using Microsoft.AspNetCore.Mvc;
using MusicStoreWebApp.Models;
using MusicStoreWebApp.Services;
using System.Diagnostics;
using System.Net.Http.Headers;
using Newtonsoft.Json;
namespace MusicStoreWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        string Baseurl = "http://localhost:5174";
        public async Task<IActionResult> Index()
        {
            List<AlbumViewModel> albums = new List<AlbumViewModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/AlbumApi");
                if (Res.IsSuccessStatusCode)
                {
                    var response = Res.Content.ReadAsStringAsync().Result;
                    if (response != null)
                    {
                        albums = JsonConvert.DeserializeObject<List<AlbumViewModel>>(response);
                    }

                }
                return View(albums);

            }

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
