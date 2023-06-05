using System.ComponentModel.DataAnnotations;

namespace RadioCabs.Models
{
    public class Services
    {
        public int ServicesId { get; set; }

        [StringLength(50)]
        [Display(Name = "Service Heading")]
        public string HService1 { get; set; }

        [StringLength(500)]
        [Display(Name = "Service Description")]
        public string DService1 { get; set; }

        [StringLength(50)]
        [Display(Name = "Service 2 Heading")]
        public string HService2 { get; set; }

        [StringLength(500)]
        [Display(Name = "Service 2 Description")]
        public string DService2 { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Service 3 Heading")]
        public string HService3 { get; set; }

        [StringLength(500)]
        [Display(Name = "Service 3 Description")]
        public string DService3 { get; set; }

        [Required]
        public int CompanyId { get; set; }
    }
}
