using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace Cure.WebAdmin.Controls
{
    using System.Drawing;
    using System.IO;
    using DevExpress.Web.ASPxUploadControl;
    using Utils;

    public partial class PhotoUploader : UserControl
    {
        const string OriginalDirectory = "~/Upload/{0}/";
        const string ThumbDirectory = "~/Upload/{0}/Thumb/";
        const string BigDirectory = "~/Upload/{0}/Big/";

        public Guid ItemGuid
        {
            get
            {
                Guid result;
                if (Guid.TryParse(uxItemIdHidden.Value, out result) || (Cache["ItemGuid"] != null && Guid.TryParse(Cache["ItemGuid"].ToString(), out result)))
                {
                    return result;
                }
                return Guid.Empty;
            }
            set
            {
                uxItemIdHidden.Value = value.ToString();
                Cache["ItemGuid"] = uxItemIdHidden.Value;
            }
        }

        protected void uxUploadControl_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
            if (!ItemGuid.Equals(Guid.Empty)) { e.CallbackData = SavePostedFile(e.UploadedFile); }
        }

        string SavePostedFile(UploadedFile uploadedFile)
        {
            if (!uploadedFile.IsValid)
                return string.Empty;
            string folder = MapPath(String.Format(OriginalDirectory, ItemGuid));
            string folderThumb = MapPath(String.Format(ThumbDirectory, ItemGuid));
            string folderBig = MapPath(String.Format(BigDirectory, ItemGuid));
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            if (!Directory.Exists(folderThumb))
            {
                Directory.CreateDirectory(folderThumb);
            }
            if (!Directory.Exists(folderBig))
            {
                Directory.CreateDirectory(folderBig);
            }
            string fileName = Path.Combine(folder, uploadedFile.FileName);
            string fileThumbName = Path.Combine(folderThumb, uploadedFile.FileName);
            string fileBigName = Path.Combine(folderBig, uploadedFile.FileName);
            using (Image original = Image.FromStream(uploadedFile.FileContent))
            using (Image thumb = PhotoUtils.Inscribe(original, 313, 313))
            using (Image big = PhotoUtils.Inscribe(original, 919, 538))
            {
                PhotoUtils.SaveToJpeg(original, fileName);
                PhotoUtils.SaveToJpeg(thumb, fileThumbName);
                PhotoUtils.SaveToJpeg(big, fileBigName);
            }
            return uploadedFile.FileName;
        }
    }
}