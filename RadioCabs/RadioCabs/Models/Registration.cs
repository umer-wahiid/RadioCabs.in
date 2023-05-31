using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RadioCabs.Models
{
    public class Registration
    {
        public int RegistrationId { get; set; }

        [StringLength(25, ErrorMessage = "Max 25 Characters Allowed")]
        [Required]
        public string Name { get; set; }

        [Required]
        [StringLength(25)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [MaxLength(20, ErrorMessage = "Max 20 Characters Allowed"),MinLength(8, ErrorMessage = "Min 8 Characters Allowed")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MaxLength(20, ErrorMessage = "Max 20 Characters Allowed"),MinLength(8, ErrorMessage = "Min 8 Characters Allowed")]
        [Display(Name = "Confirm Password")]
        [Compare("Password",ErrorMessage = "Password Not Match")]
        public string ConfirmPassword { get; set; }

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
        public string City { get; set; }
        
        [Required]
        [StringLength(150)]
        public string Profile { get; set; }

        [Required]
        [DefaultValue(1)]
        public int RoleId { get ; set; }
    }
}
