using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarwellAutoshop.Domain.Entities
{
    [Table("payment")]
    public class Payment
    {
        [Key]
        [Column("paymentid")]
        public int PaymentId { get; set; }

        [Column("invoiceid")]
        public int InvoiceId { get; set; }

        [Column("paymentmodeid")]
        public int PaymentModeId { get; set; }

        [Column("paidamount")]
        public decimal PaidAmount { get; set; }

        [MaxLength(20)]
        [Column("paymentstatus")]
        public string PaymentStatus { get; set; } = "Success";

        [MaxLength(100)]
        [Column("referenceno")]
        public string ReferenceNo { get; set; }

        [Column("paymentdate")]
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

        public Invoice Invoice { get; set; }
        public PaymentMode PaymentMode { get; set; }
    }
}