using System.ComponentModel.DataAnnotations;

namespace RadioCabs.Models
{
    public class DriversRegistration
    {
        [Key]
        public int DriverId { get; set; }

        [StringLength(50)]
        [Required]
        [Display(Name = "Driver Name")]
        public string DriverName { get; set; }

        [StringLength(25)]
        [Required]
        [Display(Name = "Contact Person")]
        public string ContactPerson { get; set; }

        [StringLength(150)]
        [Required]
        public string Address { get; set; }

        [Required]
        [StringLength(50)]
        public string City { get; set; }

        [MaxLength(11, ErrorMessage = "Only 11 Numbers Are Allowed"), MinLength(11, ErrorMessage = "Only 11 Numbers Are Allowed")]
        [Required]
        public string Mobile { get; set; }

        [MaxLength(11, ErrorMessage = "Only 11 Numbers Are Allowed"), MinLength(11, ErrorMessage = "Only 11 Numbers Are Allowed")]
        [Required]
        public string Telephone { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(200)]
        public string Experience { get; set; }

        [Required]
        [StringLength(300)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Payment Type")]
        [StringLength(25)]
        public string PaymentType { get; set; }

        [StringLength(1000)]
        public string DriverImg { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}
