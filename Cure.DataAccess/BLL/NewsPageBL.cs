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
        public NewsPage GetNewsPage(int id)
        {
            return dataRepository.GetNewsPage(id);
        }

        public IEnumerable<NewsPage> GetAllActive()
        {
            return dataRepository.GetAllActive();
        }

        public IEnumerable<NewsPage> MoreNews(int skipRecords, int takeRecords = 12)
        {
            return dataRepository.MoreNews(skipRecords, takeRecords);
        }

        public IEnumerable<NewsPage> GetNewsPages()
        {
            return dataRepository.GetNewsPages();
        }

        public void InsertNewsPage(NewsPage newsPage)
        {
            try
            {
                dataRepository.InsertNewsPage(newsPage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteNewsPage(NewsPage newsPage)
        {
            try
            {
                dataRepository.DeleteNewsPage(newsPage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateNewsPage(NewsPage newsPage)
        {
            try
            {
                dataRepository.UpdateNewsPage(newsPage);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
