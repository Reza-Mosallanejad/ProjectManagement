using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Application.ViewModels
{
    public class ProjectViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [Required]
        [MaxLength(150)]
        public string? Caption { get; set; }
        [Required]
        [MaxLength(150)]
        public string? Employer { get; set; }
        public DateTime FromDate { get; set; } = DateTime.Now;
        public DateTime ToDate { get; set; } = DateTime.Now.AddDays(1);
        public string? Description { get; set; }
    }
}
