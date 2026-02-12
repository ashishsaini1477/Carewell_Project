using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarwellAutoshop.Domain.Entities
{
    [Table("jobcardstatus")]
    public class JobCardStatus
    {
        [Key]
        [Column("jobcardstatusid")]
        public int JobCardStatusId { get; set; }

        [Required, MaxLength(50)]
        [Column("statusname")]
        public string StatusName { get; set; }

        [Column("displayorder")]
        public int DisplayOrder { get; set; }

        [Column("isfinal")]
        public bool IsFinal { get; set; }

        [Column("isactive")]
        public bool IsActive { get; set; } = true;

        public ICollection<JobCard> JobCards { get; set; }
    }
}