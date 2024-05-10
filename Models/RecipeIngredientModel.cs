namespace KitchenStock.Models
{
    public class RecipeIngredientModel
    {
        #nullable enable
        //Fields
        private MeasurementTypeEnum mMeasurementType;

        //Properties
        public int Id { get; set; }

        public int IngredientId { get; set; }
        public IngredientModel Ingredient { get; set; }

        //Ingredients can be of four types, weight, volume, qty or other (for example butter would probably be weight, milk volume, eggs qty)
        public int? WeightInGrams { get; set; }
        public int? VolumeInMilliliters { get; set; }
        public double? QuantityInPieces { get; set; }
        public string? MeasurementDescription { get; set; }
        #nullable disable


        public MeasurementTypeEnum MeasurementType
        {
            get
            {
                return mMeasurementType;
            }
            set
            {
                mMeasurementType = value;

                //Set all other values to null
                switch(value)
                {
                    case (MeasurementTypeEnum.Weight):
                        VolumeInMilliliters = null;
                        QuantityInPieces = null;
                        MeasurementDescription = null;
                        break;

                    case (MeasurementTypeEnum.Volume):
                        WeightInGrams = null;
                        QuantityInPieces = null;
                        MeasurementDescription = null;
                        break;

                    case (MeasurementTypeEnum.Quantity):
                        VolumeInMilliliters = null;
                        WeightInGrams = null;
                        MeasurementDescription = null;
                        break;

                    case (MeasurementTypeEnum.Other):
                        VolumeInMilliliters = null;
                        QuantityInPieces = null;
                        WeightInGrams = null;
                        break;

                    default:
                        break;
                }
            }
        }

        #region Constructors
        //Default constructor
        public RecipeIngredientModel() { }

        //Copy constructor
        public RecipeIngredientModel(RecipeIngredientModel aIngredientModelOriginal)
        {
            if(aIngredientModelOriginal.Ingredient != null)
            {
                Ingredient = new IngredientModel(aIngredientModelOriginal.Ingredient);
            }
            WeightInGrams = aIngredientModelOriginal.WeightInGrams;
            VolumeInMilliliters = aIngredientModelOriginal.VolumeInMilliliters;
            QuantityInPieces = aIngredientModelOriginal.QuantityInPieces;
            MeasurementDescription = aIngredientModelOriginal.MeasurementDescription;
            MeasurementType = aIngredientModelOriginal.MeasurementType;
        }
        #endregion


        #region Methods
        /// <summary>
        /// Gets the measurement
        /// </summary>
        /// <returns>A string containing the measurement</returns>
        public string GetMeasurement(double aMultiplier)
        {
            switch(MeasurementType)
            {
                case MeasurementTypeEnum.Weight:
                    return GetWeightString(aMultiplier);

                case MeasurementTypeEnum.Volume:
                    return GetVolumeString(aMultiplier);

                case MeasurementTypeEnum.Quantity:
                    return GetQuantityString(aMultiplier);

                case MeasurementTypeEnum.Other:
                    return GetOtherString();

                default:
                    return "";
            }
        }
        /// <summary>
        /// Gets the weight in a suiting unit
        /// </summary>
        /// <returns>A string with the weight and a unit</returns>
        public string GetOtherString()
        {
            return MeasurementDescription;
        }


        /// <summary>
        /// Gets the weight in a suiting unit
        /// </summary>
        /// <returns>A string with the weight and a unit</returns>
        public string GetWeightString(double aMultiplier)
        {
            if (WeightInGrams == null)
            {
                return "";
            }
            else if(WeightInGrams * aMultiplier >= 1000)
            {
                return $"{(double)WeightInGrams * aMultiplier / 1000} kg";
            }
            else
            {
                return $"{WeightInGrams * aMultiplier} g";
            }
        }

        /// <summary>
        /// Gets the weight in a suiting unit
        /// </summary>
        /// <returns>A string with the weight and a unit</returns>
        public string GetVolumeString(double aMultiplier)
        {
            string sStringOut = "";

            if (VolumeInMilliliters == null)
            {
                sStringOut = "";
            }
            else if (VolumeInMilliliters * aMultiplier >= 1000)
            {
                sStringOut = $"{(double)VolumeInMilliliters * aMultiplier / 1000} l";
            }
            else if (VolumeInMilliliters * aMultiplier < 1000 && VolumeInMilliliters * aMultiplier >= 50)
            {
                sStringOut = $"{(double)VolumeInMilliliters * aMultiplier /100} dl";
            }
            else if (VolumeInMilliliters * aMultiplier >= 15 && VolumeInMilliliters * aMultiplier < 50)
            {
                sStringOut = $"{(double)VolumeInMilliliters * aMultiplier / 15} tbsp.";
            }
            else if (VolumeInMilliliters * aMultiplier >= 5 && VolumeInMilliliters * aMultiplier < 15)
            {
                sStringOut = $"{(double)VolumeInMilliliters * aMultiplier / 5} tsp.";
            }
            else if (VolumeInMilliliters * aMultiplier < 5)
            {
                sStringOut = $"{(double)VolumeInMilliliters} ml";
            }

            return sStringOut;
        }

        /// <summary>
        /// Gets the quantity in a string
        /// </summary>
        /// <returns>A string with the qty + suffix</returns>
        public string GetQuantityString(double aMultiplier)
        {
            if (QuantityInPieces == null)
            {
                return "";
            }
            else
            {
                return $"{QuantityInPieces * aMultiplier} pcs";
            }
        }
        #endregion
    }
}
