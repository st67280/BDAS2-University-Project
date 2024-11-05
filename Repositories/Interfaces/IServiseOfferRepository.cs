using BDAS2_University_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDAS2_University_Project.Repositories.Interfaces
{
    public interface IServiseOfferRepository
    {
        IEnumerable<ServiseOffer> GetAll();
        ServiseOffer GetById(int id);
        void Add(ServiseOffer serviseOffer);
        void Update(ServiseOffer serviseOffer);
        void Delete(int id);
    }
}
