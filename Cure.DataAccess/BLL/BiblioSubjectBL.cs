using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure.DataAccess.BLL
{
    public partial class DataAccessBL
    {

        public IEnumerable<BiblioSubject> GetBiblioSubjects()
        {
            return dataRepository.GetBiblioSubjects();
        }

        public void InsertBiblioSubject(BiblioSubject rodstvo)
        {
            try
            {
                dataRepository.InsertBiblioSubject(rodstvo);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteBiblioSubject(BiblioSubject rodstvo)
        {
            try
            {
                dataRepository.DeleteBiblioSubject(rodstvo);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateBiblioSubject(BiblioSubject rodstvo)
        {
            try
            {
                dataRepository.UpdateBiblioSubject(rodstvo);
            } catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
