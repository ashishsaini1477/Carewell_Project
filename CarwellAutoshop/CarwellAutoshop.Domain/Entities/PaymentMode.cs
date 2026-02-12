using System.ComponentModel.DataAnnotations;

namespace CarwellAutoshop.Domain.Entities
{
    public class PaymentMode
    {
        [Key]
        public int PaymentModeId { get; set; }

        [Required, MaxLength(50)]
        public string ModeName { get; set; }

        public bool IsOnline { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<Payment> Payments { get; set; }
    }

}
