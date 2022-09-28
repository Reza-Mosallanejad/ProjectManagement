using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Application.ViewModels
{
    public class ProjectReportViewModel
    {
        public string ProjectCaption { get; set; }
        public List<JobLogViewModel> Details { get; set; }
        public int TotalTime => Details.Sum(d => d.Duration);
    }
}
