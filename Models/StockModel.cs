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

        public DateTime? CreateDate { get; set; } = DateTime.Now;
        public DateTime? UpdateDate { get; set; }

        
        //Default empty constructor
        public StockModel() { }

        //Copy constructor
        public StockModel(StockModel aOriginalStockModel)
        {
            Id = aOriginalStockModel.Id;
            ArticleId = aOriginalStockModel.ArticleId;
            Article = aOriginalStockModel.Article;
            LocationId = aOriginalStockModel.LocationId;
            Location = aOriginalStockModel.Location;
            BestBeforeDate = aOriginalStockModel.BestBeforeDate;
            PercentageLeft = aOriginalStockModel.PercentageLeft;
            CreateDate = aOriginalStockModel.CreateDate;
            UpdateDate = aOriginalStockModel.UpdateDate;
        }
    }
}
