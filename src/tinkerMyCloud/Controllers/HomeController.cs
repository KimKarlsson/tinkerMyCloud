using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using tinkerMyCloud.Models;
using Newtonsoft.Json;

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

        //Ladda up fil till disk
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

        //Ladda upp fil till blob via API
        [HttpPost]
        public async Task<IActionResult> FileUpLoadAPI(IFormFile fileAPI)
        {
            var uploads = Path.Combine(_environment.WebRootPath, "/Temp/");
            uploads = @"C:\Projekt\tinkerMyCloud\src\tinkerMyCloud\Temp"; //TODO: relativ sökväg
            if (fileAPI == null)
            {
                throw new NotImplementedException();
            }
            else if (fileAPI.Length > 0)
            {
                byte[] array = new byte[0];
                using (var fileStream = new FileStream(Path.Combine(uploads, fileAPI.FileName), FileMode.Create))
                {
                    await fileAPI.CopyToAsync(fileStream);

                    // Skapa array 
                    byte[] fileArray = new byte[fileStream.Length];

                    // konventera stream till array
                    fileStream.Read(fileArray, 0, System.Convert.ToInt32(fileStream.Length));

                    // sätt array utanför scope
                    array = fileArray;
                }

                // skicka api..
                var myfile = new FileBlob();
                myfile.Id = 0;
                myfile.FileName = "testNshit";
                myfile.MimeType = "kgbShitTypeOfFile";
                myfile.FileData = array;

                var jsondata = JsonConvert.SerializeObject(myfile);

                var uri = new Uri("http://localhost:1729/api/Rest");

                var client = new System.Net.Http.HttpClient();

                System.Net.Http.HttpContent contentPost = new System.Net.Http.StringContent(jsondata, System.Text.Encoding.UTF8, "application/json");
                try
                {
                    var response = client.PostAsync(uri, contentPost);
                }
                catch (Exception)
                {
                    throw new NotImplementedException();
                }

                //TODO: tabort temp fil som sparas till stream..
            }
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
