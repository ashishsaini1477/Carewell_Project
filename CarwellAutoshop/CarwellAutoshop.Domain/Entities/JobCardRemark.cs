using System.ComponentModel.DataAnnotations;

namespace CarwellAutoshop.Domain.Entities
{
    public class JobCardRemark
    {
        [Key]
        public int RemarkId { get; set; }

        public int JobCardId { get; set; }

        [Required, MaxLength(1000)]
        public string RemarkText { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public JobCard JobCard { get; set; }
    }

}
