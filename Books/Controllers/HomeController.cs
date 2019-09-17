﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Books.Models;
using System.Data.SQLite;

namespace Books.Controllers
{
    public class HomeController : Controller
    {
        BookContext db = null;
        string _ConnectFileName;

        public HomeController()
        {
            db = new BookContext();
            SQLiteConnection Connect = db.Database.Connection as SQLiteConnection;
            Connect.Open();
            _ConnectFileName = Connect.FileName;
            Connect.Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Index()
        {

            ViewBag.DB3Name = _ConnectFileName; // 

            return View(db.Books.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ChooseByAuthor()
        {
            return View();
        }

        [HttpPost]
        public ActionResult BookSearch(string name)
        {
            var allbooks = db.Books.Where(a => a.Author.Contains(name)).ToList();
            if (allbooks.Count <= 0)
            {
                return HttpNotFound();
            }
            return PartialView(allbooks);
        }

    }
}