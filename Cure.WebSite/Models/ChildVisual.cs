using System.Configuration;
using System.IO;
using Cure.DataAccess;
using System.Collections.Generic;
using System;

namespace Cure.WebSite.Models
{

    public class ChildVisual : ViewChild
    {
        public int Age { get; set; }
        public string PhotoUrl { get; set; }
        public PhotoItem PhotoItem { get; set; }
        public string AvaFileName { get; set; }
        //public List<PhotoItem> PhotoItemLst { get; set; }
        //public List<DocItem> DocItemLst { get; set; }
        public bool IsAnySocial
        {
            get
            {
                return !string.IsNullOrEmpty(this.SocialFb)
                    || !string.IsNullOrEmpty(this.SocialMm)
                    || !string.IsNullOrEmpty(this.SocialOk)
                    || !string.IsNullOrEmpty(this.SocialVk)
                    || !string.IsNullOrEmpty(this.SocialYoutube);
            }
        }
        public string FinWebMoneyString
        {
            get
            {
                var lines = new List<string>();
                if (!string.IsNullOrEmpty(this.FinWebmoney))
                {
                    lines.Add(this.FinWebmoney);
                }
                if (!string.IsNullOrEmpty(this.FinWebmoney2))
                {
                    lines.Add(this.FinWebmoney2);
                }
                if (!string.IsNullOrEmpty(this.FinWebmoney3))
                {
                    lines.Add(this.FinWebmoney3);
                }
                return string.Join(@"<br />", lines);
            }
        }

        public bool CheckForActive()
        {
            bool result = !string.IsNullOrEmpty(this.Name)
                          && !string.IsNullOrEmpty(this.Region)
                          && (!string.IsNullOrEmpty(this.PhotoUrl) && !this.PhotoUrl.Contains("no_photo"));
            return result;
        }


        public ChildVisual(ViewChild child, ChildAvaFile childAvaFile = null)
        {
            if (child != null)
            {
                this.BankBik = child.BankBik;
                this.BankCountryName = child.BankCountryName;
                this.BankDescription = child.BankDescription;
                this.BankInn = child.BankInn;
                this.BankKorrAccount = child.BankKorrAccount;
                this.BankKpp = child.BankKpp;
                this.BankName = child.BankName;
                this.BankOktmo = child.BankOktmo;
                this.BankOkved = child.BankOkved;
                this.Birthday = child.Birthday;
                this.ContactEmail = child.ContactEmail;
                this.ContactName = child.ContactName;
                this.ContactPhone = child.ContactPhone;
                this.ContactRodstvoId = child.ContactRodstvoId;
                this.CountryId = child.CountryId;
                this.CountryName = child.CountryName;
                this.Diagnoz = child.Diagnoz;
                this.DiagnozId = child.DiagnozId;
                this.FinBankId = child.FinBankId;
                this.FinBankOther = child.FinBankOther;
                this.FinCardNumber = child.FinCardNumber;
                this.FinCardName = child.FinCardName;
                this.FinCountryId = child.FinCountryId;
                this.FinCountryName = child.FinCountryName;
                this.FinKiwi = child.FinKiwi;
                this.FinOperatorId = child.FinOperatorId;
                this.FinPhoneNumber = child.FinPhoneNumber;
                this.FinWebmoney = child.FinWebmoney;
                this.FinWebmoney2 = child.FinWebmoney2;
                this.FinWebmoney3 = child.FinWebmoney3;
                this.FinYandexMoney = child.FinYandexMoney;
                this.GuidId = child.GuidId;
                this.Id = child.Id;
                this.IsActive = child.IsActive;
                this.Name = child.Name;
                this.OperatorCountryName = child.OperatorCountryName;
                this.OperatorDescription = child.OperatorDescription;
                this.OperatorName = child.OperatorName;
                this.OperatorParams = child.OperatorParams;
                this.OwnerUser = child.OwnerUser;
                this.Region = child.Region;
                this.RodstvoDescription = child.RodstvoDescription;
                this.RodstvoName = child.RodstvoName;
                this.RodstvoSoprovodChLabel = child.RodstvoSoprovodChLabel;
                this.RodstvoSoprovodRuLabel = child.RodstvoSoprovodRuLabel;
                this.RodstvoSort = child.RodstvoSort;
                this.SocialFb = child.SocialFb;
                this.SocialMm = child.SocialMm;
                this.SocialOk = child.SocialOk;
                this.SocialVk = child.SocialVk;
                this.SocialYoutube = child.SocialYoutube;
                this.UserComment = child.UserComment;
                this.UserEmail = child.UserEmail;
                this.UserIsAnonymous = child.UserIsAnonymous;
                this.UserIsApproved = child.UserIsApproved;
                this.UserIsLockedOut = child.UserIsLockedOut;
                this.UserLastActivityDate = child.UserLastActivityDate;
                this.UserLastLoginDate = child.UserLastLoginDate;
                this.UserLoweredEmail = child.UserLoweredEmail;
                this.UserLoweredUserName = child.UserLoweredUserName;

                PhotoItem = new PhotoItem();
                //PhotoItemLst = new List<PhotoItem>();
                //DocItemLst = new List<DocItem>();
                Age = DateTime.Today.Year - child.Birthday.Year;
                string photoLocation = Path.Combine(ConfigurationManager.AppSettings["PhotoLocation"], this.GuidId.ToString());
                string photoUrl = Path.Combine(ConfigurationManager.AppSettings["PhotoUrl"], this.GuidId.ToString());
                string docsLocation = photoLocation.Replace("Upload", "Documents");
                string docsUrl = photoUrl.Replace("Upload", "Documents");

                //Галлерея
                if (Directory.Exists(photoLocation))
                {
                    var dirInfo = new DirectoryInfo(photoLocation);
                    FileInfo[] fileInfoArray = dirInfo.GetFiles();

                    if (fileInfoArray.Length <= 0)
                    {
                        PhotoUrl = "/Content/images/no_photo_big.jpg";
                    }

                    this.AvaFileName = (childAvaFile == null || childAvaFile.Id == 0)
                        ? string.Empty
                        : childAvaFile.FileName;

                    foreach (FileInfo fileInfo in fileInfoArray)
                    {
                        if ((childAvaFile == null || childAvaFile.Id == 0) || fileInfo.Name == childAvaFile.FileName)
                        {
                            PhotoUrl = Path.Combine(Path.Combine(photoUrl, "Thumb"), fileInfo.Name);
                            PhotoItem = new PhotoItem(photoUrl, fileInfo.Name);
                            break;
                        }
                    }
                    if ((PhotoItem.UrlOriginal == string.Empty || PhotoItem.UrlOriginal.Contains("no_photo")) && fileInfoArray.Length > 0 && !(childAvaFile == null || childAvaFile.Id == 0))
                    {
                        PhotoUrl = Path.Combine(Path.Combine(photoUrl, "Thumb"), fileInfoArray[0].Name);
                        PhotoItem = new PhotoItem(photoUrl, fileInfoArray[0].Name);
                    }
                }
            }
        }
    }
}