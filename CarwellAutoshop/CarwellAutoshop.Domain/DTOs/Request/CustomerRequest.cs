using System.ComponentModel.DataAnnotations;

namespace CarwellAutoshop.Domain.DTOs.Request
{
    public class CustomerRequest
    {
        [Required, MaxLength(200)]
        public string Name { get; set; }

        public long Mobile { get; set; }

        [MaxLength(500)]
        public string Address { get; set; }

        [MaxLength(200)]
        public string Email { get; set; }
    }
}
