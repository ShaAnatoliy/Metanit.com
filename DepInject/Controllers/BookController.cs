using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DepInject.Controllers
{
    using Models;
    using System.Web.Mvc;

    public class BookController : Controller
    {
        BookRepository repo;
        public BookController()
        {
            repo = new BookRepository();
        }
        public ActionResult Index()
        {
            return View(repo.List());
        }
    }
}
