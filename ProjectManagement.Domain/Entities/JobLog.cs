using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Domain.Entities
{
    public class JobLog : BaseEntity
    {
        public int JobId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? StopDate { get; set; }
        public int Duration { get; set; }
        public string? StartDescription { get; set; }
        public string? StopDescription { get; set; }


        public virtual Job Job { get; set; }
    }
}
