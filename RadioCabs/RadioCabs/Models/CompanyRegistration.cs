using System.ComponentModel.DataAnnotations;

namespace RadioCabs.Models
{
    public enum MembershipType
    {
        Premium,
        Basic,
        Free
    }
    public enum City
    {
        Karachi,
        Lahore,
        Islamabad,
        Quetta,
        Peshawar,
        Multan,
        Gujaranwala,
        Hyderabad,
        Faisalabad,
        Gujarat,
        NawabShah
    }
    public class CompanyRegistration
    {
        [Key]
        public int CompanyId { get; set; }

        [StringLength(50),MinLength(10)]
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

        [StringLength(13), MinLength(11)]
        [Required]
        public string Mobile { get; set; }

        [StringLength(13), MinLength(11)]
        [Required]
        public string Telephone { get; set; }

        [Required]
        [Display(Name = "Fax Number")]
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
        public City City { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        [StringLength(1000)]
        [Display(Name = "Logo Image")]
        public string LogoImage { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}
