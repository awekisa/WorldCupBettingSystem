using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BettingSystem.Models.ViewModels
{
    public class CompetitionViewModel
    {
        public string Name { get; set; }
        public ICollection<User> Users { get; set; }
    }
}