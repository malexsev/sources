using System.Configuration;
using System.IO;
using Cure.DataAccess;
using System.Collections.Generic;
using System;

namespace Cure.WebSite.Models
{
    using System.Globalization;
    using Utils;

    public class ChildVisual : ViewChild
    {
        public string Age { get; set; }
        public string PhotoUrl { get; set; }
        public PhotoItem PhotoItem { get; set; }
        public string AvaFileName { get; set; }
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
                if (!string.IsNullOrEmpty(this.FinWebmoney4))
                {
                    lines.Add(this.FinWebmoney4);
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

        public static string NoPhotoUrl;

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
                this.Birthday = child.Birthday.Year == 2111 ? DateTime.Today : child.Birthday;
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
                this.FinOperator2Id = child.FinOperator2Id;
                this.FinOperator3Id = child.FinOperator3Id;
                this.FinOperator4Id = child.FinOperator4Id;
                this.FinPhoneNumber = child.FinPhoneNumber;
                this.FinPhoneNumber2 = child.FinPhoneNumber2;
                this.FinPhoneNumber3 = child.FinPhoneNumber3;
                this.FinPhoneNumber4 = child.FinPhoneNumber4;
                this.FinWebmoney = child.FinWebmoney;
                this.FinWebmoney2 = child.FinWebmoney2;
                this.FinWebmoney3 = child.FinWebmoney3;
                this.FinWebmoney4 = child.FinWebmoney4;
                this.FinYandexMoney = child.FinYandexMoney;
                this.GuidId = child.GuidId;
                this.Id = child.Id;
                this.IsActive = child.IsActive;
                this.Name = child.Name;
                this.OperatorDescription = child.OperatorDescription;
                this.OperatorName = child.OperatorName;
                this.OperatorParams = child.OperatorParams;
                this.Operator2Description = child.Operator2Description;
                this.Operator2Name = child.Operator2Name;
                this.Operator2Params = child.Operator2Params;
                this.Operator3Description = child.Operator3Description;
                this.Operator3Name = child.Operator3Name;
                this.Operator3Params = child.Operator3Params;
                this.Operator4Description = child.Operator4Description;
                this.Operator4Name = child.Operator4Name;
                this.Operator4Params = child.Operator4Params;
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

                ChildVisual.NoPhotoUrl = string.Format("/Content/img/userpics/no_photo_{0}.jpg",
                    SiteUtils.GetRandom(this.Id, 3));

                this.PhotoItem = new PhotoItem(this.Id);
                //PhotoItemLst = new List<PhotoItem>();
                //DocItemLst = new List<DocItem>();
                int age = DateTime.Today.Year - child.Birthday.Year;
                string let = "лет";
                if (age > 1 && age < 5)
                {
                    let = "года";
                }
                else if (age == 1)
                {
                    let = "год";
                }
                Age = child.Birthday.Year == 2111 ? "Возраст не указан" : string.Format("{0} {1}", age.ToString(CultureInfo.InvariantCulture), let);
                string photoLocation = Path.Combine(ConfigurationManager.AppSettings["PhotoLocation"], this.GuidId.ToString());
                string photoUrl = Path.Combine(ConfigurationManager.AppSettings["PhotoUrl"], this.GuidId.ToString());
                string docsLocation = photoLocation.Replace("Upload", "Documents");
                string docsUrl = photoUrl.Replace("Upload", "Documents");

                //Галлерея
                this.AvaFileName = (childAvaFile == null || childAvaFile.Id == 0)
                    ? string.Empty
                    : childAvaFile.FileName;

                if (!string.IsNullOrEmpty(AvaFileName))
                {
                    this.PhotoUrl = Path.Combine(Path.Combine(photoUrl, "Thumb"), AvaFileName);
                    this.PhotoItem = new PhotoItem(photoUrl, AvaFileName);
                }
                else
                {
                    if (Directory.Exists(photoLocation))
                    {
                        var dirInfo = new DirectoryInfo(photoLocation);
                        FileInfo[] fileInfoArray = dirInfo.GetFiles();

                        if (fileInfoArray.Length <= 0)
                        {
                            this.PhotoUrl = ChildVisual.NoPhotoUrl;
                        }


                        foreach (FileInfo fileInfo in fileInfoArray)
                        {
                            if ((childAvaFile == null || childAvaFile.Id == 0) || fileInfo.Name == childAvaFile.FileName)
                            {
                                this.PhotoUrl = Path.Combine(Path.Combine(photoUrl, "Thumb"), fileInfo.Name);
                                this.PhotoItem = new PhotoItem(photoUrl, fileInfo.Name);
                                break;
                            }
                        }
                        if ((PhotoItem.UrlOriginal == string.Empty || PhotoItem.UrlOriginal.Contains("no_photo")) && fileInfoArray.Length > 0 && !(childAvaFile == null || childAvaFile.Id == 0))
                        {
                            this.PhotoUrl = Path.Combine(Path.Combine(photoUrl, "Thumb"), fileInfoArray[0].Name);
                            this.PhotoItem = new PhotoItem(photoUrl, fileInfoArray[0].Name);
                        }

                    }
                    else
                    {
                        this.PhotoUrl = ChildVisual.NoPhotoUrl;
                        this.PhotoItem = new PhotoItem(this.Id);
                    }
                }
            }
        }
    }
}