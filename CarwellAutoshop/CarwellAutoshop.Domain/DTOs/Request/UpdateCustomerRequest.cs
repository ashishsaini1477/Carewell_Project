using System.ComponentModel.DataAnnotations;

namespace CarwellAutoshop.Domain.DTOs.Request
{
    public class UpdateCustomerRequest : CustomerRequest
    {
        [Required]
        public int CustomerId { get; set; }
    }
}
