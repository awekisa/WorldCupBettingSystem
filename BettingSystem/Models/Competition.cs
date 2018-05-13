using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BettingSystem.Models
{
    public class Competition
    {
        public Competition()
        {
            Bets = new List<Bet>();
            Users = new List<User>();
            Games = new List<Game>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Bet> Bets { get; }
        public ICollection<User> Users { get; }
        public ICollection<Game> Games { get; }
    }
}