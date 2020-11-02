using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BookStore.Data_Access_Layer
{
    public class Initp : DropCreateDatabaseAlways<DbCtx>
    {
        protected override void Seed(DbCtx ctx)
        {
            ctx.Books.Add(new Book
            {
                Title = "The Atomic Times",
                Author = "Michael Harris",
                Publisher = new Publisher
                {
                    Name = "Publisher 1",
                    ContactInfo = new ContactInfo { PhoneNumber = "123456" }
                },
                Genres = new List<Genre> {
                new Genre {Name = "Genre 1"} },
                DateCreation = DateTime.Now
            });
            ctx.Books.Add(new Book
            {
                Title = "In Defense of Elitism",
                Author = "Joel Stein",
                Publisher = new Publisher { Name = "Publisher 2", ContactInfo = new ContactInfo { PhoneNumber = "345678" } },
                Genres = new List<Genre> {
                new Genre {Name = "Genre 1"},
                new Genre {Name = "Genre 2"}
 },
                DateCreation = DateTime.Now // cum ar fi daca proprietatea aceasta de DateTime nu ar fi map-ata?
            });
            ctx.SaveChanges(); // aceste informatii unde exista de fapt in acest moment?
            base.Seed(ctx);
        }
    }
}