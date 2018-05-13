using BettingSystem.Models;
using BettingSystem.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BettingSystem.Controllers
{
    public class CompetitionController : Controller
    {
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult AddCompetition()
        {
            using (var db = new BettingSystemDbContext())
            {
                var competitions = db.Competitions.ToArray();
                return View();
            }
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult AddCompetition(Competition competition)
        {
            if (ModelState.IsValid)
            {
                using (var db = new BettingSystemDbContext())
                {
                    db.Competitions.Add(competition);
                    db.SaveChanges();

                    return RedirectToAction("ListCompetitions", "Admin");
                }
            }
            else
            {
                ModelState.AddModelError("", "One or more fields have been missing!");
            }

            return View();
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult EditCompetition(int competitionId)
        {
            using (var db = new BettingSystemDbContext())
            {
                var competition = db.Competitions
                    .FirstOrDefault(t => t.Id == competitionId);

                return View(competition);
            }
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult EditCompetition(Competition competition)
        {
            using (var db = new BettingSystemDbContext())
            {
                Competition dbCompetition = db.Competitions.SingleOrDefault(t => t.Id == competition.Id);
                if (dbCompetition != null)
                {
                    dbCompetition.Id = competition.Id;
                    dbCompetition.Name = competition.Name;
                }

                db.SaveChanges();

                return RedirectToAction("ListCompetitions", "Competition");
            }
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteCompetition(int competitionId, bool delete = false)
        {
            using (var db = new BettingSystemDbContext())
            {
                Competition competition = db.Competitions.FirstOrDefault(g => g.Id == competitionId);
                if (competition != null)
                {
                    db.Competitions.Remove(competition);
                    db.SaveChanges();
                }

                return RedirectToAction("ListCompetitions", "Competition");
            }
        }

        [HttpGet]
        public ActionResult ListCompetitions()
        {
            using (var db = new BettingSystemDbContext())
            {
                var competitions = db.Competitions.ToArray();

                return View(competitions);
            }
        }

        [HttpGet]
        public ActionResult CompetitionStandings(int competitionId)
        {
            using (var db = new BettingSystemDbContext())
            {
                var competition = db.Competitions.FirstOrDefault(c => c.Id == competitionId);
                var games = db.Games.Where(b => b.CompetitionId == competitionId).ToArray();
                var bets = db.Bets
                    .Where(b => b.CompetitionId == competitionId 
                        && b.Date > games.FirstOrDefault(g => g.Id == b.GameId).Date)
                    .ToArray();

                var users = db.Users
                    .OrderByDescending(u => u.Bets.Sum(b => b.Points))
                    .ToArray();
                var competitionModel = new CompetitionViewModel
                {
                    Name = competition.Name,
                    Users = users
                };

                return View(competitionModel);
            }
        }
    }
}