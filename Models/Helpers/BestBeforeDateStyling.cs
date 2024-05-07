namespace KitchenStock.Models.Helpers
{
    public static class BestBeforeDateStyling
    {
        //The limit before showing a warning on the stock
        private static TimeSpan mTimeInDaysBeforeWarning = TimeSpan.FromDays(2);

        /// <summary>
        /// Returns a css-styling string depending on the value of the Best Before Date
        /// </summary>
        /// <param name="aBestBeforeDate">The Best Before Date of the Stock</param>
        /// <returns>A css styling string</returns>
        public static string GetBestBeforeDateStylingForStock(DateTime? aBestBeforeDate)
        {
            if(!aBestBeforeDate.HasValue)
            {
                return "";
            }

            if(aBestBeforeDate.Value < DateTime.Now)
            {
                return "color: red; font-weight: bold;";
            }
            else if((aBestBeforeDate.Value - mTimeInDaysBeforeWarning) < DateTime.Now)
            {
                return "color: darkorange; font-weight: bold;";
            }

            return "";
        }
    }
}
