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
        // TODO: adaugare action Details(int/string id)
        // TODO: generare View din acest Action (click dreapta Add View)
    }
}