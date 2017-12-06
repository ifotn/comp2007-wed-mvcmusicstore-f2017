using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcMusicStore_Wed_F2017.Models
{
    public interface IArtistRepository
    {
        IQueryable<Artist> artists { get;  }
    }
}
