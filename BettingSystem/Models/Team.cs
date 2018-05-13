using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BettingSystem.Models
{
    public class Team
    {
        public Team()
        {
            this.HomeGames = new HashSet<Game>();
            this.AwayGames = new HashSet<Game>();
        }

        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Game> HomeGames { get; set; }
        public ICollection<Game> AwayGames { get; set; }
    }
}