namespace KitchenStock.Models
{
    public class ArticleModel
    {
        #nullable enable
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string? Description { get; set; }
        public string? EAN { get; set; }

        public double? WeightInGrams { get; set; }
        public double? VolumeInLiters { get; set; }
        public double? QuantityInPackage { get; set; }

        public int? IngredientId { get; set; }
        public IngredientModel? Ingredient { get; set; }

        public DateTime? CreateDate { get; set; } = DateTime.Now;
        public DateTime? UpdateDate { get; set; }

        public List<StockModel> Stock { get; set; } = null!;
        public List<CategoryModel> Categories { get; set; } = null!;

        #nullable disable

        /// <summary>
        /// Gets the categories in a comma seperated string
        /// </summary>
        /// <returns>The categories, seperated by a comma</returns>
        public string GetCategoryString()
        {
            string sStringOut = "";
            
            if(Categories != null)
            {
                for (int i = 0; i < Categories.Count; i++)
                {
                    sStringOut += Categories[i].Name;

                    if (i < Categories.Count - 1)
                    {
                        sStringOut += ", ";
                    }
                }
            }

            return sStringOut;
        }
    }
}
