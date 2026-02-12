using System.ComponentModel.DataAnnotations;

namespace CarwellAutoshop.Domain.Entities
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required, MaxLength(200)]
        public string Name { get; set; }

        public long Mobile { get; set; }

        [MaxLength(500)]
        public string Address { get; set; }

        [MaxLength(200)]
        public string Email { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

       // public ICollection<Vehicle> Vehicles { get; set; }
       // public ICollection<Invoice> Invoices { get; set; }
    }
}
