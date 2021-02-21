using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LatihanMigration.Controllers
{
    public class SuppliersController : Controller
    {
        readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:44302/API/")
        };

        // GET: Suppliers
        public ActionResult Index()
        {
            // nampung model
            IEnumerable<Supplier> suppliers = null; // Model

            //nyambungin client dengan GetAsync (seperti concat)
            var resposeTask = client.GetAsync("Suppliers"); // <- nama controller di API

            resposeTask.Wait();
            var result = resposeTask.Result;

            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<Supplier>>();
                readTask.Wait();
                suppliers = readTask.Result;
            }

            return View(suppliers);
        }


        // GET: Suppliers/Details/{id}
        public async Task<ActionResult> Details(int id)
        {
            HttpResponseMessage response = await client.GetAsync("Suppliers");
            var data = await response.Content.ReadAsAsync<IList<Supplier>>();
            var supplier = data.FirstOrDefault(s => s.Id == id);

            //var json = JsonConvert.SerializeObject(supplierRepository.Get(id));

            if (supplier == null)
            {
                return RedirectToAction("ErrorNotFound");
            }
            else
            {
                return View(supplier);
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Supplier supplier)
        {
            // data yg ditampilkan API berbentuk JSON
            // jadi bacanya juga harus sebagai JSON

            HttpResponseMessage response = client.PostAsJsonAsync("Suppliers", supplier).Result;

            return RedirectToAction("Index");
        }


        // GET: Suppliers/Edit
        public ActionResult Edit(int id)
        {
            IEnumerable<Supplier> supplier = null;

            var resposeTask = client.GetAsync("Suppliers/" + id.ToString());
            resposeTask.Wait();
            var result = resposeTask.Result;

            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<Supplier>>();
                readTask.Wait();
                supplier = readTask.Result;
                // supplier isinya IEnumerable
            }
            try
            {
                var model = supplier.FirstOrDefault(s => s.Id == id);

                if (model != null)
                {
                    return View(model);
                }
            }
            catch (Exception)
            {
                return RedirectToAction("ErrorNotFound");
            }

            return RedirectToAction("ErrorNotFound");
        }

        [HttpPost]
        public ActionResult Edit(Supplier supplier)
        {

            // kirim data sebagai json pakai putAsJsonAsync
            var put = client.PutAsJsonAsync<Supplier>("Suppliers/" + supplier.Id, supplier);
            put.Wait();
            var result = put.Result;

            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
        }


        public ActionResult Delete(int id)
        {
            IEnumerable<Supplier> supplier = null;

            var resposeTask = client.GetAsync("Suppliers/" + id.ToString());
            resposeTask.Wait();
            var result = resposeTask.Result;

            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<Supplier>>();
                readTask.Wait();
                supplier = readTask.Result;
            }

            var model = supplier.FirstOrDefault(s => s.Id == id);

            if (model != null)
            {
                return View(model);
            }

            return RedirectToAction("ErrorNotFound");
        }

        [HttpPost]
        public ActionResult Delete(Supplier supplier, int id)
        {

            var deleteTask = client.DeleteAsync("Suppliers/" + id.ToString());
            deleteTask.Wait();
            var result = deleteTask.Result;

            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult ErrorNotFound()
        {
            return View();
        }
    }
}