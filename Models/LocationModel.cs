namespace KitchenStock.Models
{
    public class LocationModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int? SortOrder { get; set; }

        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public List<StockModel> Stock { get; set; }

        public string GetLocationUrl()
        {
            return $"Location/{Id}";
        }
    }
}
