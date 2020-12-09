using BookStore.Data_Access_Layer;
using BookStore.Models;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    public class BooksController : Controller
    {
        private DbCtx db = new DbCtx();
        // GET: Books

        [HttpGet]
        public ActionResult Index()
        {
            while (true)
            {
                try
                {

                    List<Book> books = db.Books.Include("Publisher").ToList();
                    ViewBag.Books = books;
                    return View();
                }
                catch (System.Data.SqlClient.SqlException e)
                {
                    HandleConnectionIsOpen(e);
                }
            }
        }

        private static void HandleConnectionIsOpen(System.Data.SqlClient.SqlException e)
        {
            var dbName = e.Message.Split('\"')[1];
            Server server = new Server(@"(localdb)\MSSQLLocalDB");
            Database database = new Database(server, dbName);
            database.Refresh();
            server.KillAllProcesses(dbName);
            database.DatabaseOptions.UserAccess = DatabaseUserAccess.Single;
            //database.Alter(TerminationClause.RollbackTransactionsImmediately);
        }

        [HttpPost]
        public ActionResult Create(Book bookRequest)
        {
            while (true)
            {
                try
                {
                    if (ModelState.IsValid) // ModelState - model binding corect si nu sunt incalcate reguli de validare
                    {
                        bookRequest.Publisher = db.Publisher.FirstOrDefault(p => p.PublisherId.Equals(1)); // de ce e nevoie de populare prop Publisher?
                        bookRequest.DateCreation = DateTime.Now;
                        db.Books.Add(bookRequest);
                        db.SaveChanges();
                        return RedirectToAction("Index"); // RedirectToAction - redirect catre actiunea Index din acelasi controller
                    }
                    return View(bookRequest);
                }
                catch (System.Data.SqlClient.SqlException e)
                {
                    HandleConnectionIsOpen(e);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult New()
        {
            Book book = new Book
            {
                Genres = new List<Genre>()
            };
            return View(book);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                Book book = db.Books.Find(id);
                if (book == null)
                {
                    return HttpNotFound("Nu s-a gasit cartea");
                }
                return View(book);
            }
            return HttpNotFound("Nu ai dat id cartii");
        }

        [HttpPut]
        public ActionResult Edit(Book bookRequest)
        {
            while (true)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        Book book = db.Books
                        .Include("Publisher")
                        .SingleOrDefault(b => b.BookId.Equals(bookRequest.BookId));
                        if (TryUpdateModel(book))
                        {
                            book.Title = bookRequest.Title;
                            book.Author = bookRequest.Author;
                            book.Summary = bookRequest.Summary;
                            db.SaveChanges();
                        }
                        return RedirectToAction("Index");
                    }
                    return View(bookRequest);
                }
                catch (System.Data.SqlClient.SqlException e)
                {
                    HandleConnectionIsOpen(e);
                }
            }

        }
        [HttpGet]
        public ActionResult Details()
        {
            Book book = db.Books
            .Include("Publisher")
            .SingleOrDefault(b => b.BookId.Equals(1));
            return View(book);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Book book = db.Books.Find(id);
            if (book != null)
            {
                db.Books.Remove(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return HttpNotFound("Nu s-a putut găsi cartea ");
        }

    }
}
// TODO: adaugare action Details(int/string id)
// TODO: generare View din acest Action (click dreapta Add View)

