using System.ComponentModel.DataAnnotations;

namespace RadioCabs.Models
{
    public enum MembershipType
    {
        Premium,
        Basic,
        Free
    }
    public class CompanyRegistration
    {
        [Key]
        public int CompanyId { get; set; }

        [StringLength(50),MinLength(10)]
        [Required]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [StringLength(13),MinLength(11)]
        [Required]
        [Display(Name = "Contact Person")]
        public string ContactPerson { get; set; }

        [StringLength(20)]
        [Required]
        public string Designation { get; set; }

        [StringLength(150)]
        [Required]
        public string Address { get; set; }

        [StringLength(13), MinLength(11)]
        [Required]
        public string Mobile { get; set; }

        [StringLength(13), MinLength(11)]
        [Required]
        public string Telephone { get; set; }
        public string FaxNumber { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "MemberShip Type")]
        public MembershipType MembershipType { get; set; }

        [Required]
        [Display(Name = "Payment Type")]
        public string PaymentType { get; set; }

        [Required]
        [StringLength(1000)]
        public string LogoImage { get; set; }

        public int UserId { get; set; }
    }
}
