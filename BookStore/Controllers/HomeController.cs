using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BookStore.Models;
using BookStore.Util;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        // создаем контекст данных
        BookContext db = new BookContext();

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult Index()
        {
            // получаем из бд все объекты Book
            IEnumerable<Book> books = db.Books;
            // передаем все объекты в динамическое свойство Books в ViewBag
            ViewBag.Books = books;
            // возвращаем представление
            return View();
        }

        public ActionResult ViewList()
        {
            IEnumerable<Book> books = db.Books;

            return View(books.OrderBy(o => o.Name));
        }

        [HttpGet]  // [HttpDelete] [HttpPut]
        public ActionResult Buy(int id)
        {
            ViewBag.BookId = id;
            return View();  //  Вызов метода View возвращает объект ViewResult.
        }

        [HttpPost]
        public string Buy(Purchase purchase)
        {
            purchase.Date = DateTime.Now;
            // добавляем информацию о покупке в базу данных
            db.Purchases.Add(purchase);
            // сохраняем в бд все изменения
            db.SaveChanges();
            
            return HttpContext.Server.MachineName + ". Спасибо," + purchase.Person + ", за покупку!";
        }

        public ActionResult GetHtml()
        {
            return new HtmlResult("<h2>Привет мир!</h2>");
        }

        public ActionResult GetImage()
        {
            string path = "../Images/visualstudio.png";
            return new ImageResult(path);
        }

        // доступны следующие объекты контекста: Request, Response, RoutedData, HttpContext и Server.
        // Фрейморк ASP.NET MVC предлагает нам богатую палитру классов результатов действий, которые охватывают большинство, если не все возможные ситуации
        /*
         * ContentResult: пишет указанный контент напрямую в ответ в виде строки, практически как предыдущие примеры
         * EmptyResult: по сути ничего не делает, отправляет пустой ответ
         * FileResult: является базовым классом для всех объектов, пишущих бинарный ответ в выходной поток. Предназначен для отправки файлов.
         * FileContentResult: класс, производный от FileResult, пишет в ответ массив байтов
         * FilePathResult: также производный от FileResult класс, пишет в ответ файл, находящийся по заданному пути
         * FileStreamResult: класс, производный от FileResult, пишет бинарный поток в выходной ответ
         * HttpStatusCodeResult: результат действия, который возвращает клиенту определенный статусный код HTTP
         * HttpUnauthorizedResult: класс, производный от HttpStatusCodeResult.Возвращает клиенту ответ в виде статусного кода HTTP 401, указывая, что пользователь не прошел авторизацию и не имеет прав доступа к запрошенному ресурсу.
         * HttpNotFoundResult: производный от HttpStatusCodeResult.Возвращает клиенту ответ в виде статусного кода HTTP 404, указывая, что запрошенный ресурс не найден
         * JavaScriptResult: возвращает в ответ в качестве содержимого код JavaScript
         * JsonResult: возвращает в качестве ответа объект или набор объектов в формате JSON
         * PartialViewResult: производит рендеринг частичного представления в выходной поток
         * RedirectResult: перенаправляет пользователя по другому адресу URL, возвращая статусный код 302 для временной переадресации или код 301 для постоянной переадресации зависимости от того, установлен ли флаг Permanent.
         * RedirectToRouteResult: класс работает подобно RedirectResult, но перенаправляет пользователя по определенному адресу URL, указанному через параметры маршрута
         * ViewResult: производит рендеринг представления и отправляет результаты рендеринга в виде html-страницы клиенту
        */

    }
}