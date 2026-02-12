using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarwellAutoshop.Domain.Entities
{
    [Table("vehicle")]
    public class Vehicle
    {
        [Key]
        [Column("vehicleid")]
        public int VehicleId { get; set; }

        [Column("customerid")]
        public int CustomerId { get; set; }

        [Required, MaxLength(20)]
        [Column("registrationno")]
        public string RegistrationNo { get; set; }

        [MaxLength(100)]
        [Column("model")]
        public string Model { get; set; }

        [Column("fueltypeid")]
        public int FuelTypeId { get; set; }

        [Column("isactive")]
        public bool IsActive { get; set; } = true;

        public Customer Customer { get; set; }
        public FuelType FuelType { get; set; }

        public ICollection<JobCard> JobCards { get; set; }
    }
}