using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BookStore.Data_Access_Layer
{
    public class DbCtx : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public DbSet<Publisher> Publisher { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<ContactInfo> ContactInfo { get; set; }
        public DbCtx() : base("BookCS")
        {
            Database.SetInitializer<DbCtx>(new Initp());
        }
    }
}