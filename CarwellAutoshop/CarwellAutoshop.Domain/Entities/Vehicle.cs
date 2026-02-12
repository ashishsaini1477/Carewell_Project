using System.ComponentModel.DataAnnotations;

namespace CarwellAutoshop.Domain.Entities
{
    public class Vehicle
    {
        [Key]
        public int VehicleId { get; set; }

        public int CustomerId { get; set; }

        [Required, MaxLength(20)]
        public string RegistrationNo { get; set; }

        [MaxLength(100)]
        public string Model { get; set; }

        public int FuelTypeId { get; set; }

        public bool IsActive { get; set; } = true;

        public Customer Customer { get; set; }
        public FuelType FuelType { get; set; }

        public ICollection<JobCard> JobCards { get; set; }
    }
}
