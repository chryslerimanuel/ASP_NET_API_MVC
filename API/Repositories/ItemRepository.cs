using API.Models;
using API.Repositories.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace API.Repositories
{
    public class ItemRepository : IItemRepository
    {
        DynamicParameters parameters = new DynamicParameters();
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ConnectionString);

        public int Create(Item item)
        {
            var SP_Name = "SP_InsertItem";
            parameters.Add("@name", item.ItemName);
            parameters.Add("@quantity", item.Quantity);
            parameters.Add("@price", item.Price);
            parameters.Add("@supplierId", item.SupplierId);

            var Create = connection.Execute(SP_Name, parameters, commandType: CommandType.StoredProcedure);

            return Create;
        }

        public int Delete(int id)
        {
            var SP_Name = "SP_DeleteItem";
            parameters.Add("@id", id);

            var Delete = connection.Execute(SP_Name, parameters, commandType: CommandType.StoredProcedure);

            return Delete;
        }

        public int Update(int id, Item item)
        {
            var SP_Name = "SP_UpdateItem";
            parameters.Add("@id", id);
            parameters.Add("@name", item.ItemName);
            parameters.Add("@quantity", item.Quantity);
            parameters.Add("@price", item.Price);
            parameters.Add("@supplierId", item.SupplierId);

            var Update = connection.Execute(SP_Name, parameters, commandType: CommandType.StoredProcedure);

            return Update;
        }

        public IEnumerable<ItemViewModel> GetAll()
        {
            var SP_Name = "SP_RetrieveAllItem";

            var GetAll = connection.Query<ItemViewModel>(SP_Name, commandType: CommandType.StoredProcedure);

            return GetAll;
        }

        public async Task<IEnumerable<ItemViewModel>> GetById(int id)
        {
            var SP_Name = "SP_RetrieveItemById";
            parameters.Add("@id", id);

            var GetById = await connection.QueryAsync<ItemViewModel>(SP_Name, parameters, commandType: CommandType.StoredProcedure);

            return GetById;
        }
    }
}