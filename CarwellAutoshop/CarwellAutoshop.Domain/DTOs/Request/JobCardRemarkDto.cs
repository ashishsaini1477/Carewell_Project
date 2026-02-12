using System.ComponentModel.DataAnnotations;

namespace CarwellAutoshop.Domain.DTOs.Request
{
   public class JobCardRemarkDto
    {
        public int JobCardId { get; set; }

        [Required, MaxLength(1000)]
        public string RemarkText { get; set; }
    }
}
