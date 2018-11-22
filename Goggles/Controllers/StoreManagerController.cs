using Goggles.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Goggles.Controllers
{
    //[Authorize(Users = "sidharth")]
    public class StoreManagerController : Controller
    {
        private GogglesEntities db = new GogglesEntities();

        // GET: /StoreManager/
        public ActionResult Index()
        {
            if (User.Identity.Name != "")
            {
                if (User.Identity.Name == "sidharth")
                {
                    var items = db.Items.Include(i => i.Category).Include(i => i.Producer);
                    return View(items.ToList());
                }
                else
                    return View("~/Views/Shared/UnauthorizedAccess.cshtml");
            }
            else
            {
                TempData["msg"] = "<div class='alert alert-warning' id='success-alert'><strong>Oops!</strong> Please Login with an Admin account to access store controls. </div>";

                return RedirectToAction("Index", "Home");
            }
        }

        // GET: /StoreManager/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: /StoreManager/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name");
            ViewBag.ProducerId = new SelectList(db.Producers, "ProducerID", "Name");
            return View();
        }

        // POST: /StoreManager/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ItemId,CategoryId,ProducerId,Title,Price,ItemArtUrl,description")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Items.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", item.CategoryId);
            ViewBag.ProducerId = new SelectList(db.Producers, "ProducerID", "Name", item.ProducerId);
            return View(item);
        }

        // GET: /StoreManager/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", item.CategoryId);
            ViewBag.ProducerId = new SelectList(db.Producers, "ProducerID", "Name", item.ProducerId);
            return View(item);
        }

        // POST: /StoreManager/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemId,CategoryId,ProducerId,Title,Price,ItemArtUrl,description")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", item.CategoryId);
            ViewBag.ProducerId = new SelectList(db.Producers, "ProducerID", "Name", item.ProducerId);
            return View(item);
        }

        // GET: /StoreManager/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: /StoreManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = db.Items.Find(id);
            db.Items.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
