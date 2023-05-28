using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RadioCabs.Models
{
    public class Registration
    {
        public int RegistrationId { get; set; }

        [StringLength(50, ErrorMessage = "Max 50 Characters Allowed"), MinLength(10, ErrorMessage = "Min 10 Characters Allowed")]
        [Required]
        public string Name { get; set; }

        [StringLength(25)]
        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }

        [StringLength(20, ErrorMessage = "Max 20 Characters Allowed"), MinLength(8, ErrorMessage = "Min 8 Characters Allowed")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password")]
        [Required]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [StringLength(250)]
        [Required]
        public string Address { get; set; }

        [Required]
        [StringLength(13)]
        public string Mobile { get; set; }

        [Required]
        [StringLength(13)]
        public string TelePhone { get; set; }

        [Required]
        [StringLength(20)]
        public string City { get; set; }
        
        [Required]
        [StringLength(150)]
        public string Profile { get; set; }

        [Required]
        [DefaultValue("1")]
        public int RoleId { get; set; }
    }
}
