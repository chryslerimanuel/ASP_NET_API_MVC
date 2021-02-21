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
    public class ItemsController : Controller
    {
        readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:44302/API/")
        };

        public ActionResult Index()
        {
            IEnumerable<ItemViewModel> iVM = null;

            var responseTask = client.GetAsync("Items"); // api controller
            responseTask.Wait();
            var result = responseTask.Result;

            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<ItemViewModel>>();
                readTask.Wait();
                iVM = readTask.Result;
            }

            return View(iVM);
        }

       
        public ActionResult Details(int id)
        {
            IEnumerable<ItemViewModel> item = null;

            var resposeTask = client.GetAsync("Items/" + id.ToString());
            resposeTask.Wait();
            var result = resposeTask.Result;

            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<ItemViewModel>>();
                readTask.Wait();
                item = readTask.Result;
                // item isinya IEnumerable
            }
             
            try
            {
                var model = item.FirstOrDefault(s => s.Id == id);

                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("ErrorNotFound", "Suppliers");
            }
        }

      
        public async Task<ActionResult> Create()
        {
            HttpResponseMessage response = await client.GetAsync("Suppliers");
            var data = await response.Content.ReadAsAsync<IList<Supplier>>();

            ViewBag.listSupplier = new SelectList(data, "Id", "SupplierName");

            return View();
        }

        
        [HttpPost]
        public ActionResult Create(Item item)
        {
            HttpResponseMessage response = client.PostAsJsonAsync("Items", item).Result;

            return RedirectToAction("Index");
        }

       
        public async Task<ActionResult> Edit(int id)
        {
            HttpResponseMessage responseItems = await client.GetAsync("Items");
            var listItem = await responseItems.Content.ReadAsAsync<IList<Item>>();

            // -------------- //

            HttpResponseMessage responseSuppliers = await client.GetAsync("Suppliers");
            var listSupplier = await responseSuppliers.Content.ReadAsAsync<IList<Supplier>>();

            ViewBag.listSupplier = new SelectList(listSupplier, "Id", "SupplierName");

            try
            {
                var model = listItem.FirstOrDefault(i => i.Id == id);

                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("ErrorNotFound", "Suppliers");
            }
        }

        [HttpPost]
        public ActionResult Edit(Item item)
        {
            var put = client.PutAsJsonAsync<Item>("Items/" + item.Id, item);
            put.Wait();
            var result = put.Result;

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(int id)
        {
            HttpResponseMessage responseItems = await client.GetAsync("Items");
            var listItem = await responseItems.Content.ReadAsAsync<IList<ItemViewModel>>();

            try
            {
                var model = listItem.FirstOrDefault(i => i.Id == id);

                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("ErrorNotFound", "Suppliers");
            }
        }

        [HttpPost]
        public ActionResult Delete(int id, Item item)
        {
            var deleteTask = client.DeleteAsync("Items/" + id.ToString());
            deleteTask.Wait();
            var result = deleteTask.Result;

            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

    }
}