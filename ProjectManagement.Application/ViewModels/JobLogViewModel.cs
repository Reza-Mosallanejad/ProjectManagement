using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Application.ViewModels
{
    public class JobLogViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int JobId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? StopDate { get; set; }
        public string? StartDescription { get; set; }
        public string? StopDescription { get; set; }


        [Display(Name = "Duration(min)")]
        public int Duration => StopDate.HasValue ? (int)(StopDate.Value - StartDate).TotalMinutes : 0;
        public string? Description => StartDescription + " " + StopDescription;
    }

    public class JobLogIndexViewModel
    {
        public int JobId { get; set; }
        public string JobCaption { get; set; }
        public bool IsActive { get; set; }

        public List<JobLogViewModel> Details { get; set; }
    }

    public class JobLogActivityViewModel
    {
        public int UserId { get; set; }
        public int JobId { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string? Description { get; set; }
    }
}
