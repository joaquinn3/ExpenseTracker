using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTrackerWeb.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage ="Category name is required.")]
        [MaxLength(20)]
        public string? Name { get; set; }

        [Required]
        [MaxLength(20)]
        public string? Type { get; set; }

        [MaxLength(5)]
        public string? Icon { get; set; }

        [NotMapped]
        public string NameWithIcon
        {
            get {return Icon + " " + Name; }
        }
    }
}
