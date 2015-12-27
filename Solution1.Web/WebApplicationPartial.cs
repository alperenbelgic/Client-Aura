using DevExpress.ExpressApp.Web;
using DevExpress.Web;
using Solution1.Module.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace Solution1.Web
{
    public partial class Solution1AspNetApplication : IShowInformationBox
    {
        public void ShowInfoPopup(string informationText)
        {
            
            var popup = new ASPxPopupControl();

            popup.ID =
            popup.ClientInstanceName =
            "clientAuraPopup";

            popup.HeaderText = "Info";

            popup.Modal = true;
            popup.AllowDragging = true;
            popup.ShowOnPageLoad = true;
            popup.Text = informationText;
            popup.PopupVerticalAlign = PopupVerticalAlign.WindowCenter;
            popup.PopupHorizontalAlign = PopupHorizontalAlign.WindowCenter;

            (WebWindow.CurrentRequestWindow.View.Control as Control).Controls.Add(popup);

            popup.ShowOnPageLoad = true;

            WebWindow.CurrentRequestWindow.RegisterStartupScript("y", "window.onload=clientAuraPopup.Show;");
        }

        public void ShowInformationBox(InformationMessage informationMessage)
        {
            Helper.InfromationBoxHelper.SetInformationMessage(informationMessage);
        }
    }
}