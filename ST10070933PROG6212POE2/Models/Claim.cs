using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ST10070933PROG6212POE2.Models
{
    public class Claim
    {
        public int Id { get; set; }

        [Required]
        public int LecturerId { get; set; }

        [Required]
        [Range(0.1, 1000)]
        public decimal HoursWorked { get; set; }

        [Required]
        [Range(0.01, 1000)]
        public decimal HourlyRate { get; set; }

        public string Notes { get; set; }

        public string Status { get; set; } = "Pending";

        public DateTime SubmissionDate { get; set; } = DateTime.Now;

        public virtual ICollection<SupportingDocument> Documents { get; set; }
    }
}