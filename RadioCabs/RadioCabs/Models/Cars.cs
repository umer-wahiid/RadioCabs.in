using System.ComponentModel.DataAnnotations;

namespace RadioCabs.Models
{
    public class Cars
    {
        public int CarsId { get; set; }

        [StringLength(50)]
        [Display(Name = "Car Name")]
        [Required]
        public string CarsName { get; set; }

        [StringLength(1000)]
        [Display(Name = "Car Image")]
        [Required]
        public string CarsImage { get; set; }

        [StringLength(100)]
        [Display(Name = "Car Model")]
        [Required]
        public string CarsModel { get; set; }

        [StringLength(25)]
        [Display(Name = "Car Number")]
        [Required]
        public string CarsNumber { get; set; }

        public string CompanyId { get; set; }
    }
}
