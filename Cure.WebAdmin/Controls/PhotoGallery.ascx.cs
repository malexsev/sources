using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cure.WebAdmin.Controls
{
    using System.IO;

    public partial class PhotoGalery : System.Web.UI.UserControl
    {
        const string SourceDirectory = "~/Upload/{0}/";

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public Guid ItemGuid
        {
            get
            {
                Guid result;
                if (Guid.TryParse(uxItemIdHidden.Value, out result))
                {
                    return result;
                }
                return Guid.Empty;
            }
            set
            {
                uxItemIdHidden.Value = value.ToString();
                ShowPictures();
            }
        }

        public void RemoveFolder(Guid folderId)
        {
            string sourceDirectory = MapPath(String.Format(SourceDirectory, folderId));
            Utils.FileUtils.DeleteFolder(sourceDirectory);
        }

        public void RemovePhotoByIndex(int index)
        {
            string relSourceDirectory = String.Format(SourceDirectory, ItemGuid);
            string sourceDirectory = MapPath(relSourceDirectory);

            string file = Directory.GetFiles(sourceDirectory)[index];

            Utils.FileUtils.DeleteFileFromSubfolders(file);
            ShowPictures();
        }

        protected void ShowPictures()
        {
            string relSourceDirectory = String.Format(SourceDirectory, ItemGuid);
            string sourceDirectory = MapPath(relSourceDirectory);
            if (Directory.Exists(sourceDirectory))
            {
                uxImageGallery.SettingsFolder.ImageSourceFolder = relSourceDirectory;
                uxImageGallery.SettingsFolder.ImageCacheFolder = relSourceDirectory;
            }
        }
    }
}