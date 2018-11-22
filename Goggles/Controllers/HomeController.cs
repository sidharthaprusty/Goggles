using Goggles.Models;
using Goggles.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Goggles.Controllers
{
    public class HomeController : Controller
    {
        GogglesEntities storeDB = new GogglesEntities();

        private List<Item> GetTopSellingItems(int count)
        {
            return storeDB.Items.OrderByDescending(i => i.OrderDetails.Count())
                .Take(count)
                .ToList();
        }

        public ActionResult Index()
        {
            //dynamic mymodel = new ExpandoObject();
            //mymodel.Items = storeDB.Items.ToList();
            //mymodel.Categories = storeDB.Categories.ToList();
            var items = storeDB.Items.ToList();
            return View(items);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public ActionResult Contact(ContactViewModel model)
        {
            ViewBag.Message = "Your contact page.";
            TempData["msg"] = "<div class='alert alert-warning'><strong>Wow!!!</strong> Thank You for your feedback. We'll look into it as soon as possible. </div>";

            return View();
        }
    }
}