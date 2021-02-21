using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using API.Context;
using API.Models;
using API.Repositories;

namespace API.Controllers
{
    public class SuppliersController : ApiController
    {
        SupplierRepository supplierRepository = new SupplierRepository();

        public IHttpActionResult Post(Supplier supplier)
        {
            var result = supplierRepository.Create(supplier);
            if (result == 0)
            {
                return Content(HttpStatusCode.InternalServerError, "Terjadi Kesalahan");
            }
            return Ok("Berhasil Create");
        }

        public IHttpActionResult Delete(int id)
        {
            var result = supplierRepository.Delete(id);
            if (result == 0)
            {
                return Content(HttpStatusCode.NotFound, "Data Tidak Ditemukan");
            }
            return Ok("Berhasil Delete");
        }

        // Patch ganti Put
        public IHttpActionResult Put(int id, Supplier supplier)
        {
            var result = supplierRepository.Update(id, supplier);
            if (result == 0)
            {
                return Content(HttpStatusCode.NotFound, "Data Tidak Ditemukan");
            }
            return Ok("Berhasil Update");
        }

        public IHttpActionResult Get()
        {
            var result = supplierRepository.GetAll();
            if (result == null)
            {
                return Content(HttpStatusCode.InternalServerError, "Terjadi Kesalahan");
            }
            return Ok(result);
        }

        public async Task<IHttpActionResult> Get(int id)
        {
            var result = await supplierRepository.GetById(id);
            if (result != null)
            {
                if (result.Count() == 0)
                {
                    return Content(HttpStatusCode.NotFound, "Data Tidak Ditemukan");
                }
                return Ok(result);
            }
            return Content(HttpStatusCode.InternalServerError, "Terjadi Kesalahan");
        }
    }
}