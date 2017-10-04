using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcMusicStore_Wed_F2017.Models;

namespace MvcMusicStore_Wed_F2017.Controllers
{
   
    public class StoreManagerController : Controller
    {
        private MusicStoreModel db = new MusicStoreModel();

        // GET: StoreManager
        public ActionResult Index()
        {
            var albums = db.Albums.Include(a => a.Artist).Include(a => a.Genre);

            ViewBag.AlbumCount = albums.Count();
            return View(albums.ToList().OrderBy(a => a.Artist.Name).ThenBy(a => a.Title));
        }

        // POST: StoreManager - Search Albums By Title
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(String Title)
        {
            var albums = from a in db.Albums
                         where a.Title.Contains(Title)
                         select a;

            ViewBag.AlbumCount = albums.Count();
            return View(albums);
        }

        // GET: StoreManager/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // GET: StoreManager/Create
        public ActionResult Create()
        {
            // sort by Artist and Genre
            var artists = db.Artists.ToList().OrderBy(a => a.Name);
            var genres = db.Genres.ToList().OrderBy(g => g.Name);

            ViewBag.ArtistId = new SelectList(artists, "ArtistId", "Name");
            ViewBag.GenreId = new SelectList(genres, "GenreId", "Name");
            return View();
        }

        // POST: StoreManager/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AlbumId,GenreId,ArtistId,Title,Price,AlbumArtUrl")] Album album)
        {
            if (ModelState.IsValid)
            {
                // check for a new cover image upload
                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];

                    if (file.FileName != null && file.ContentLength > 0)
                    {
                        string path = Server.MapPath("~/Content/Images/") + file.FileName;
                        file.SaveAs(path);

                        // add path to image name before saving
                        album.AlbumArtUrl = "/Content/Images/" + file.FileName;
                    }
                }

                db.Albums.Add(album);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name", album.ArtistId);
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name", album.GenreId);
            return View(album);
        }

        // GET: StoreManager/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }

            // sort by Artist and Genre
            var artists = db.Artists.ToList().OrderBy(a => a.Name);
            var genres = db.Genres.ToList().OrderBy(g => g.Name);

            ViewBag.ArtistId = new SelectList(artists, "ArtistId", "Name", album.ArtistId);
            ViewBag.GenreId = new SelectList(genres, "GenreId", "Name", album.GenreId);
            return View(album);
        }

        // POST: StoreManager/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AlbumId,GenreId,ArtistId,Title,Price,AlbumArtUrl")] Album album)
        {
            if (ModelState.IsValid)
            {
                // check for a new cover image upload
                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];

                    if (file.FileName != null && file.ContentLength > 0)
                    {
                        string path = Server.MapPath("~/Content/Images/") + file.FileName;
                        file.SaveAs(path);

                        // add path to image name before saving
                        album.AlbumArtUrl = "/Content/Images/" + file.FileName;
                    }
                }

                db.Entry(album).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name", album.ArtistId);
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name", album.GenreId);
            return View(album);
        }

        // GET: StoreManager/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // POST: StoreManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Album album = db.Albums.Find(id);
            db.Albums.Remove(album);
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
