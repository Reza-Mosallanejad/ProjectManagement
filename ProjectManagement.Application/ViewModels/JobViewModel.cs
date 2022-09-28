using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Application.ViewModels
{
    public class JobViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SplitId { get; set; }
        [Required]
        [MaxLength(150)]
        public string? Caption { get; set; }
        [Display(Name = "Duratin(min)")]
        public int Duration { get; set; }
        public string? Description { get; set; }
    }

    public class JobInfoViewModel
    {
        public int Id { get; set; }
        public string? Caption { get; set; }
        public bool IsActive { get; set; }
        public string ProjectCaption { get; set; }
        public string SplitCaption { get; set; }
        public bool SplitIsActive { get; set; }
        public int Duration { get; set; }
        public int SpentTime { get; set; }
    }
}
