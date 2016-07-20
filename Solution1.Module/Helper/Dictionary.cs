using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution1.Module.Helper
{
    public static class Dictionary
    {
        public static string GetValue(string key)
        {
            if (Values.Keys.Contains(key))
            {
                return Values[key];
            }
            else
            {
                return key;
            }
        }

        private static Dictionary<string, string> Values = new Dictionary<string, string>()
        {
            {
                "Order_SurveyProcessStartedMessage",
@"Survey sending process started. Survey ""[SurveyName]"" will be sent to [CustomerName] [SurveySendingDays] days later. You can change these choices by clicking ""Stop Process and Edit"". You should start the process again after you made necessary changes."
            }
        };
    }
}
