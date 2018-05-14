using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BettingSystem.Models
{
    public class User
    {
        public User()
        {
            this.Bets = new HashSet<Bet>();
            this.Competitions = new HashSet<Competition>();
        }

        [Required]
        public int Id { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [StringLength(200)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public int Points { get; set; }

        public bool IsAdmin { get; set; }

        public ICollection<Bet> Bets { get; set; }
        public ICollection<Competition> Competitions { get; set;}

    }
}