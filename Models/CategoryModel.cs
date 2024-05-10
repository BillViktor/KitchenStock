namespace KitchenStock.Models
{
    public class CategoryModel
    {
        #nullable enable
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string? Description { get; set; }
        public DateTime? CreateDate { get; set; } = DateTime.Now;
        public DateTime? UpdateDate { get; set; }

        public List<ArticleModel> Articles { get; set; } = null!;

        #nullable disable
    }
}
