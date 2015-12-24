using System;
using System.Collections.Generic;
using System.Web.UI;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Web.Templates;
using DevExpress.ExpressApp.Web.Templates.ActionContainers;
using System.Web.UI.HtmlControls;

public partial class Default : BaseXafPage
{
    protected override ContextActionsMenu CreateContextActionsMenu()
    {
        return new ContextActionsMenu(this, "Edit", "RecordEdit", "ObjectsCreation", "ListView", "Reports");
    }
    public override Control InnerContentPlaceHolder
    {
        get
        {
            return Content;
        }
    }




    private List<Control> GetControls(Control c)
    {
        var list = new List<Control>();
        foreach (Control item in c.Controls)
        {
            if (item.Controls.Count > 0 )
            {
                list.AddRange(GetControls(item));
            }
            list.Add(item);
            

        }
        return list;
    }


}
