using System.ComponentModel.DataAnnotations;

namespace RadioCabs.Models
{
    public class DriversRegistration
    {
        [Key]
        public int DriverId { get; set; }

        [StringLength(50), MinLength(10)]
        [Required]
        [Display(Name = "Driver Name")]
        public string DriverName { get; set; }

        [StringLength(13), MinLength(11)]
        [Required]
        [Display(Name = "Contact Person")]
        public string ContactPerson { get; set; }

        [StringLength(150)]
        [Required]
        public string Address { get; set; }

        [Required]
        public City City { get; set; }

        [StringLength(13), MinLength(11)]
        [Required]
        public string Mobile { get; set; }

        [StringLength(13), MinLength(11)]
        [Required]
        public string Telephone { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(20)]
        public string Experience { get; set; }

        [Required]
        [StringLength(300)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Payment Type")]
        public string PaymentType { get; set; }

        [Required]
        [StringLength(1000)]
        public string DriverImg { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}
