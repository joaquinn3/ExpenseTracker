using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTrackerWeb.Models
{
    public class Transaction
    {
        [Key]       
        public int TransactionId { get; set; }

        [ForeignKey("Category")]
        [Required(ErrorMessage = "Category is required.")]
        public int CategoryId { get; set; }
        public Category? category { get; set; }

        [Required]
        [Range(1, float.MaxValue, ErrorMessage = "Wrong amount input")]
        public float Amount { get; set; }

        [MaxLength(50)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        public DateTime Date { get; set; } = DateTime.Today;

        [NotMapped]
        public string CategoryAndIcon
        {
            get { return category == null ? "" : category.NameWithIcon; }
        }

        [NotMapped]
        public string? AmountFormat
        {

            get { return Amount.ToString("C2"); }
        //    get { return category.Type == "Expense" ? "- " + Amount.ToString("C2") : "+ " + Amount.ToString("C2"); }
        }

    }
}
