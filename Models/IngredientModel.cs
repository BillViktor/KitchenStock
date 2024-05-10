namespace KitchenStock.Models
{
    public class IngredientModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Checkstock { get; set; } = true;

        //Nutritional properties
        public double KcalPerHundredGrams { get; set; }
        public double ProteinPerHundredGrams { get; set; }
        public double FatsPerHundredGrams { get; set; }
        public double CarbsPerHundredGrams { get; set; }

        //Conversion properties
        public double? WeightOfOneQuantity { get; set; } //For example eggs
        public double? WeightOfOneHundredMilliliters { get; set; } //For example oats, flour (stuff often measured by volume)

        public DateTime? CreateDate { get; set; } = DateTime.Now;
        public DateTime? UpdateDate { get; set; }

        #region Constructors
        //Default empty constructor
        public IngredientModel() { }

        //Copy constructor
        public IngredientModel(IngredientModel aIngredientModelOriginal)
        {
            Id = aIngredientModelOriginal.Id;
            Name = aIngredientModelOriginal.Name;
            Description = aIngredientModelOriginal.Description;
            CreateDate = aIngredientModelOriginal.CreateDate;
            UpdateDate = aIngredientModelOriginal.UpdateDate;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Converts the given volume in milliliters to a weight in grams (if conversion is set)
        /// </summary>
        /// <param name="aVolumeInMilliters">The volume to convert to grams</param>
        /// <returns>The weight in grams, null if conversion not set</returns>
        public double? ConvertVolumeInMillilitersToGrams(int aVolumeInMilliters)
        {
            if (WeightOfOneHundredMilliliters == null) return null;

            double sWeightOfOneMilliliter = (double)WeightOfOneHundredMilliliters / 100;

            return sWeightOfOneMilliliter * aVolumeInMilliters;
        }

        /// <summary>
        /// Converts the given qty to a weight in grams (if conversion is set)
        /// </summary>
        /// <param name="aQuantity">The qty to convert to grams</param>
        /// <returns>The weight in grams, null if conversion not set</returns>
        public double? ConvertQuantityInPiecesToGrams(double aQuantity)
        {
            if (WeightOfOneQuantity == null) return null;

            return WeightOfOneQuantity * aQuantity;
        }
        #endregion
    }
}
