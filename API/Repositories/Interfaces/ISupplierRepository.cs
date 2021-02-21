using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Repositories.Interfaces
{
    interface ISupplierRepository
    {
        IEnumerable<Supplier> GetAll();
        Task<IEnumerable<Supplier>> GetById(int id);
        int Create(Supplier supplier);
        int Update(int id, Supplier supplier);
        int Delete(int id);
    }
}
