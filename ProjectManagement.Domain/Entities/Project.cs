using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Domain.Entities
{
    public class Project : BaseEntity
    {
        public int UserId { get; set; }
        public string? Caption { get; set; }
        public string? Employer { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string? Description { get; set; }


        public virtual User User { get; set; }
        public virtual ICollection<Split> Splits { get; set; }

    }
}
