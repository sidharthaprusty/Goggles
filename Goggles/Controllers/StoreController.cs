using Goggles.Models;
using System.Linq;
using System.Web.Mvc;

namespace Goggles.Controllers
{
    public class StoreController : Controller
    {
        GogglesEntities storeDB = new GogglesEntities();
        //
        // GET: /Store/
        public ActionResult Index()
        {
            var items = storeDB.Items.ToList();
            return View(items);
        }
        
        public ActionResult Browse(string category)
        {
            var categoryModel = storeDB.Categories.Include("Items").Single(c => c.Name == category);
            return View(categoryModel);
        }

        public ActionResult Details(int id)
        {
            var Item = storeDB.Items.Find(id);
            return View(Item);
        }
    }
}