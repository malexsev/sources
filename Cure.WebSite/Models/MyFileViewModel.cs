using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cure.WebSite.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Configuration;
    using System.IO;
    using System.Web.Mvc;
    using DataAccess;

    public class MyFileViewModel
    {
        const string OriginalDirectory = @"{0}{1}\UserFiles\{2}";
        [Key]
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Created { get; set; }
        public string Modified { get; set; }
        public string MimeType { get; set; }
        public string VirtualPath { get; set; }
        
        public MyFileViewModel()
        {
        }

        public MyFileViewModel(FileInfo fileInfo, Guid guid)
        {
            this.Name = fileInfo.Name;
            this.FullName = fileInfo.FullName;
            this.MimeType = MimeMapping.GetMimeMapping(this.FullName);
            this.Created = fileInfo.CreationTime.ToString("dd.MM.yyyy");
            this.Modified = fileInfo.LastWriteTime.ToString("dd.MM.yyyy HH:mm");

            string photoUrl = ConfigurationManager.AppSettings["PhotoUrl"];
            string location = photoUrl.Replace("Upload", "Documents");

            this.VirtualPath = string.Format(OriginalDirectory, location, guid, this.Name); ;
        }
    }
}