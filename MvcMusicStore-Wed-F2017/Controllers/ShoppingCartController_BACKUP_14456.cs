using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

// reference the models
using MvcMusicStore_Wed_F2017.Models;

namespace MvcMusicStore_Wed_F2017.Controllers
{
    public class ShoppingCartController : Controller
    {
        // db
        MusicStoreModel db = new MusicStoreModel();

<<<<<<< HEAD
        // free promo code
        const string PromoCode = "FREE";

=======
>>>>>>> b0670701c76886386c7848a527578b0074a56c76
        // GET: ShoppingCart
        public ActionResult Index()
        {
            // get current cart
            var cart = ShoppingCart.GetCart(this.HttpContext);

            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetCartTotal()
            };

            return View(viewModel);
        }

        // GET: AddToCart
        public ActionResult AddToCart(int AlbumId)
        {
            // get current cart (if any) and selected album
            var cart = ShoppingCart.GetCart(this.HttpContext);
            var album = db.Albums.SingleOrDefault(a => a.AlbumId == AlbumId);

            // add selected album to current cart
            cart.AddToCart(album);

            // redirect to /ShoppingCart
            return RedirectToAction("Index");

        }
<<<<<<< HEAD

        // GET: RemoveFromCart/5
        public ActionResult RemoveFromCart(int AlbumId)
        {
            // get current cart 
            var cart = ShoppingCart.GetCart(this.HttpContext);

            // remove 1 from quantity
            int itemCount = cart.RemoveFromCart(AlbumId);

            // reload the whole cart page 
            return RedirectToAction("Index");

            // fetch the updated cart (the whole thing)

            // send back the updated cart as json

        }

        [Authorize]
        // GET: /AddressAndPayment
        public ActionResult AddressAndPayment()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        // POST: /AddressAndPayment
        public ActionResult AddressAndPayment(FormCollection values)
        {
            // instantiate a new order
            var order = new Order();
            TryUpdateModel(order);

            // check PromoCode equals "FREE"
            if (string.Equals(values["PromoCode"], PromoCode, StringComparison.OrdinalIgnoreCase) == false) {
                ViewBag.Message = "Invalid Promo Code - use FREE";
                return View();
            }

            // populate the new order object
            order.Username = User.Identity.Name;
            order.Email = User.Identity.Name;
            order.OrderDate = DateTime.Now;
            var cart = ShoppingCart.GetCart(this.HttpContext);
            order.Total = cart.GetCartTotal();

            // save
            db.Orders.Add(order);
            db.SaveChanges();

            // get new OrderId
            int OrderId = order.OrderId;

            // save Order Details from cart
            var cartItems = cart.GetCartItems();

            foreach (Cart item in cartItems)
            {
                var orderDetail = new OrderDetail();
                orderDetail.OrderId = OrderId;
                orderDetail.AlbumId = item.AlbumId;
                orderDetail.Quantity = item.Count;
                orderDetail.UnitPrice = item.Album.Price;

                // save new order details
                db.OrderDetails.Add(orderDetail);
                db.SaveChanges();

                //db.Carts.Attach(item);
                //db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                //db.SaveChanges();
            }

            // remove cart items
            cart.EmptyCart();

            // remove cart from session
            Session["CartId"] = null;

            // show confirmation page
            return RedirectToAction("OrderSummary", new { Id = order.OrderId });
        }

        [Authorize]
        // GET: /OrderSummary/5
        public ActionResult OrderSummary(int Id)
        {
            // look up new order and pass to the view
            var order = db.Orders.SingleOrDefault(o => o.OrderId == Id);

            return View(order);
        }

=======
>>>>>>> b0670701c76886386c7848a527578b0074a56c76
    }
}