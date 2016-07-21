using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using Solution1.Module.BusinessObjects.General;
using DevExpress.ExpressApp.Web.SystemModule;
using Solution1.Module.Helper;

namespace Solution1.Module.Web.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class DeleteController : WebDeleteObjectsViewController
    {
        public DeleteController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
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

        protected override void Delete(SimpleActionExecuteEventArgs args)
        {

            if (this.View is DetailView)
            {
                if (this.View.CurrentObject is IHaveIsDeletedMember)
                {
                    Validator.RuleSet.Validate(this.View.ObjectSpace, this.View.CurrentObject, "Delete");

                    SetIsDeletedTrue(this.View.CurrentObject);
                    this.ObjectSpace.CommitChanges();
                    this.View.Close();
                }
                else
                {
                    base.Delete(args);
                }
            }
            else if (this.View is ListView)
            {
                if (
                    (this.View as ListView).SelectedObjects.Count > 0 &&
                    (this.View as ListView).SelectedObjects[0] is IHaveIsDeletedMember
                  )
                {
                    foreach (var item in (this.View as ListView).SelectedObjects)
                    {
                        Validator.RuleSet.Validate(this.View.ObjectSpace, item, "Delete");

                        SetIsDeletedTrue(item);
                        this.ObjectSpace.CommitChanges();
                        this.View.Refresh();

                    }
                }
                else
                {
                    base.Delete(args);
                }
            }
            else
            {
                base.Delete(args);
            }
        }

        private void SetIsDeletedTrue(object obj)
        {
            (obj as IHaveIsDeletedMember).IsDeleted = true;
        }
    }
}
