namespace KitchenStock.Models
{
    public class StockLogModel
    {
        #nullable enable
        public int Id { get; set; }

        //Foreign key
        public int? ArticleId { get; set; }
        public ArticleModel? Article { get; set; }

        //Foreign key
        public int? PreviousLocationId { get; set; }
        public LocationModel? PreviousLocation { get; set; }

        //Foreign key
        public int? NewLocationId { get; set; }
        public LocationModel? NewLocation { get; set; }

        public double? PreviousPercentageLeft { get; set; }
        public double? NewPercentageLeft { get; set; }

        public DateTime? PreviousBestBeforeDate { get; set; }
        public DateTime? NewBestBeforeDate { get; set; }

        public string Description { get; set; } = "";

        public DateTime? CreateDate { get; set; } = DateTime.Now;
        #nullable disable
    }
}
