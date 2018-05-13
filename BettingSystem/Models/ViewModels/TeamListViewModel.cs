using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BettingSystem.Models.ViewModels
{
    public class TeamListViewModel
    {
        public IEnumerable<Team> Teams { get; set; }
    }
}