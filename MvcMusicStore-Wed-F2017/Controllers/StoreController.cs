﻿using MvcMusicStore_Wed_F2017.Models;
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
            // mock up some up genre data and pass that to the view
            var genres = new List<Genre>();
            for (int i = 0; i <= 10; i++)
            {
                genres.Add(new Genre { Name = "Genre " + i });
            }

            // ViewBag.genres = genres;
            ViewBag.Message = "Please select a Genre";
            return View(genres);
        }

        // GET: Store/Browse
        public ActionResult Browse(string genre) {

            // Send genre back to the View
            ViewBag.Genre = genre;
            return View();
        }
    }
}