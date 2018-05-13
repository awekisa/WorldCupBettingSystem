using BettingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BettingSystem.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using(var db = new BettingSystemDbContext())
            {
                var games = db.Games.ToList();

                return View(games);
            }
        }
    }
}