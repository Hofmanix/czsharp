using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace CzSharp.Utils.Extensions
{
    public static class ITempDataDictionaryExtensions
    {
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