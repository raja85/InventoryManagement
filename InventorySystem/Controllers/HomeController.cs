using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace InventorySystem.Controllers
{
    public class HomeController : Controller
    {
        InventoryDb context = new InventoryDb();

        // GET: Home
        public ActionResult Index()
        {
            try
            {
                context.Database.CreateIfNotExists();
                context.SaveChanges();

                List<Models.Hammer> hammers = context.Hammers.ToList();
                return View(hammers);
            }
            catch
            {
                return View(new List<Models.Hammer>());
            }       
        }

       // GET: Home/Add
        public ActionResult Add()
        { 
            return View();
        }

        // POST: Home/Add
        [HttpPost]
        public ActionResult Add(Models.Hammer hammer)
        {
            if (!ModelState.IsValid) // It's valid even when user = null
            {
                return View();
            }

            try
            {
                hammer.CreatedOn = System.DateTime.Now;
                hammer.UpdatedOn = System.DateTime.Now;

                context.Hammers.Add(hammer);
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int id)
        {
           Models.Hammer hammer = context.Hammers.FirstOrDefault(x => x.Id == id);
           return View(hammer);
        }

        // POST: Home/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Models.Hammer hammer)
        {
            if (!ModelState.IsValid) // It's valid even when user = null
            {
                return View();
            }

            try
            {
                Models.Hammer oldHammer = context.Hammers.FirstOrDefault(x => x.Id == id);
                oldHammer.Name = hammer.Name;
                oldHammer.Description = hammer.Description;
                oldHammer.Stock = hammer.Stock;
                oldHammer.Cost = hammer.Cost;
                oldHammer.UpdatedOn = System.DateTime.Now;

                context.Hammers.Attach(oldHammer);
                context.Entry(oldHammer).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                context.Hammers.Remove(context.Hammers.FirstOrDefault(x => x.Id == id));
                context.SaveChanges();
            }
            catch { }

            return RedirectToAction("Index");
        }        
    }
}
