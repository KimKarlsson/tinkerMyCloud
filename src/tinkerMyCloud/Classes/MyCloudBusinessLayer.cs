using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using tinkerMyCloud.Data;
using tinkerMyCloud.Interface;
using tinkerMyCloud.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace tinkerMyCloud.Classes
{
    public class MyCloudBusinessLayer : IMyCloudBusinessLayer
    {

        #region Dependency Injection Constructor
        private readonly ApplicationDbContext _context;

        public MyCloudBusinessLayer(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        // Read (All)
        public Task<List<FileItem>> GetAllFileItems()
        {
            var ReturnList = _context.dbFileItem.ToListAsync();
            return ReturnList;
        }

        // Create (One)
        public async Task<bool> AddFileItemApi(FileItem item)
        {
            bool add = await Add(item);
            if (!add)
            {
                throw new NotImplementedException();
            }
            return add;
        }

        // Test Create No Api (satte uppladdning till 1G)
        public async Task<bool> AddFileNoApi(string file)
        {

            var myFile = new FileItem();
            myFile.URL = file;
            myFile.Created = DateTime.Now;
            myFile.FileIsPrivate = false;
            myFile.Type = "noApiTest";
            myFile.UserId = 1;

            bool add = await Add(myFile);
            return add;

        }


        // Metod för Add
        private async Task<bool> Add(object create)
        {
            try
            {
                _context.Add(create);
                await _context.SaveChangesAsync();
                return true;
            }
            catch { return false; }
        }

        //Exempel json (för tex postman)
        // {"Id":0,"UserId":1,"URL":"/Test/Test","Type":"Test","FileIsPrivate":false,"Created":"2016-11-21T00:00:00"}

        //Exempel "get all"
        // http://localhost:0000/api/Rest 
    }
}
