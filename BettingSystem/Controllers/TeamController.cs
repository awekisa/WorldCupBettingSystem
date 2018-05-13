using BettingSystem.Models;
using BettingSystem.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BettingSystem.Controllers
{
    public class TeamController : Controller
    {
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult AddTeam()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult AddTeam(Team team)
        {
            if (ModelState.IsValid)
            {
                using (var db = new BettingSystemDbContext())
                {
                    db.Teams.Add(team);
                    db.SaveChanges();

                    return RedirectToAction("ListTeams", "Team");
                }
            }
            else
            {
                ModelState.AddModelError("", "One or more fields have been missing!");
            }

            return View();
        }

        [HttpGet]
        public ActionResult ListTeams()
        {
            using (var db = new BettingSystemDbContext())
            {
                TeamListViewModel teams = new TeamListViewModel
                {
                    Teams = db.Teams.ToList()
                };

                return View(teams);
            }
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteTeam(int teamId)
        {
            using (var db = new BettingSystemDbContext())
            {
                Team team = db.Teams.FirstOrDefault(t => t.Id == teamId);
                if (team != null)
                {
                    db.Teams.Remove(team);
                    db.SaveChanges();
                }

                return RedirectToAction("ListTeams", "Team");
            }
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult EditTeam(int teamId)
        {
            using (var db = new BettingSystemDbContext())
            {
                var team = db.Teams.FirstOrDefault(t => t.Id == teamId);

                return View(team);
            }
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult EditTeam(Team team)
        {
            using (var db = new BettingSystemDbContext())
            {
                Team dbTeam = db.Teams.SingleOrDefault(t => t.Id == team.Id);
                if (dbTeam != null)
                {
                    dbTeam.Name = team.Name;
                }

                db.SaveChanges();

                return RedirectToAction("ListTeams", "Team");
            }
        }
    }
}