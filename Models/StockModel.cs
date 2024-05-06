namespace KitchenStock.Models
{
    public class StockModel
    {
        public int Id { get; set; }

        //Foreign key
        public int ArticleId { get; set; }
        public ArticleModel Article { get; set; }

        //Foreign key
        public int LocationId { get; set; }
        public LocationModel Location { get; set; }

        public DateTime? BestBeforeDate { get; set; }
        public double PercentageLeft { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
