namespace KitchenStock.Models
{
    public class LocationModel
    {
        #nullable enable
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string? Description { get; set; }
        public int? SortOrder { get; set; }

        public DateTime? CreateDate { get; set; } = DateTime.Now;
        public DateTime? UpdateDate { get; set; }

        public List<StockModel> Stock { get; set; } = new List<StockModel>();
        #nullable disable

        public string GetLocationUrl()
        {
            return $"Location/{Id}";
        }
    }
}
