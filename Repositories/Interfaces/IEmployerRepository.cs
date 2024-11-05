using BDAS2_University_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDAS2_University_Project.Repositories.Interfaces
{
    public interface IEmployerRepository
    {
        IEnumerable<Employer> GetAll();
        Employer GetById(int id);
        void Add(Employer employer);
        void Update(Employer employer);
        void Delete(int id);
    }
}
