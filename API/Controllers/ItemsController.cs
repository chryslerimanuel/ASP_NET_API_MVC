using API.Models;
using API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace API.Controllers
{
    public class ItemsController : ApiController
    {
        ItemRepository itemRepo = new ItemRepository();

        public IHttpActionResult Post(Item item)
        {
            var result = itemRepo.Create(item);

            if (item.ItemName == null || item.Price == 0 || item.Quantity == 0 || item.SupplierId == 0)
            {
                return Content(HttpStatusCode.InternalServerError, "Internal Server Error");
            }

            if (result == 0)
            {
                return Content(HttpStatusCode.BadRequest, "Terjadi Kesalahan");
            }
            return Ok("Berhasil");
        }

        public IHttpActionResult Delete(int id)
        {
            var result = itemRepo.Delete(id);
            if (result == 0)
            {
                return Content(HttpStatusCode.NotFound, "Data Tidak Ditemukan");
            }
            return Ok("Berhasil");
        }

        public IHttpActionResult Put(int id, Item item)
        {
            var result = itemRepo.Update(id, item);
            if (result == 0)
            {
                return Content(HttpStatusCode.NotFound, "Data Tidak Ditemukan");
            }
            return Ok("Berhasil Update");
        }

        public IHttpActionResult Get()
        {
            var result = itemRepo.GetAll();
            if (result == null)
            {
                return Content(HttpStatusCode.BadRequest, "Terjadi Kesalahan");
            }
            return Ok(result);
        }

        public async Task<IHttpActionResult> Get(int id)
        {
            var result = await itemRepo.GetById(id);
            if (result != null)
            {
                if (result.Count() == 0)
                {
                    return Content(HttpStatusCode.NotFound, "Data Tidak Ditemukan");
                }
                return Ok(result);
            }
            return Content(HttpStatusCode.BadRequest, "Terjadi Kesalahan");
        }
    }
}
