using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConferencesProject.Models;

namespace ConferencesProject
{
    public class ConferenceContext : DbContext
    {
        public ConferenceContext()
            : base("name=ConferencesManagerDb") {}

        public DbSet<Conference> Conferences { get; set; }
        public DbSet<Talk> Talks { get; set; }
        public DbSet<Location> Locations { get; set; }
    }
}
