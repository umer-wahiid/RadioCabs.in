using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RadioCabs.Models
{
    public class AdvertiseRegistration
    {
        [Key]
        public int AdvId { get; set; }

        [StringLength(50, ErrorMessage = "Max 50 Characters Allowed"), MinLength(10, ErrorMessage = "Min 10 Characters Allowed")]
        [Required]
        public string CompanyName { get; set; }

        [Required]
        [StringLength(300)]
        public string Destination { get; set; }

        [StringLength(20)]
        [Required]
        public string Designation { get; set; }

        [StringLength(250)]
        [Required]
        public string Address { get; set; }

        [Required]
        [MaxLength(11, ErrorMessage = "Only 11 Numbers Are Allowed"), MinLength(11, ErrorMessage = "Only 11 Numbers Are Allowed")]
        public string Mobile { get; set; }

        [Required]
        [MaxLength(11, ErrorMessage = "Only 11 Numbers Are Allowed"), MinLength(11, ErrorMessage = "Only 11 Numbers Are Allowed")]
        public string TelePhone { get; set; }

        [Required]
        [MaxLength(10, ErrorMessage = "Only 10 Numbers Are Allowed"), MinLength(10, ErrorMessage = "Only 10 Numbers Are Allowed")]
        public string FaxNumber { get; set; }

        [StringLength(25)]
        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }

        [StringLength(100)]
        [Required]
        public string Description { get; set; }

        [StringLength(10)]
        [Required]
        public string PaymentType { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}
