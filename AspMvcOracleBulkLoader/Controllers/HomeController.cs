using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using AspMvcOracleBulkLoader.Models;

namespace AspMvcOracleBulkLoader.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UploadFile(){
            return View();
        }

        public async Task<IActionResult> OnPostUploadAsync(List<IFormFile> files){
            long filesize = files.Sum(f => f.Length);

            foreach (var formfile in files){
                if(formfile.Length>0){
                    var filepath = System.IO.Path.GetTempFileName();
                
                using (var stream = System.IO.File.Create(filepath)){
                    await formfile.CopyToAsync(stream);
                }
                }
            }
                return Ok(new { count = files.Count, filesize });

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
