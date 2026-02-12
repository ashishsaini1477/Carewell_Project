using System.ComponentModel.DataAnnotations;

namespace CarwellAutoshop.Domain.Entities
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        public int InvoiceId { get; set; }
        public int PaymentModeId { get; set; }

        public decimal PaidAmount { get; set; }

        [MaxLength(20)]
        public string PaymentStatus { get; set; } = "Success";

        [MaxLength(100)]
        public string ReferenceNo { get; set; }

        public DateTime PaymentDate { get; set; } = DateTime.Now;

        public Invoice Invoice { get; set; }
        public PaymentMode PaymentMode { get; set; }
    }
}
