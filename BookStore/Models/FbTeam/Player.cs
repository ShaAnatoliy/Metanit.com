using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Models.FbTeam
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Position { get; set; }

        public int? TeamId { get; set; }
        public Team Team { get; set; }
    }

    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Coach { get; set; }

        public ICollection<Player> Players { get; set; }
        public Team()
        {
            Players = new List<Player>();
        }
    }

    public class SoccerContext : DbContext
    {
        public SoccerContext() : base("FootballConnection") { }

        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
    }

    public class PlayersListViewModel
    {
        public IEnumerable<Player> Players { get; set; }
        public SelectList Teams { get; set; }
        public SelectList Positions { get; set; }
    }

}