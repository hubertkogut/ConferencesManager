using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferencesProject.Models
{
    public class ErrorLog
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string ControllerName { get; set; }
        public string TargetedResult { get; set; }
        public string SessionId { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
