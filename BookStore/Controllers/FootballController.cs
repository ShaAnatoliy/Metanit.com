using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Models.FbTeam;
using System.Data.Entity;

namespace BookStore.Controllers
{
    public class FootballController : Controller
    {
        SoccerContext db = new SoccerContext();

        // GET: Football
        public ActionResult Index()
        {
            var players = db.Players.Include(p => p.Team);

            return View();
        }

    }
}
