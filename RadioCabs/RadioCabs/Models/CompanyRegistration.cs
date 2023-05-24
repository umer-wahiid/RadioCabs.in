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

        [StringLength(20)]
        [Required]
        public string Designation { get; set; }

        public string FaxNumber { get; set; }

        [Required]
        public MembershipType MembershipType { get; set; }

        public int RegistrationId { get; set; }
        public Registration Registration { get; set; }
    }
}
