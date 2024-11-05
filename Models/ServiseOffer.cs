using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDAS2_University_Project.Models
{
    public class ServiseOffer
    {
        public int OfferId { get; set; }
        public decimal PricePerHour { get; set; }
        public DateTime DateOffer { get; set; }
        public int EmployerEmployerId { get; set; }
        public int CarCarId { get; set; }
        public string Typ { get; set; }
        public int WorkingHours { get; set; }
    }
}
