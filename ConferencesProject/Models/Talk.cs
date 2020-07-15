using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferencesProject.Models
{
    public class Talk
    {
        public int Id { get; set; }
        public virtual Conference Conference { get; set; }
        public string Title { get; set; }
        public string Abstract { get; set; }
    }
}
