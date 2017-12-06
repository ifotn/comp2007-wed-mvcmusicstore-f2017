using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcMusicStore_Wed_F2017.Models
{
    public class EFStoreManagerRepository : IStoreManagerRepository
    {
        // db - moved from StoreManagerController
        MusicStoreModel db = new MusicStoreModel();

        public IQueryable<Album> Albums { get { return db.Albums; } }

        public IQueryable<Genre> Genres {  get { return db.Genres; } }

        public IQueryable<Artist> Artists { get { return db.Artists; } }

        public Album Find(int? id)
        {
            return db.Albums.FirstOrDefault(a => a.AlbumId == id);
        }

        public void Delete(Album album)
        {
            db.Albums.Remove(album);
            db.SaveChanges();
        }

        public Album Save(Album album)
        {
            if (album.AlbumId == 0)
            {
                db.Albums.Add(album);
            }
            else
            {
                db.Entry(album).State = System.Data.Entity.EntityState.Modified;
            }

            db.SaveChanges();
            return album;
        }
    }
}