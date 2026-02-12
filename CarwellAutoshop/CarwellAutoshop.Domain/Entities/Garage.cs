using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarwellAutoshop.Domain.Entities
{
    [Table("garage")]
    public class Garage
    {
        [Key]
        [Column("garageid")]
        public int GarageId { get; set; }

        [Required, MaxLength(200)]
        [Column("Name")]
        public string Name { get; set; }

        [MaxLength(500)]
        [Column("address")]
        public string Address { get; set; }

        [MaxLength(20)]
        [Column("phone")]
        public string Phone { get; set; }

        [MaxLength(20)]
        [Column("gstin")]
        public string GSTIN { get; set; }

        [Column("createddate")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        // public ICollection<JobCard> JobCards { get; set; }
    }
}