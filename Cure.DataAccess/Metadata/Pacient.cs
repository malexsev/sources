using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure.DataAccess
{
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(PacientAnnotation))]
    public partial class Pacient
    {
        private const string FullNameTemplate = "{0} {1} {2:yyyy-MM-dd}";

        public string FullName
        {
            get
            {
                return String.Format(FullNameTemplate, this.Familiya, this.Name, this.BirthDate);
            }
        }
    }

    public class PacientAnnotation { }
}
