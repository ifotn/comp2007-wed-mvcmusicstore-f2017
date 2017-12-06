using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcMusicStore_Wed_F2017.Models
{
    public interface IStoreManagerRepository
    {
        // Mock interface for unit testing
        IQueryable<Album> Albums { get;  }
        IQueryable<Genre> Genres { get;  }
        IQueryable<Artist> Artists { get; }
        Album Save(Album album);
        void Delete(Album album);

    }
}
