using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;
using Solution1.Module.Helper;
using System.Collections.Generic;

namespace Solution1.Module.Controllers.GeneralControllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class NestedCollectionViewActionRemoverController : ViewController
    {
        List<string> LinkUnlinkRemovingViewIds = new List<string> {
            "Order_OrderItems_ListView",
            "SurveyDefinition_Questions_ListView"
        };

        string[] NewRemovingViewIdArray = new string[] { };
        string[] DeleteRemovingViewIdArray = new string[] { };
        string[] EditRemovingViewIdArray = new string[] { };

        public NestedCollectionViewActionRemoverController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.

            RemoveLinkUnlinkActions();

        }

        void RemoveLinkUnlinkActions()
        {
            if (this.LinkUnlinkRemovingViewIds.Contains(this.View.Id))
            {
                var linkUnlinkController = Frame.GetController<LinkUnlinkController>();
                linkUnlinkController.LinkAction.Active.SetItemValue(GeneralKeys.ActionActiveKey, false);
                linkUnlinkController.UnlinkAction.Active.SetItemValue(GeneralKeys.ActionActiveKey, false);
            }
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
