using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace CzSharp.Utils.Extensions
{
    public static class ITempDataDictionaryExtensions
    {
        /// <summary>
        /// Creates success message which is sent with redirect
        /// </summary>
        /// <param name="tempData"></param>
        /// <param name="message"></param>
        public static void AddSuccessMessage(this ITempDataDictionary tempData, string message)
        {
            if (tempData.ContainsKey("SuccessMessage"))
            {
                tempData["SuccessMessage"] = message;
            }
            else
            {
                tempData.Add("SuccessMessage", message);
            }
        }
        
        /// <summary>
        /// Creates error message which is sent with redirect
        /// </summary>
        /// <param name="tempData"></param>
        /// <param name="message"></param>
        public static void AddErrorMessage(this ITempDataDictionary tempData, string message)
        {
            if (tempData.ContainsKey("ErrorMessage"))
            {
                tempData["ErrorMessage"] = message;
            }
            else
            {
                tempData.Add("ErrorMessage", message);
            }
        }
    }
}