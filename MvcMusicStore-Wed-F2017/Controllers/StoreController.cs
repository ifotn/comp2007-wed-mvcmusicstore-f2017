using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcMusicStore_Wed_F2017.Controllers
{
    public class StoreController : Controller
    {
        // GET: Store
        public ActionResult Index()
        {
            ViewBag.Message = "Please select a Genre";
            return View();
        }

        // GET: Store/Browse
        public ActionResult Browse(string genre) {

            // Send genre back to the View
            ViewBag.Genre = genre;
            return View();
        }
    }
}