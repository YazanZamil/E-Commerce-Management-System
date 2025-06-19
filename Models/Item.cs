using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Item
    {
        [Key]
        public int ItemId { get; set; }

        [Required(ErrorMessage = "please enter a valid name")]
        [MinLength(3)]
        [MaxLength(25)]
        [Display(Name = "Name")]
        public string ItamName { get; set; } = string.Empty;
        [DataType(dataType: DataType.Currency)]
        [Required(ErrorMessage = "please enter a valid price ")]
        public double Price { get; set; }
        [Range(0, 5)]
        [Required(ErrorMessage ="please enter a valid value")]
        [Display(Prompt = "value between 1,5",Name ="Item Rate")]
        public double Rate { get; set; }
        [Required]
        public EStatus Status { get; set; } = EStatus.in_stock;        
        //navigation
        public IEnumerable<ShoppingBasket>? shoppingBaskets { get; set; }
    }
    public enum EStatus
    {
        in_stock,
        out_stock
        
    }
}
    
    
