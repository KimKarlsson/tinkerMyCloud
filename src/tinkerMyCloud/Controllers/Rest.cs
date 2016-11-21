using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tinkerMyCloud.Interface;
using tinkerMyCloud.Models;

namespace tinkerMyCloud.Controllers
{
    [Route("api/[Controller]")]
    public class Rest : Controller
    {
        private readonly IMyCloudBusinessLayer _businessLayer;
        public Rest(IMyCloudBusinessLayer businessLayer)
        {
            _businessLayer = businessLayer;
        }

        //Read (All)
        [HttpGet]
        public async Task<string> GetAll()
        {
            List<FileItem> returnList = await _businessLayer.GetAllFileItems();
            if (returnList == null || returnList.Count <= 0)
            {
                return "empty";
            }
            else
            {
                var json = JsonConvert.SerializeObject(returnList);
                return json;
            }
        }

        // Create (One)
        [HttpPost]
        public async Task<IActionResult> CreateProd([FromBody]FileItem item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            bool add = await _businessLayer.AddFileItemApi(item);
            if (add)
            {
                return new ObjectResult("Sucess");
            }
            else
            {
                return new ObjectResult("Api error - when trying to add object.");
            }
        }

    }
}
