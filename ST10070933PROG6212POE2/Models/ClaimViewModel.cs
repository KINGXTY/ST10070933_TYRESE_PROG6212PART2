using System.ComponentModel.DataAnnotations;

namespace ST10070933PROG6212POE2.Models
{
    public class ClaimViewModel
    {
        public int LecturerId { get; set; }  

        [Required(ErrorMessage = "Hours worked is required")]
        [Range(0.1, 1000, ErrorMessage = "Hours must be between 0.1 and 1000")]
        [Display(Name = "Hours Worked")]
        public decimal HoursWorked { get; set; }

        [Required(ErrorMessage = "Hourly rate is required")]
        [Range(0.01, 1000, ErrorMessage = "Rate must be between 0.01 and 1000")]
        [Display(Name = "Hourly Rate")]
        public decimal HourlyRate { get; set; }

        [Display(Name = "Additional Notes")]
        public string Notes { get; set; }
    }
}