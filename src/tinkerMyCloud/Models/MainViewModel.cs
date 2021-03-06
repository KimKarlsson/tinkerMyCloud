﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace tinkerMyCloud.Models
{
    public class MainViewModel
    {
        public List<FileItem> ListFileItem { get; set; }
    }

    public class FileItem
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string URL { get; set; }
        public string Type { get; set; }
        public bool FileIsPrivate { get; set; }
        public DateTime Created { get; set; }
    }


    public class FileSet
    {
        public string Name { get; set; }
        public List<FileBlob> ListFileBlobs { get; set; }
    }

    public class FileBlob
    {
        [Key]
        public int Id { get; set; }
        public string FileName { get; set; }
        public string MimeType { get; set; }
        public byte[] FileData { get; set; }
    }

}
