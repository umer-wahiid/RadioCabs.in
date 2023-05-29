using System.ComponentModel.DataAnnotations;

namespace RadioCabs.Models
{
    public class Services
    {
        public int ServicesId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Heading Service 1")]
        public string HService1 { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "D Service 1")]
        public string DService1 { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Heading Service 2")]
        public string HService2 { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "D Service 2")]
        public string DService2 { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Heading Service 3")]
        public string HService3 { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "D Service 3")]
        public string DService3 { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}
