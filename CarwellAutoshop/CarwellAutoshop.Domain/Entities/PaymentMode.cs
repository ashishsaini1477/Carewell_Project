using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarwellAutoshop.Domain.Entities
{
    [Table("paymentmode")]
    public class PaymentMode
    {
        [Key]
        [Column("paymentmodeid")]
        public int PaymentModeId { get; set; }

        [Required, MaxLength(50)]
        [Column("modename")]
        public string ModeName { get; set; }

        [Column("isonline")]
        public bool IsOnline { get; set; }

        [Column("isactive")]
        public bool IsActive { get; set; } = true;

        public ICollection<Payment> Payments { get; set; }
    }
}