namespace KitchenStock.Models
{
    public class RecipeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PortionCount { get; set; } = 4; //Default portion count is 4 (most common)
        public string Description { get; set; }
        public string CookingInstructions { get; set; }
        public int? Rating { get; set; } //1-5
        public List<RecipeIngredientModel> Ingredients { get; set; } = new List<RecipeIngredientModel>();
        public DateTime? CreateDate { get; set; } = DateTime.Now;
        public DateTime? UpdateDate { get; set; }

        #region Methods
        /// <summary>
        /// Gets the cooking instruction string in a list, sepereated by double new lines
        /// </summary>
        /// <returns>A list of cooking instructions</returns>
        public List<string> GetCookingInstructionList()
        {
            List<string> sInstructions = new List<string>();

            if(CookingInstructions.Length > 0)
            {
                sInstructions = CookingInstructions.Split("\n\n").ToList();

            }

            return sInstructions;
        }

        /// <summary>
        /// Get's a string of all ingredients names
        /// </summary>
        /// <returns>A comma seperated ingredients name string</returns>
        public string GetIngredientsString()
        {
            string sStringOut = "";

            if(Ingredients != null && Ingredients.Count > 0)
            {
                for(int i = 0; i < Ingredients.Count; i++)
                {
                    sStringOut += Ingredients[i].Ingredient.Name;

                    if(i < Ingredients.Count - 1)
                    {
                        sStringOut += ", ";
                    }
                }
            }

            return sStringOut;
        }

        /// <summary>
        /// Gets the number of kcal per portion
        /// </summary>
        /// <returns>Kcal/Portion</returns>
        public double GetKcalPerPortion(out bool aInaccurateResult)
        {
            double sKcalCount = 0;

            //Flag to keep track if the result is trustworthy or not (depending on conversions for ingredients set)
            aInaccurateResult = false;

            foreach (var sIngredient in Ingredients)
            {
                //If the measurement is in weight, just get the kcal/100g
                if (sIngredient.MeasurementType == MeasurementTypeEnum.Weight)
                {
                    sKcalCount += ((sIngredient.WeightInGrams ?? 0) / 100) * sIngredient.Ingredient.KcalPerHundredGrams;
                }

                //If its in volume, we have to convert the volume to weight
                else if (sIngredient.MeasurementType == MeasurementTypeEnum.Volume)
                {
                    var sWeightForVolume = sIngredient.Ingredient.ConvertVolumeInMillilitersToGrams((int)sIngredient.VolumeInMilliliters);

                    if (sWeightForVolume == null) aInaccurateResult = true;
                    else
                    {
                        sKcalCount += ((double)sWeightForVolume / 100) * sIngredient.Ingredient.KcalPerHundredGrams;
                    }
                }

                //Same for quantity, convert to weight
                else if (sIngredient.MeasurementType == MeasurementTypeEnum.Quantity)
                {
                    var sWeightForQty = sIngredient.Ingredient.ConvertQuantityInPiecesToGrams((int)sIngredient.QuantityInPieces);

                    if (sWeightForQty == null) aInaccurateResult = true;
                    else
                    {
                        sKcalCount += ((double)sWeightForQty / 100) * sIngredient.Ingredient.KcalPerHundredGrams;
                    }
                }
            }

            return sKcalCount / PortionCount;
        }

        /// <summary>
        /// Gets the number of carbs per portion in grams
        /// </summary>
        /// <returns>Carbs/Portion in grams</returns>
        public double GetCarbsPerPortion(out bool aInaccurateResult)
        {
            double sCarbsCount = 0;

            //Flag to keep track if the result is trustworthy or not (depending on conversions for ingredients set)
            aInaccurateResult = false;

            foreach (var sIngredient in Ingredients)
            {
                //If the measurement is in weight, just get the kcal/100g
                if (sIngredient.MeasurementType == MeasurementTypeEnum.Weight)
                {
                    sCarbsCount += ((sIngredient.WeightInGrams ?? 0) / 100) * sIngredient.Ingredient.CarbsPerHundredGrams;
                }

                //If its in volume, we have to convert the volume to weight
                else if (sIngredient.MeasurementType == MeasurementTypeEnum.Volume)
                {
                    var sWeightForVolume = sIngredient.Ingredient.ConvertVolumeInMillilitersToGrams((int)sIngredient.VolumeInMilliliters);

                    if (sWeightForVolume == null) aInaccurateResult = true;
                    else
                    {
                        sCarbsCount += ((double)sWeightForVolume / 100) * sIngredient.Ingredient.CarbsPerHundredGrams;
                    }
                }

                //Same for quantity, convert to weight
                else if (sIngredient.MeasurementType == MeasurementTypeEnum.Quantity)
                {
                    var sWeightForQty = sIngredient.Ingredient.ConvertQuantityInPiecesToGrams((int)sIngredient.QuantityInPieces);

                    if (sWeightForQty == null) aInaccurateResult = true;
                    else
                    {
                        sCarbsCount += ((double)sWeightForQty / 100) * sIngredient.Ingredient.CarbsPerHundredGrams;
                    }
                }
            }

            return sCarbsCount / PortionCount;
        }

        /// <summary>
        /// Gets the number of fats per portion in grams
        /// </summary>
        /// <returns>Fats/Portion in grams</returns>
        public double GetProteinsPerPortion(out bool aInaccurateResult)
        {
            double sProteinCount = 0;

            //Flag to keep track if the result is trustworthy or not (depending on conversions for ingredients set)
            aInaccurateResult = false;

            foreach (var sIngredient in Ingredients)
            {
                //If the measurement is in weight, just get the kcal/100g
                if (sIngredient.MeasurementType == MeasurementTypeEnum.Weight)
                {
                    sProteinCount += ((sIngredient.WeightInGrams ?? 0) / 100) * sIngredient.Ingredient.ProteinPerHundredGrams;
                }

                //If its in volume, we have to convert the volume to weight
                else if (sIngredient.MeasurementType == MeasurementTypeEnum.Volume)
                {
                    var sWeightForVolume = sIngredient.Ingredient.ConvertVolumeInMillilitersToGrams((int)sIngredient.VolumeInMilliliters);

                    if (sWeightForVolume == null) aInaccurateResult = true;
                    else
                    {
                        sProteinCount += ((double)sWeightForVolume / 100) * sIngredient.Ingredient.ProteinPerHundredGrams;
                    }
                }

                //Same for quantity, convert to weight
                else if (sIngredient.MeasurementType == MeasurementTypeEnum.Quantity)
                {
                    var sWeightForQty = sIngredient.Ingredient.ConvertQuantityInPiecesToGrams((int)sIngredient.QuantityInPieces);

                    if (sWeightForQty == null) aInaccurateResult = true;
                    else
                    {
                        sProteinCount += ((double)sWeightForQty / 100) * sIngredient.Ingredient.ProteinPerHundredGrams;
                    }
                }
            }

            return sProteinCount / PortionCount;
        }

        /// <summary>
        /// Gets the number of fats per portion in grams
        /// </summary>
        /// <returns>Fats/Portion in grams</returns>
        public double GetFatsPerPortion(out bool aInaccurateResult)
        {
            double sFatsCount = 0;

            //Flag to keep track if the result is trustworthy or not (depending on conversions for ingredients set)
            aInaccurateResult = false;

            foreach (var sIngredient in Ingredients)
            {
                //If the measurement is in weight, just get the kcal/100g
                if (sIngredient.MeasurementType == MeasurementTypeEnum.Weight)
                {
                    sFatsCount += ((sIngredient.WeightInGrams ?? 0) / 100) * sIngredient.Ingredient.FatsPerHundredGrams;
                }

                //If its in volume, we have to convert the volume to weight
                else if (sIngredient.MeasurementType == MeasurementTypeEnum.Volume)
                {
                    var sWeightForVolume = sIngredient.Ingredient.ConvertVolumeInMillilitersToGrams((int)sIngredient.VolumeInMilliliters);

                    if (sWeightForVolume == null) aInaccurateResult = true;
                    else
                    {
                        sFatsCount += ((double)sWeightForVolume / 100) * sIngredient.Ingredient.FatsPerHundredGrams;
                    }
                }

                //Same for quantity, convert to weight
                else if (sIngredient.MeasurementType == MeasurementTypeEnum.Quantity)
                {
                    var sWeightForQty = sIngredient.Ingredient.ConvertQuantityInPiecesToGrams((int)sIngredient.QuantityInPieces);

                    if (sWeightForQty == null) aInaccurateResult = true;
                    else
                    {
                        sFatsCount += ((double)sWeightForQty / 100) * sIngredient.Ingredient.FatsPerHundredGrams;
                    }
                }
            }

            return sFatsCount / PortionCount;
        }
        #endregion
    }
}
