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
using Solution1.Module.BusinessObjects;
using Solution1.Module.Helper;

namespace Solution1.Module.Controllers.GeneralControllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class CompanyFilterListViewController : ObjectViewController<ListView, IBusinessObject>
    {
        private string CompanyMemberName = "Company";
        private string IsDeletedMemberName = "IsDeleted";

        public CompanyFilterListViewController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.

        }

        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.

            FilterForCompany();

            // warning: this call and Controller's name is incompitable.
            FilterForIsDeletedProperty();
        }

        private void FilterForCompany()
        {
            var user = (SecuritySystem.CurrentUser as TheUser);
            if (!user.Roles.Any(r => r.Name == RoleNames.Administrators))
            {
                var hasCompanyMember = this.View.ObjectTypeInfo.Members.Any(m => m.Name == CompanyMemberName);

                if (hasCompanyMember)
                {
                    View.CollectionSource.Criteria["CompanyCriteria"] = new BinaryOperator("Company.Id", user.Company.Id, BinaryOperatorType.Equal);
                }
            }
        }

        private void FilterForIsDeletedProperty()
        {
            var hasIsDeletedMember = this.View.ObjectTypeInfo.Members.Any(m => m.Name == IsDeletedMemberName);

            if (hasIsDeletedMember)
            {
                View.CollectionSource.Criteria["IsDeletedCriteria"] = new BinaryOperator("IsDeleted", false);
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
