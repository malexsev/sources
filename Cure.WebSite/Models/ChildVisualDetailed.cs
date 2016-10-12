using System.Configuration;
using System.IO;
using Cure.DataAccess;
using System.Collections.Generic;
using System;

namespace Cure.WebSite.Models
{
    using System.Linq;

    public class ChildVisualDetailed : ChildVisual
    {
        public List<ChildHideFile> HiddenPhotoList { get; set; }
        public List<PhotoItem> PhotoItemList { get; set; }
        public List<DocItem> DocItemList { get; set; }
        public List<PostModel> PostItemList { get; set; }
        public DocItem Userpic { get; set; }

        public ChildVisualDetailed(ViewChild child, IEnumerable<ChildHideFile> hiddenFiles, ChildAvaFile avaFile = null, IEnumerable<Post> posts = null)
            : base(child, avaFile)
        {
            if (child != null)
            {
                this.PhotoItemList = new List<PhotoItem>();
                this.DocItemList = new List<DocItem>();
                this.PostItemList = new List<PostModel>();
                this.HiddenPhotoList = new List<ChildHideFile>(hiddenFiles);
                this.Userpic = new DocItem();
                string photoLocation = Path.Combine(ConfigurationManager.AppSettings["PhotoLocation"], this.GuidId.ToString());
                string photoUrl = Path.Combine(ConfigurationManager.AppSettings["PhotoUrl"], this.GuidId.ToString());
                string docsLocation = photoLocation.Replace("Upload", "Documents");
                string docsUrl = photoUrl.Replace("Upload", "Documents");
                string userpicLocation = photoLocation.Replace("Upload", "Userpics");
                string userpicUrl = photoUrl.Replace("Upload", "Userpics");

                //Галлерея
                if (Directory.Exists(photoLocation))
                {
                    var dirInfo = new DirectoryInfo(photoLocation);
                    FileInfo[] fileInfoArray = dirInfo.GetFiles();

                    foreach (FileInfo fileInfo in fileInfoArray)
                    {
                        this.PhotoItemList.Add(new PhotoItem(photoUrl, fileInfo.Name));
                    }
                }

                //Документы
                if (Directory.Exists(docsLocation))
                {
                    var dirInfo = new DirectoryInfo(docsLocation);
                    FileInfo[] fileInfoArray = dirInfo.GetFiles();

                    foreach (FileInfo fileInfo in fileInfoArray)
                    {
                        this.DocItemList.Add(new DocItem(docsUrl, fileInfo.Name));
                    }
                }

                //Юзерпики
                if (Directory.Exists(userpicLocation))
                {
                    var dirInfo = new DirectoryInfo(userpicLocation);
                    FileInfo[] fileInfoArray = dirInfo.GetFiles();

                    foreach (FileInfo fileInfo in fileInfoArray)
                    {
                        this.Userpic = new DocItem(userpicUrl, fileInfo.Name);
                    }
                }

                //Лента впечатлений
                if (posts != null)
                {
                    var enumerable = posts as IList<Post> ?? posts.ToList();
                    this.PostItemList = enumerable.Select(x => new PostModel(x)).ToList();
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

        public void DeleteUserpic(string fileName)
        {
            string photoLocation = Path.Combine(ConfigurationManager.AppSettings["PhotoLocation"], this.GuidId.ToString());
            string userpicLocation = photoLocation.Replace("Upload", "Userpics");
            if (Directory.Exists(userpicLocation))
            {
                string filePath = Path.Combine(userpicLocation, fileName);
                Utils.FileUtils.DeleteFileFromSubfolders(filePath);
            }
        }
    }
}