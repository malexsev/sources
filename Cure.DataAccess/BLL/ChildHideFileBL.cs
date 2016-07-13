namespace Cure.DataAccess.BLL
{
    using System;
    using System.Collections.Generic;

    public partial class DataAccessBL
    {
        public IEnumerable<ChildHideFile> GetChildHideFiles(int childId)
        {
            return dataRepository.GetChildHideFiles(childId);
        }

        /// <summary>
        /// Возвращает true если в системе присутствует запись о скрытии для данного child id и filename, false - если файл не скрыт (записи нет).
        /// </summary>
        /// <param name="childId"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool CheckChildHideFile(int childId, string fileName)
        {
            return dataRepository.CheckChildHideFile(childId, fileName);
        }

        public void InsertChildHideFile(ChildHideFile childHideFile)
        {
            dataRepository.InsertChildHideFile(childHideFile);
        }

        public void DeleteChildHideFile(ChildHideFile childHideFile)
        {
            dataRepository.DeleteChildHideFile(childHideFile);
        }

        public void DeleteChildHideFile(int childId, string fileName)
        {
            dataRepository.DeleteChildHideFile(childId, fileName);
        }

    }
}
