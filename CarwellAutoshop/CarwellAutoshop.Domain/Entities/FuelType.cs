using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarwellAutoshop.Domain.Entities
{
    [Table("fueltype")]
    public class FuelType
    {
        [Key]
        [Column("fueltypeid")]
        public int FuelTypeId { get; set; }

        [Required, MaxLength(50)]
        [Column("Name")]
        public string Name { get; set; }

        [Column("isactive")]
        public bool IsActive { get; set; } = true;

        public ICollection<Vehicle> Vehicles { get; set; }
    }
}