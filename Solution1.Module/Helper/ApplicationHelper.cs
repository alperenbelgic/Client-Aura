using DevExpress.ExpressApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Solution1.Module.Helper
{
    public static class ApplicationHelper
    {
        public static void ShowInfoPopup(this XafApplication application, string informationText)
        {
            MethodInfo methodInfo = application.GetType().GetMethod("ShowInfoPopup");

            methodInfo.Invoke(application, new object[] { informationText });

        }

        public static void ShowInformationBox(this XafApplication application, InformationMessage informationMessage)
        {
            if (application is IShowInformationBox)
            {
                (application as IShowInformationBox).ShowInformationBox(informationMessage);
            }
        }
    }

    public interface IShowInformationBox
    {
        void ShowInformationBox(InformationMessage informationMessage);
    }


    public class InformationMessage
    {
        public InformationMessage(string message, MessageType messageType, View view)
        {
            this.Messages.Add(message);
            this.MessageType = messageType;
            this.View = view;
        }

        public InformationMessage(MessageType messageType, View view)
        {
            this.MessageType = messageType;
            this.View = view;
        }

        public View View { get; set; }

        public List<string> Messages = new List<string>();

        public MessageType MessageType { get; set; }
    }

    public enum MessageType
    {
        Information,
        Success,
        Warning,
        Error
    }
}
