using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure.DataAccess.Enums
{
    public enum OrderStatus
    {
        Черновик = 1,
        Новый = 2,
        НаРссмотрении = 3,
        Отказался = 4,
        Отказано = 5,
        Одобрено = 6,
        КупленыБилеты = 7,
        Выполняется = 8,
        Завершён = 9,
    }
}
