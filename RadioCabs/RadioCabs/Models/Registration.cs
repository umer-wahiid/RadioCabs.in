using Newtonsoft.Json;
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

        [StringLength(25)]
        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }
        
        [Required]
        [MaxLength(20, ErrorMessage = "Max 20 Characters Allowed"),MinLength(8, ErrorMessage = "Min 8 Characters Allowed")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage = "Password Not Match")]
        [MaxLength(20, ErrorMessage = "Max 20 Characters Allowed"),MinLength(8, ErrorMessage = "Min 8 Characters Allowed")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [StringLength(250)]
        [Required]
        public string Address { get; set; }

        [Required]
        [StringLength(11), MinLength(11)]
        public string Mobile { get; set; }

        [Required]
        [StringLength(11), MinLength(11)]
        public string TelePhone { get; set; }

        [Required]
        public string City { get; set; }
        
        [Required]
        [StringLength(150)]
        public string Profile { get; set; }

        [Required]
        [DefaultValue("1")]
        public int RoleId { get; set; }
    }
}
