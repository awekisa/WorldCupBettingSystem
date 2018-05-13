using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BettingSystem.Models
{
    public class Bet
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int GameId { get; set; }
        public Game Game { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        public int? CompetitionId { get; set; }
        public Competition Competition { get; set; }

        [Required]
        public int HomeGoals { get; set; }
        [Required]
        public int AwayGoals { get; set; }

        public int Points { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}