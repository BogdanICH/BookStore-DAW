using BookStore.Data_Access_Layer;
using BookStore.Models;
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
            List<Book> books = db.Books.Include("Publisher").ToList();
            ViewBag.Books = books;
            return View();
        }

        [HttpPost]
        public ActionResult Create(Book bookRequest)
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
            catch (Exception e)
            {
                return View(bookRequest);
            }
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

    }
}
        // TODO: adaugare action Details(int/string id)
        // TODO: generare View din acest Action (click dreapta Add View)
    
