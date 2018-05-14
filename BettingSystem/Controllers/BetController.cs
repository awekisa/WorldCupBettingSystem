using BettingSystem.Models;
using BettingSystem.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BettingSystem.Controllers
{
    public class BetController : Controller
    {
        [Authorize]
        public ActionResult ListBets()
        {
            using (var db = new BettingSystemDbContext())
            {
                var user = db.Users.Single(u => u.Username == User.Identity.Name);
                var bets = db.Bets.ToArray();

                var games = db.Games
                    //.Where(g => !g.IsOver && g.Bets.All(b => b.UserId != user.Id))
                    .Select(g => new BetModel
                    {
                        GameId = g.Id,
                        UserId = user.Id,
                        CompetitionId = g.CompetitionId,
                        HomeTeam = g.HomeTeam.Name,
                        AwayTeam = g.AwayTeam.Name,
                        HomeGoals = g.HomeGoals,
                        AwayGoals = g.AwayGoals,
                        Date = g.Date,
                        BetMade = g.Bets.Any(b => b.GameId == g.Id && b.UserId == user.Id),
                        IsOver = g.Date > DateTime.Now
                    })
                    .ToArray();

                foreach (var game in games)
                {
                    game.HomeGoals = game.BetMade ? bets.Single(b => b.GameId == game.GameId && b.UserId == user.Id).HomeGoals : game.HomeGoals;
                    game.AwayGoals = game.BetMade ? bets.Single(b => b.GameId == game.GameId && b.UserId == user.Id).AwayGoals : game.AwayGoals;
                }

                return View(games);
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddBet(BetModel betModel)
        {
            using (var db = new BettingSystemDbContext())
            {
                var userId = db.Users.Single(u => u.Username == User.Identity.Name).Id;

                Bet bet = new Bet
                {
                    GameId = betModel.GameId,
                    UserId = userId,
                    CompetitionId = betModel.CompetitionId,
                    HomeGoals = betModel.HomeGoals,
                    AwayGoals = betModel.AwayGoals,
                    Date = DateTime.Now
                };

                db.Bets.Add(bet);
                db.SaveChanges();
                return RedirectToAction("UserBets", "Bet");
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult EditBet(BetModel betModel)
        {
            using (var db = new BettingSystemDbContext())
            {
                var userId = db.Users.Single(u => u.Username == User.Identity.Name).Id;

                var dbBet = db.Bets.First(b => b.UserId == userId && b.GameId == betModel.GameId);
                dbBet.GameId = betModel.GameId;
                dbBet.UserId = userId;
                dbBet.CompetitionId = betModel.CompetitionId;
                dbBet.HomeGoals = betModel.HomeGoals;
                dbBet.AwayGoals = betModel.AwayGoals;
                dbBet.Date = DateTime.Now;

                db.SaveChanges();

                return RedirectToAction("UserBets", "Bet");
            }
        }

        [Authorize]
        public ActionResult UserBets()
        {
            using (var db = new BettingSystemDbContext())
            {
                var userId = db.Users.Single(u => u.Username == User.Identity.Name).Id;
                var bets = db.Bets
                    .Where(b => b.UserId == userId)
                    .Select(b => new BetModel
                    {
                        Id = b.Id,
                        GameId = b.GameId,
                        CompetitionId = b.CompetitionId,
                        HomeTeam = b.Game.HomeTeam.Name,
                        AwayTeam = b.Game.AwayTeam.Name,
                        HomeGoals = b.HomeGoals,
                        AwayGoals = b.AwayGoals,
                        GameDate = b.Game.Date,
                        Date = b.Date
                    })
                    .ToArray();

                return View(bets);
            }
        }
    }
}