using BettingSystem.Models;
using BettingSystem.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BettingSystem.Controllers
{
    public class GameController : Controller
    {
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult AddGame()
        {
            using (var db = new BettingSystemDbContext())
            {
                var teams = db.Teams.ToArray();
                var competitions = db.Competitions.ToArray();
                SelectList list = new SelectList(teams, "Id", "Name");
                SelectList competitionSelectList = new SelectList(competitions, "Id", "Name");
                ViewBag.Teams = list;
                ViewBag.Competitions = competitionSelectList;
                return View();
            }
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult AddGame(Game game)
        {
            if (ModelState.IsValid)
            {
                using (var db = new BettingSystemDbContext())
                {
                    game.HomeGoals = 0;
                    game.AwayGoals = 0;
                    db.Games.Add(game);
                    db.SaveChanges();

                    return RedirectToAction("ListGames", "Game");
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
        public ActionResult EditGame(int gameId)
        {
            using (var db = new BettingSystemDbContext())
            {
                var game = db.Games
                    .Select(g => new GameItem
                    {
                        Id = g.Id,
                        HomeTeamId = g.HomeTeamId,
                        HomeTeam = g.HomeTeam.Name,
                        AwayTeamId = g.AwayTeamId,
                        CompetitionId = g.CompetitionId,
                        Competition = g.Competition,
                        AwayTeam = g.AwayTeam.Name,
                        HomeGoals = g.HomeGoals,
                        AwayGoals = g.AwayGoals,
                        IsOver = g.IsOver,
                        Date = g.Date

                    }).
                    FirstOrDefault(t => t.Id == gameId);
                var teams = db.Teams.ToList();
                SelectList list = new SelectList(teams, "Id", "Name");
                SelectList competitionSelectList = new SelectList(teams, "Id", "Name");
                ViewBag.Teams = list;
                ViewBag.Competitions = competitionSelectList;
                return View(game);
            }
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult EditGame(GameItem game)
        {
            using (var db = new BettingSystemDbContext())
            {
                Game dbGame = db.Games.SingleOrDefault(t => t.Id == game.Id);
                if (dbGame != null)
                {
                    dbGame.HomeTeamId = game.HomeTeamId;
                    dbGame.AwayTeamId = game.AwayTeamId;
                    dbGame.CompetitionId = game.CompetitionId;
                    dbGame.Competition = game.Competition;
                    dbGame.HomeGoals = game.HomeGoals;
                    dbGame.AwayGoals = game.AwayGoals;
                    dbGame.Date = game.Date;
                }

                db.SaveChanges();

                return RedirectToAction("ListGames", "Game");
            }
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteGame(int gameId, bool delete = false)
        {
            using (var db = new BettingSystemDbContext())
            {
                Game game = db.Games.FirstOrDefault(g => g.Id == gameId);
                if (game != null)
                {
                    db.Games.Remove(game);
                    db.SaveChanges();
                }

                return RedirectToAction("ListGames", "Game");
            }
        }

        [HttpGet]
        public ActionResult ListGames()
        {
            using (var db = new BettingSystemDbContext())
            {
                var games = new GameListViewModel
                {
                    Games = db.Games.Select(g => new GameItem
                    {
                        Id = g.Id,
                        HomeTeamId = g.HomeTeamId,
                        HomeTeam = g.HomeTeam.Name,
                        AwayTeamId = g.AwayTeamId,
                        AwayTeam = g.AwayTeam.Name,
                        HomeGoals = g.HomeGoals,
                        AwayGoals = g.AwayGoals,
                        Date = g.Date
                    }).ToList()
                };

                return View(games);
            }
        }
    }
}