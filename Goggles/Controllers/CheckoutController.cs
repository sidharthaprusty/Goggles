using Goggles.Models;
using System;
using System.Dynamic;
using System.Linq;
using System.Web.Mvc;

namespace Goggles.Controllers
{

    public class CheckoutController : Controller
    {
        GogglesEntities storeDB = new GogglesEntities();

        public ActionResult AddressAndPayment()
        {
            if (User.Identity.Name != "")
            {
                return View();
            }
            else
            {
                TempData["msg"] = "<div class='alert alert-warning'><strong>Oops!</strong> Please Login/Sign Up to proceed to checkout. </div>";

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult AddressAndPayment(FormCollection values)
        {
            if (User.Identity.Name != "")
            {
                var order = new Order();
                TryUpdateModel(order);
                dynamic mymodel = new ExpandoObject();
                mymodel.Orders = order;
                mymodel.Cart = storeDB.Carts.ToList();
                try
                {
                    //if (string.Equals(values["PromoCode"], PromoCode,
                    //    StringComparison.OrdinalIgnoreCase) == false)
                    //{
                    //    return View(order);
                    //}
                    //else
                    //{
                    //Get the order details of thr user
                    order.Username = User.Identity.Name;
                    order.OrderDate = DateTime.Now;

                    //store the order details in Database
                    storeDB.Orders.Add(order);
                    storeDB.SaveChanges();

                    //process the cart
                    var cart = ShoppingCart.GetCart(this.HttpContext);
                    cart.CreateOrder(order);

                    return RedirectToAction("Complete",
                        new { id = order.OrderId });
                    //}
                }
                catch
                {

                    return View(order);
                }
            }
            else
            {
                TempData["msg"] = "<div class='alert alert-warning' id='success-alert'><strong>Oops!</strong> Please Login/Sign Up to proceed to checkout. </div>";

                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Complete(int id)
        {

            bool isValid = storeDB.Orders.Any(
                o => o.OrderId == id &&
                o.Username == User.Identity.Name);

            if (isValid)
            {
                TempData["removeCart"] = "googles.cart.destroy()";
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }
    }
}