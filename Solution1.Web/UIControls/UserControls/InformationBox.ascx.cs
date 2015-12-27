using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Solution1.Web.UIControls.UserControls
{
    public partial class InformationBox : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Label1.Text = " hulooo";

        }

        protected override void Render(HtmlTextWriter writer)
        {
            var message = Helper.InfromationBoxHelper.GetInformationMessage(this.Page);

            Label1.Visible = false;
            if (message!= null)
            {
                Label1.Text = string.Join(", ", message.Messages.ToArray());
                Label1.Visible = true;
            }

            base.Render(writer);
        }
    }
}