using System.ComponentModel.DataAnnotations;

namespace RadioCabs.Models
{
    public class CompanyRegistration
    {
        [Key]
        public int CompanyId { get; set; }

        [StringLength(50)]
        [Required]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [StringLength(25)]
        [Required]
        [Display(Name = "Contact Person")]
        public string ContactPerson { get; set; }

        [StringLength(20)]
        [Required]
        public string Designation { get; set; }

        [StringLength(150)]
        [Required]
        public string Address { get; set; }

        [MaxLength(11, ErrorMessage = "Only 11 Numbers Are Allowed"), MinLength(11, ErrorMessage = "Only 11 Numbers Are Allowed")]
        [Required]
        public string Mobile { get; set; }

        [MaxLength(11, ErrorMessage = "Only 11 Numbers Are Allowed"), MinLength(11, ErrorMessage = "Only 11 Numbers Are Allowed")]
        [Required]
        public string Telephone { get; set; }

        [Required]
        [Display(Name = "Fax Number")]
        [MaxLength(10, ErrorMessage = "Only 10 Numbers Are Allowed"), MinLength(10, ErrorMessage = "Only 10 Numbers Are Allowed")]
        public string FaxNumber { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "MemberShip Type")]
        [StringLength(25)]
        public string MembershipType { get; set; }

        [Required]
        [Display(Name = "Payment Type")]
        [StringLength(25)]
        public string PaymentType { get; set; }

        [Required]
        [StringLength(25)]
        public string City { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; }

        [Display(Name = "Logo Image")]
        public string LogoImage { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}
