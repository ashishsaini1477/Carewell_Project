using System.ComponentModel.DataAnnotations;

namespace CarwellAutoshop.Domain.Entities
{
    public class FuelType
    {
        [Key]
        public int FuelTypeId { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<Vehicle> Vehicles { get; set; }
    }

}
