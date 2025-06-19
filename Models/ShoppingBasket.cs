using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class ShoppingBasket
    {
        [Key]
        public int ShoppingBasketId { get; set; }
        [Display(Name = "customer name")]
        public int CustomerId { get; set; }
        [Display(Name = "item name")]
        public int ItemId { get; set; }
        [Required]
        [Range(0,20)]
        public int quantity { get; set; }
        [Display(Name = "arrived time")]
        public DateTime  ArrivedTime { get; set; }=DateTime.Today.AddHours(130);

        //navigation       
        public Customer? customer { get; set; }
        public Item? items { get; set; }
    }
}
