using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        [Display(Name = "customer name")]
        [Column(TypeName = "NVarChar(50)")]
        public string CustomerName { get; set; } = string.Empty;

        [Required]
        public EPay type { get; set; } = EPay.Cash;

        [Required]
        public string Location { get; set; } = string.Empty;

        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Compare("Email", ErrorMessage = "The email confirmation does not match.")]
        [Display(Name = "email confirm")]
        public string EmailConfirmation { get; set; } = string.Empty;

        [Phone]
        public string Phone { get; set; } = string.Empty;

        public IEnumerable<ShoppingBasket>? shoppingBaskets { get; set; }
    }
}

namespace Project
{
    public enum EPay
    {
        Cash,
        Visa,
        CliQ
    }
}
