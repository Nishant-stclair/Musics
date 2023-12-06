using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcSongs.Models;

    public class MvcSongsContext : DbContext
    {
        public MvcSongsContext (DbContextOptions<MvcSongsContext> options)
            : base(options)
        {
        }

        public DbSet<MvcSongs.Models.Songs> Songs { get; set; }
    }
