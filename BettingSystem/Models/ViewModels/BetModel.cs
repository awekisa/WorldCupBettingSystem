using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BettingSystem.Models.ViewModels
{
    public class BetModel
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int UserId { get; set; }
        public int? CompetitionId { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public int HomeGoals { get; set; }
        public int AwayGoals { get; set; }
        public DateTime Date { get; set; }
        public bool BetMade { get; set; }
        public bool IsOver { get; set; }
    }
}