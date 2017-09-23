using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure.DataAccess.BLL
{
    using System.Data;

    public partial class DataAccessBL
    {
        public BiblioPage GetBiblioPage(int id)
        {
            return dataRepository.GetBiblioPage(id);
        }

        public BiblioPage GetBiblioPage(string alias)
        {
            return dataRepository.GetBiblioPage(alias);
        }

        public IEnumerable<BiblioPage> GetAllBiblioPageActive()
        {
            return dataRepository.GetAllBiblioPageActive();
        }

        public IEnumerable<BiblioPage> MoreBiblio(int skipRecords, int takeRecords = 12)
        {
            return dataRepository.MoreBiblio(skipRecords, takeRecords);
        }

        public IEnumerable<BiblioPage> GetBiblioPages()
        {
            return dataRepository.GetBiblioPages();
        }

        public void InsertBiblioPage(BiblioPage biblioPage)
        {
            try
            {
                dataRepository.InsertBiblioPage(biblioPage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteBiblioPage(BiblioPage biblioPage)
        {
            try
            {
                dataRepository.DeleteBiblioPage(biblioPage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateBiblioPage(BiblioPage biblioPage)
        {
            try
            {
                dataRepository.UpdateBiblioPage(biblioPage);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
