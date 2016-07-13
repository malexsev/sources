using System.Configuration;
using System.IO;
using Cure.DataAccess;
using System.Collections.Generic;
using System;

namespace Cure.WebSite.Models
{

    public class ChildVisualDetailed : ChildVisual
    {
        public List<ChildHideFile> HiddenPhotoLst { get; set; }
        public List<PhotoItem> PhotoItemLst { get; set; }
        public List<DocItem> DocItemLst { get; set; }

        public ChildVisualDetailed(ViewChild child, IEnumerable<ChildHideFile> hiddenFiles, ChildAvaFile avaFile = null)
            : base(child, avaFile)
        {
            if (child != null)
            {
                this.PhotoItemLst = new List<PhotoItem>();
                this.DocItemLst = new List<DocItem>();
                this.HiddenPhotoLst = new List<ChildHideFile>(hiddenFiles);
                string photoLocation = Path.Combine(ConfigurationManager.AppSettings["PhotoLocation"], this.GuidId.ToString());
                string photoUrl = Path.Combine(ConfigurationManager.AppSettings["PhotoUrl"], this.GuidId.ToString());
                string docsLocation = photoLocation.Replace("Upload", "Documents");
                string docsUrl = photoUrl.Replace("Upload", "Documents");

                //Галлерея
                if (Directory.Exists(photoLocation))
                {
                    var dirInfo = new DirectoryInfo(photoLocation);
                    FileInfo[] fileInfoArray = dirInfo.GetFiles();

                    foreach (FileInfo fileInfo in fileInfoArray)
                    {
                        this.PhotoItemLst.Add(new PhotoItem(photoUrl, fileInfo.Name));
                    }
                }

                //Документы
                if (Directory.Exists(docsLocation))
                {
                    var dirInfo = new DirectoryInfo(docsLocation);
                    FileInfo[] fileInfoArray = dirInfo.GetFiles();

                    foreach (FileInfo fileInfo in fileInfoArray)
                    {
                        this.DocItemLst.Add(new DocItem(docsUrl, fileInfo.Name));
                    }
                }
            }
        }

        public void DeletePhoto(string fileName)
        {
            string photoLocation = Path.Combine(ConfigurationManager.AppSettings["PhotoLocation"], this.GuidId.ToString());
            if (Directory.Exists(photoLocation))
            {
                string filePath = Path.Combine(photoLocation, fileName);
                Utils.FileUtils.DeleteFileFromSubfolders(filePath);
            }
        }

        public void DeleteDoc(string fileName)
        {
            string photoLocation = Path.Combine(ConfigurationManager.AppSettings["PhotoLocation"], this.GuidId.ToString());
            string docsLocation = photoLocation.Replace("Upload", "Documents");
            if (Directory.Exists(docsLocation))
            {
                string filePath = Path.Combine(docsLocation, fileName);
                Utils.FileUtils.DeleteFileFromSubfolders(filePath);
            }
        }
    }
}