using System.ComponentModel.DataAnnotations;

namespace RadioCabs.Models
{
    public class DriversRegistration
    {
        [Key]
        public int DriverId { get; set; }

        [Required]
        [StringLength(20)]
        public string Experience { get; set; }

        [Required]
        [StringLength(300)]
        public string Description { get; set; }

        public int RegistrationId { get; set; }
        public Registration Registration { get; set; }
    }
}
