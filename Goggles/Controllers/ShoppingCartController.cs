﻿using Goggles.Models;
using Goggles.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace Goggles.Controllers
{
    public class ShoppingCartController : Controller
    {
        GogglesEntities storeDB = new GogglesEntities();

        public ActionResult Index()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);


            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };
            if (viewModel.CartTotal >= 1)
                return View(viewModel);
            else
                return View("EmptyCart");
        }

        public ActionResult AddToCart(int id)
        {

            var addedItem = storeDB.Items
                .Single(item => item.ItemId == id);


            var cart = ShoppingCart.GetCart(this.HttpContext);

            cart.AddToCart(addedItem);


            return RedirectToAction("Index");
        }

        //[HttpPost]
        public ActionResult RemoveFromCart(int id)
        {

            var cart = ShoppingCart.GetCart(this.HttpContext);


            string itemName = storeDB.Carts
                .Single(item => item.RecordId == id).Item.Title;


            int itemCount = cart.RemoveFromCart(id);


            var results = new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(itemName) +
                    " has been removed from your shopping cart.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };
            //if (results.ItemCount == 0)
            //{
            //    return Redirect("EmptyCart");
            //}
            //else
            return Json(results, JsonRequestBehavior.AllowGet);
        }

        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            ViewData["CartCount"] = cart.GetCount();
            return PartialView("CartSummary");
        }
    }
}