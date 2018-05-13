using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BettingSystem.Models.ViewModels
{
    public class GameItem
    {
        public int Id { get; set; }

        public int HomeTeamId { get; set; }
        public string HomeTeam { get; set; }

        public int AwayTeamId { get; set; }
        public string AwayTeam { get; set; }

        public int? CompetitionId { get; set; }
        public Competition Competition { get; set; }

        public int HomeGoals { get; set; }
        public int AwayGoals { get; set; }

        public bool IsOver { get; set; }

        public DateTime Date { get; set; }
    }
}