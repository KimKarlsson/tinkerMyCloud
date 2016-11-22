using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using tinkerMyCloud.Models;

namespace tinkerMyCloud.Interface
{
    public interface IMyCloudBusinessLayer
    {
        Task<List<FileItem>> GetAllFileItems();
        Task<bool> AddFileItemApi(FileItem item);
        Task<bool> AddFileNoApi(string file);
        Task<bool> AddFileUsingExAPI(FileBlob item);
    }
}
