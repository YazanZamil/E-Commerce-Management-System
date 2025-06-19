using Project.Models;

namespace UsingSearch.Models.ViewModels
{
    public class Finditem
    {
        public string ? ItamName  { get; set; }
        public double? Price { get; set; }
        public double? Rate { get; set; }

        public IEnumerable<Item> items { get; set; } = Enumerable.Empty<Item>();
    }
}