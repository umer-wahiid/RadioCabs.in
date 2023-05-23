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
    [Keyless]
    public class FeedBack
    {
        [Required]
        [StringLength(25)]
        public string Name { get; set; }

        [Required]
        [StringLength(13)]
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
