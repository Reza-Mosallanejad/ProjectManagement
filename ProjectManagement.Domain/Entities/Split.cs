using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Domain.Entities
{
    public class Split : BaseEntity
    {
        public int ProjectId { get; set; }
        public string? Caption { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string? Description { get; set; }


        public virtual Project Project { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }

    }
}
