using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarwellAutoshop.Domain.Entities
{
    public class JobCardStatus
    {
        [Key]
        public int JobCardStatusId { get; set; }

        [Required, MaxLength(50)]
        public string StatusName { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsFinal { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<JobCard> JobCards { get; set; }
    }

}
