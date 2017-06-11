using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using HtmlPropertyHelperDemo.Models;

namespace HtmlPropertyHelperDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
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

        public ActionResult Books()
        {
            return View(new[] {
                new Book("Title 1","Author 1","2001-01-01"),
                new Book("Title 2","Author 2","2002-02-02"),
                new Book("Title 3","Author 3","2003-03-03")
            });
        }

        [HttpPost]
        public ActionResult Books(IEnumerable<Book> books)
        {
            return View("Books", books);
        }
    }
}
