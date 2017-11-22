using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcMusicStore_Wed_F2017.Models
{
    public class ShoppingCart
    {
        // db connection
        MusicStoreModel db = new MusicStoreModel();

        // unique cart Id
        string ShoppingCartId { get; set; }

        // get current cart contents
        public static ShoppingCart GetCart(HttpContextBase context)
        {
            var cart = new ShoppingCart();
            cart.ShoppingCartId = cart.GetCartId(context);
            return cart;
        }

        public List<Cart> GetCartItems()
        {
            return db.Carts.Where(c => c.CartId == ShoppingCartId).ToList();
        }

        public decimal GetCartTotal()
        {
            decimal? total = (from c in db.Carts
                              where c.CartId == ShoppingCartId
                              select (int?)c.Count * c.Album.Price).Sum();

            return total ?? decimal.Zero;
        }

        // identify current cart if there is one
        public string GetCartId(HttpContextBase context) {

            if (context.Session["CartId"] == null)
            {
                // user is authenticated and identified
                if (!string.IsNullOrEmpty(context.User.Identity.Name))
                {
                    context.Session["CartId"] = context.User.Identity.Name;
                }
                else // user is anonymous, generate unique Id & assign to session
                {
                    Guid tempCartId = Guid.NewGuid();
                    context.Session["CartId"] = tempCartId;
                }
            }

            return context.Session["CartId"].ToString();
        }

        // add to cart
        public void AddToCart(Album album)
        {
            // is item already in cart?
            var cartItem = db.Carts.SingleOrDefault(
                c => c.AlbumId == album.AlbumId
                && c.CartId == ShoppingCartId);

            // album is not already in cart
            if (cartItem == null)
            {
                cartItem = new Cart
                {
                    AlbumId = album.AlbumId,
                    CartId = ShoppingCartId,
                    Count = 1,
                    DateCreated = DateTime.Now
                };

                db.Carts.Add(cartItem);
            }
            else
            {
                cartItem.Count++;
            }

            // commit changes
            db.SaveChanges();
        }
    }
}