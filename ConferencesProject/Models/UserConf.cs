using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferencesProject.Models
{
    public class UserConf
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string IdInAspNetUsers { get; set; }
        public virtual ICollection<Conference> Conferences { get; set; }

    }
}
