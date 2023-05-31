using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RadioCabs.Models
{
    public enum Type
    {
        Complaint,
        Suggestion,
        Compliment
    }
    public class FeedBack
    {
        [Key]
        public int FeedId { get; set; }

        [Required]
        [StringLength(25)]
        public string Name { get; set; }

        [Required]
        [MaxLength(11, ErrorMessage = "Only 11 Numbers Are Allowed"), MinLength(11, ErrorMessage = "Only 11 Numbers Are Allowed")]
        public string MobileNo { get; set; }

        [StringLength(25)]
        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }

        [Required]
        [StringLength(20)]
        public string City { get; set; }

        [Required]
        public Type Type { get; set; }
    }
}
