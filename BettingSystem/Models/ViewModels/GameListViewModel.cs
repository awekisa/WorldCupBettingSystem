using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BettingSystem.Models.ViewModels
{
    public class GameListViewModel
    {
        public IEnumerable<GameItem> Games { get; set; }
    }
}