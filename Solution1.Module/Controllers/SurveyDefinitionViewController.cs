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
using Solution1.Module.Helper;
using Solution1.Module.BusinessObjects;

namespace Solution1.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class SurveyDefinitionViewController : ViewController
    {
        public SurveyDefinitionViewController()
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

        private void MakeDefault_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var currentSurveyDefinition = this.View.CurrentObject as SurveyDefinition;
            var company = UserHelper.GetUsersCompany(this.View.ObjectSpace);
            var companiesAllSurveyDefinitions = this.View.ObjectSpace.GetObjects<SurveyDefinition>(
                new BinaryOperator("Company.Id", company.Id));

            foreach (var surveyDefiniton in companiesAllSurveyDefinitions)
            {
                surveyDefiniton.IsDefault = false;
            }

            currentSurveyDefinition.IsDefault = true;

            this.View.ObjectSpace.CommitChanges();

            this.View.Refresh();
        }


    }

    [CodeRule]
    public class DeleteSurveyDefinitionValidationRule : RuleBase
    {
        public DeleteSurveyDefinitionValidationRule() : base("", "Delete", typeof(SurveyDefinition))
        {

        }
        public DeleteSurveyDefinitionValidationRule(IRuleBaseProperties properties) : base(properties) { }

        protected override bool IsValidInternal(object target, out string errorMessageTemplate)
        {
            errorMessageTemplate = "Survey Definition cannot be deleted because your company should have at least one Survey Definition.";
            var surveyDefinition = target as SurveyDefinition;

            if (surveyDefinition.ObjectSpace != null && surveyDefinition.Company != null)
            {
                var objectSpace = surveyDefinition.ObjectSpace;
                var count =
                    objectSpace.GetObjectsCount(
                        typeof(SurveyDefinition), 
                        CriteriaOperator.And(
                            new BinaryOperator("Company.Id", surveyDefinition.Company.Id),
                            new BinaryOperator("IsDeleted", false)));

                return count > 1;
            }
            else
            {
                return true;
            }

        }
    }
}
