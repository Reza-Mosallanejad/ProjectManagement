using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Domain.Entities
{
    public class Job : BaseEntity
    {
        public int SplitId { get; set; }
        public string? Caption { get; set; }
        public int Duration { get; set; }
        public string? Description { get; set; }


        public virtual Split Split { get; set; }
        public virtual ICollection<JobLog> JobLogs { get; set; }

    }
}
