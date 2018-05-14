using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BettingSystem.Models
{
    public class Game
    {
        public Game()
        {
            this.Bets = new HashSet<Bet>();
        }

        [Required]
        public int Id { get; set; }

        [Required]
        public int HomeTeamId { get; set; }
        public Team HomeTeam { get; set; }

        [Required]
        public int AwayTeamId { get; set; }
        public Team AwayTeam { get; set; }

        public int? CompetitionId { get; set; }
        public Competition Competition { get; set; }

        public int HomeGoals { get; set; }
        public int AwayGoals { get; set; }

        [NotMapped]
        public bool IsOver => this.Date > DateTime.Now;

        [Required]
        public DateTime Date { get; set; }

        public ICollection<Bet> Bets { get; set; }

    }
}