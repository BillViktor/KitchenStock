namespace KitchenStock.Models
{
    public class ShoppingModel
    {
        #nullable enable
        public int Id { get; set; }

        public int? ArticleId { get; set; }
        public ArticleModel? Article { get; set; }
        public int Quantity { get; set; } = 1; //Default quantity is 1

        public DateTime? CreateDate { get; set; } = DateTime.Now;
        public DateTime? UpdateDate { get; set; }
        #nullable disable
    }
}
