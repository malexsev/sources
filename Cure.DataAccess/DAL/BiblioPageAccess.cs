using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure.DataAccess.DAL
{
    internal partial class DataRepository
    {
        public BiblioPage GetBiblioPage(int id)
        {
            return context.BiblioPages.FirstOrDefault(x => x.Id == id);
        }

        public BiblioPage GetBiblioPage(string alias)
        {
            return context.BiblioPages.FirstOrDefault(x => x.Alias == alias);
        }

        public IEnumerable<BiblioPage> GetAllBiblioPageActive()
        {
            return context.BiblioPages.Where(x => x.IsActive).OrderByDescending(o => o.Date).ThenByDescending(x => x.Id).ToList();
        }

        public IEnumerable<BiblioPage> MoreBiblio(int skipRecords, int takeRecords = 4)
        {
            return context.BiblioPages.Where(o => o.IsActive == true)
                .OrderByDescending(o => o.Date).ThenByDescending(x => x.Id)
                .Skip(skipRecords).Take(takeRecords).ToList();
        }

        public IEnumerable<BiblioPage> GetBiblioPages()
        {
            return context.BiblioPages.OrderByDescending(o => o.Date).ThenByDescending(x => x.Id).ToList();
        }

        public void InsertBiblioPage(BiblioPage BiblioPage)
        {
            try
            {
                context.BiblioPages.AddObject(BiblioPage);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteBiblioPage(BiblioPage BiblioPage)
        {
            try
            {
                context.BiblioPages.Attach(BiblioPage);
                context.BiblioPages.DeleteObject(BiblioPage);
                SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateBiblioPage(BiblioPage BiblioPage)
        {
            try
            {
                var origBiblioPage = GetBiblioPage(BiblioPage.Id);
                origBiblioPage.Alias = BiblioPage.Alias;
                origBiblioPage.CreateDate = BiblioPage.CreateDate;
                origBiblioPage.CreatorName = BiblioPage.CreatorName;
                origBiblioPage.Date = BiblioPage.Date;
                origBiblioPage.EditDate = BiblioPage.EditDate;
                origBiblioPage.GuidId = BiblioPage.GuidId;
                origBiblioPage.LastUser = BiblioPage.LastUser;
                origBiblioPage.Name = BiblioPage.Name;
                if (BiblioPage.Settings != null)
                {
                    origBiblioPage.Settings = BiblioPage.Settings;
                }
                origBiblioPage.SubjectID = BiblioPage.SubjectID;
                origBiblioPage.Title = BiblioPage.Title;
                origBiblioPage.Text = BiblioPage.Text;
                origBiblioPage.Subtitle = BiblioPage.Subtitle;
                origBiblioPage.IsActive = BiblioPage.IsActive;
                origBiblioPage.Sort = BiblioPage.Sort;
                SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private BiblioPage GetBiblioPageByName(string name)
        {
            return context.BiblioPages.FirstOrDefault(x => x.Name == name);
        }
    }
}
