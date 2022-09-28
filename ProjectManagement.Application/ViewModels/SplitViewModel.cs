using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Application.ViewModels
{
    public class SplitViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        [Required]
        [MaxLength(150)]
        public string? Caption { get; set; }
        public DateTime FromDate { get; set; } = DateTime.Now;
        public DateTime ToDate { get; set; } = DateTime.Now.AddDays(1);
        public string? Description { get; set; }
    }

    public class SplitGeneralInfoViewModel
    {
        public int ProjectId { get; set; }
        public string ProjectCaption { get; set; }

        public List<SplitViewModel> Details { get; set; }
    }
}
