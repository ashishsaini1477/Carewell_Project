using System.ComponentModel.DataAnnotations;

namespace CarwellAutoshop.Domain.Entities
{
    public class Garage
    {
        [Key]
        public int GarageId { get; set; }

        [Required, MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Address { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }

        [MaxLength(20)]
        public string GSTIN { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

       // public ICollection<JobCard> JobCards { get; set; }
    }

}
