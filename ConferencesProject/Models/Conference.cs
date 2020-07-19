using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferencesProject.Models
{
    public class Conference
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Location Location { get; set; }
        public DateTime EventDate { get; set; } = new DateTime(2000,01,01);
        public virtual ICollection<Talk> Talks { get; set; }
        public virtual ICollection<UserConf> ParticipatingUsers { get; set; }
    }
}
