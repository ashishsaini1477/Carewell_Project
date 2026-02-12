using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarwellAutoshop.Domain.Entities
{
    [Table("customer")]
    public class Customer
    {
        [Key]
        [Column("customerid")]
        public int CustomerId { get; set; }

        [Required, MaxLength(200)]
        [Column("Name")]
        public string Name { get; set; }

        [Column("mobile")]
        public long Mobile { get; set; }

        [MaxLength(500)]
        [Column("address")]
        public string Address { get; set; }

        [MaxLength(200)]
        [Column("email")]
        public string Email { get; set; }

        [Column("isactive")]
        public bool IsActive { get; set; } = true;

        [Column("createddate")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        // Navigation collections can remain commented or be enabled when needed
        // public ICollection<Vehicle> Vehicles { get; set; } = new HashSet<Vehicle>();
        // public ICollection<Invoice> Invoices { get; set; } = new HashSet<Invoice>();
    }
}