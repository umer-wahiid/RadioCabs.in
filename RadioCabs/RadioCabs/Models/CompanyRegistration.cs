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
        [StringLength(50,ErrorMessage = "Max 50 Characters Allowed"),MinLength(10,ErrorMessage = "Min 10 Characters Allowed")]
        [Required]
        public string CompanyName { get; set; }

        [Key]
        public int CompanyId { get; set; }

        [StringLength(20, ErrorMessage = "Max 20 Characters Allowed"), MinLength(8, ErrorMessage = "Min 8 Characters Allowed")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password")]
        [Required]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(13)]
        public string ContactPerson { get; set; }

        [StringLength(20)]
        [Required]
        public string Designation { get; set; }

        [StringLength(250)]
        [Required]
        public string Address { get; set; }

        [Required]
        [StringLength(13)]
        public string Mobile { get; set; }

        [Required]
        [StringLength(13)]
        public string TelePhone { get; set; }

        public string FaxNumber { get; set; }

        [StringLength(25)]
        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }

        [Required]
        public MembershipType MembershipType { get; set; }

        [StringLength(10)]
        [Required]
        public string PaymentType { get; set; }
    }
}
