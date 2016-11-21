using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace tinkerMyCloud.Controllers
{
    public class HomeController : Controller
    {
        private IHostingEnvironment _environment;

        public HomeController(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FileUpLoad(IFormFile file, [FromServices]Interface.IMyCloudBusinessLayer _businessLayer)
        {
            //http://www.mikesdotnetting.com/article/288/uploading-files-with-asp-net-core-1-0-mvc
            var uploads = Path.Combine(_environment.WebRootPath, "/Content/Files/");
            uploads = @"C:\Projekt\tinkerMyCloud\src\tinkerMyCloud\Content\Files"; //TODO: har inte löst motsvarigheten för "var path = Path.Combine(Server.MapPath("~/Content/Files/"), fileName)" i core än. 
            //http://www.mikesdotnetting.com/article/302/server-mappath-equivalent-in-asp-net-core
            if (file == null)
            {
                throw new NotImplementedException();
            }
            else if (file.Length > 0)
            {
                using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }
            string saveNameUrl = file.FileName;
            bool testAddNoApi = await _businessLayer.AddFileNoApi(saveNameUrl);
            return View("Index");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
