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
    public class SupplierRepository : ISupplierRepository
    {
        DynamicParameters parameters = new DynamicParameters();
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ConnectionString);

        public int Create(Supplier supplier)
        {
            var SP_Name = "SP_InsertSupplier";
            parameters.Add("@name", supplier.SupplierName);

            var Create = connection.Execute(SP_Name, parameters, commandType: CommandType.StoredProcedure);

            return Create;
        }

        public int Delete(int id)
        {
            var SP_Name = "SP_DeleteSupplier";
            parameters.Add("@id", id);

            var Delete = connection.Execute(SP_Name, parameters, commandType: CommandType.StoredProcedure);

            return Delete;
        }

        public int Update(int id, Supplier supplier)
        {
            var SP_Name = "SP_UpdateSupplier";
            parameters.Add("@id", id);
            parameters.Add("@name", supplier.SupplierName);

            var Update = connection.Execute(SP_Name, parameters, commandType: CommandType.StoredProcedure);

            return Update;
        }

        public IEnumerable<Supplier> GetAll()
        {
            var SP_Name = "SP_RetrieveAllSupplier";

            var GetAll = connection.Query<Supplier>(SP_Name, commandType: CommandType.StoredProcedure);

            return GetAll;
        }

        public async Task<IEnumerable<Supplier>> GetById(int id)
        {
            var SP_Name = "SP_RetrieveSupplierById";
            parameters.Add("@id", id);

            var GetById = await connection.QueryAsync<Supplier>
                (SP_Name, parameters, commandType: CommandType.StoredProcedure);

            return GetById;
        }

    }
}