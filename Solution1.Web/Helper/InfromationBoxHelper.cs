using Solution1.Module.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace Solution1.Web.Helper
{
    public class InfromationBoxHelper
    {
        private const string InformationBoxSessionKey = "ClientAura_InformationBoxSessionKey";
        public static InformationMessage GetInformationMessage(Page page)
        {
            var list = SessionHelper.GetSessionValue<List<InformationMessage>>(InformationBoxSessionKey);

            if (list != null)
            {
                var message = list.FirstOrDefault(im => page == (im.View.Control as System.Web.UI.Control).Page);

                if (message != null)
                {
                    list.Remove(message);
                    SessionHelper.AssignSessionValue(InformationBoxSessionKey, list);

                    return message;
                }
            }

            return null;
        }

        public static void SetInformationMessage(InformationMessage informationMessage)
        {
            var list = SessionHelper.GetSessionValue<List<InformationMessage>>(InformationBoxSessionKey);

            if (list == null)
            {
                list = new List<InformationMessage>();
            }

            list.Add(informationMessage);

            SessionHelper.AssignSessionValue(InformationBoxSessionKey, list);
        }
    }
}