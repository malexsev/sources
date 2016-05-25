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
        public List<PhotoItem> PhotoItemLst { get; set; }


        public ChildVisual(ViewChild child)
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
                this.FinBankId = child.FinBankId;
                this.FinBankOther = child.FinBankOther;
                this.FinCardNumber = child.FinCardNumber;
                this.FinCountryId = child.FinCountryId;
                this.FinCountryName = child.FinCountryName;
                this.FinKiwi = child.FinKiwi;
                this.FinOperatorId = child.FinOperatorId;
                this.FinPhoneNumber = child.FinPhoneNumber;
                this.FinWebmoney = child.FinWebmoney;
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
                this.UserComment = child.UserComment;
                this.UserEmail = child.UserEmail;
                this.UserIsAnonymous = child.UserIsAnonymous;
                this.UserIsApproved = child.UserIsApproved;
                this.UserIsLockedOut = child.UserIsLockedOut;
                this.UserLastActivityDate = child.UserLastActivityDate;
                this.UserLastLoginDate = child.UserLastLoginDate;
                this.UserLoweredEmail = child.UserLoweredEmail;
                this.UserLoweredUserName = child.UserLoweredUserName;

                Age = DateTime.Today.Year - child.Birthday.Year;
                PhotoItemLst = new List<PhotoItem>();
                string photoLocation = Path.Combine(ConfigurationManager.AppSettings["PhotoLocation"], this.GuidId.ToString());
                string photoUrl = Path.Combine(ConfigurationManager.AppSettings["PhotoUrl"], this.GuidId.ToString());

                if (Directory.Exists(photoLocation))
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(photoLocation);
                    FileInfo[] fileInfoArray = dirInfo.GetFiles();

                    if (fileInfoArray.Length > 0)
                    {
                        PhotoUrl = Path.Combine(photoUrl, fileInfoArray[0].Name);
                    }
                    else
                    {
                        PhotoUrl = "/Content/images/no_photo_big.jpg";
                    }

                    foreach (FileInfo fileInfo in fileInfoArray)
                    {
                        if (PhotoItem == null)
                        {
                            PhotoItem = new PhotoItem(photoUrl, fileInfo.Name);
                        }
                        PhotoItemLst.Add(new PhotoItem(photoUrl, fileInfo.Name));
                    }
                }
            }

            if (PhotoItem == null)
            {
                PhotoItem = new PhotoItem();
            }
        }
    }
}