using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDAS2_University_Project.Models
{
    public class Employer
    {
        public int EmployerId { get; set; }
        public string Speciality { get; set; }
        public string NameEmployee { get; set; }
        public string Phone { get; set; }
        public int OfficeOfficeId { get; set; }
        public int? EmployerEmployerId { get; set; } // Ссылка на самого себя
        public int AddressAddressId { get; set; }
    }
}
