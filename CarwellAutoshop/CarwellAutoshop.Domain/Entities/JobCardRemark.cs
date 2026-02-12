using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarwellAutoshop.Domain.Entities
{
    [Table("jobcardremark")]
    public class JobCardRemark
    {
        [Key]
        [Column("remarkid")]
        public int RemarkId { get; set; }

        [Column("jobcardid")]
        public int JobCardId { get; set; }

        [Required, MaxLength(1000)]
        [Column("remarktext")]
        public string RemarkText { get; set; }

        [Column("createddate")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public JobCard JobCard { get; set; }
    }
}