using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Repositories.Interfaces
{
    interface IItemRepository
    {
        IEnumerable<ItemViewModel> GetAll();
        Task<IEnumerable<ItemViewModel>> GetById(int id);
        int Create(Item item);
        int Update(int id, Item item);
        int Delete(int id);
    }
}
