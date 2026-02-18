using System.ComponentModel.DataAnnotations;

namespace NadinSoftTask.Domain.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public DateTime ProduceDate { get; set; }

        [Required]
        public bool IsAvailable { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string ManufactureEmail { get; set; }

        [Required]
        [Phone]
        [MaxLength(20)]
        public string ManufacturePhone { get; set; }

        [Required]
        public string CreatedByUserId { get; set; } // FK  Users
    }
}
