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
using DevExpress.ExpressApp.Validation;
using DevExpress.ExpressApp.Web;

namespace Solution1.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class OrderController : ViewController
    {

        private NewObjectViewController _NewObjectViewController = null;
        private ModificationsController _ModificationsController = null;

        private Order CurrentOrderObject
        {
            get
            {
                return this.View.CurrentObject as Order;

            }
        }

        public OrderController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.

            _ModificationsController = Frame.GetController<ModificationsController>();

            _NewObjectViewController = Frame.GetController<NewObjectViewController>();
            _NewObjectViewController.ObjectCreated += _NewObjectViewController_ObjectCreated;

            this.ObjectSpace.ObjectChanged += ObjectSpace_ObjectChanged;


            ArrangeUI();
        }

        private void ObjectSpace_ObjectChanged(object sender, ObjectChangedEventArgs e)
        {
            if (e.PropertyName == "OrderSurvey.Survey")
            {
                // because object changed event triggerd before object changed, we subscribe object's property change event
                this.CurrentOrderObject.OrderSurvey.PropertyChanged += OrderSurvey_PropertyChanged;
            }
        }

        private void OrderSurvey_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Survey" && this.CurrentOrderObject.OrderSurvey.Survey != null)
            {
                this.CurrentOrderObject.OrderSurvey.SurveySendingDays =
                this.CurrentOrderObject.OrderSurvey.Survey.SurveySendingDays;

                this.CurrentOrderObject.OrderSurvey.PropertyChanged -= OrderSurvey_PropertyChanged;
            }
        }

        private void UpdateSurveySendingDaysFromEditorValues()
        {
            var detaiilView = this.View as DetailView;
            if (detaiilView != null)
            {
                var propertyEditors = detaiilView.GetItems<PropertyEditor>();
                var surveyEditor = propertyEditors.FirstOrDefault(pe => pe.Id == "OrderSurvey.Survey");
                var surveySendingDaysEditor = propertyEditors.FirstOrDefault(pe => pe.Id == "OrderSurvey.SurveySendingDays");
                if (surveyEditor != null && surveySendingDaysEditor != null)
                {
                    var surveyDefinition = surveyEditor.PropertyValue as SurveyDefinition;
                    if (surveyDefinition != null)
                    {
                        surveySendingDaysEditor.PropertyValue = surveyDefinition.SurveySendingDays;
                    }
                }
            }
        }

        private void ArrangeUI()
        {
            ArrangePropertyEditorEditibleMode();

            ArrangeActionVisibilitiies();

            SetSaveButtonCaption();
        }

        private void ArrangePropertyEditorEditibleMode()
        {
            if (this.View is DetailView)
            {
                var propertyEditors = (this.View as DetailView).GetItems<PropertyEditor>();

                foreach (var propertyEditor in propertyEditors)
                {
                    propertyEditor.AllowEdit.RemoveItem(GeneralKeys.ActionActiveKey);
                }

                if (this.CurrentOrderObject.OrderStatus != OrderStates.Draft)
                {

                    foreach (PropertyEditor propertyEditor in propertyEditors)
                    {
                        propertyEditor.AllowEdit.SetItemValue(GeneralKeys.ActionActiveKey, false);
                    }
                }
            }
        }

        private void _NewObjectViewController_ObjectCreated(object sender, ObjectCreatedEventArgs e)
        {
            var createdObject = e.CreatedObject;
            if (createdObject != null && createdObject is Order)
            {
                var order = createdObject as Order;

                order.OrderDate = DateTime.Now;

                var currentUser = UserHelper.GetCurrentUser();
                if (currentUser != null && currentUser.Company != null)
                {
                    order.Company = order.ObjectSpace.GetObjectByKey<Company>(currentUser.Company.Id);
                }

                order.IntegrationSource = "User Interface";
            }
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.

        }
        protected override void OnDeactivated()
        {
            _NewObjectViewController.ObjectCreated -= _NewObjectViewController_ObjectCreated;

            ResetSaveButtonCaption();

            this.ObjectSpace.ObjectChanged -= ObjectSpace_ObjectChanged;

            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        private void StartFeedbackProcessAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var order = this.View.CurrentObject as Order;

            if (order != null)
            {
                var result = order.SendOrderEvent(OrderEvents.OrderCreated);

                if (result.Succeeded)
                {
                    order.ObjectSpace.CommitChanges();
                    this.View.Refresh();
                    ApplicationHelper.ShowInfoPopup(this.Application, GetFeedbackProcessStartedMessage());
                }
                else
                {
                    var informationMessage = new InformationMessage(MessageType.Error, this.View);
                    if (result.Reasons.Contains(Order.OrderProcessingResultTypes.ThereIsNoOrderItem))
                    {
                        informationMessage.Messages.Add("You should add at least one product.");
                    }
                    if (result.Reasons.Contains(Order.OrderProcessingResultTypes.NoCustomerSelected))
                    {
                        informationMessage.Messages.Add("You should choose a customer.");
                    }
                    if (result.Reasons.Contains(Order.OrderProcessingResultTypes.CustomerHasNoEmailAddress))
                    {
                        informationMessage.Messages.Add("Customer should have an email address.");
                    }
                    if (result.Reasons.Contains(Order.OrderProcessingResultTypes.NoSurveySelected))
                    {
                        informationMessage.Messages.Add("A survey should be selected on Order page's detail tab.");
                    }

                    this.Application.ShowInformationBox(informationMessage);
                }
            }

            ArrangeUI();
        }

        private string GetFeedbackProcessStartedMessage()
        {
            var messageContent = Dictionary.GetValue("Order_SurveyProcessStartedMessage");
            messageContent =
            messageContent
            .Replace("[CustomerName]", this.CurrentOrderObject.Customer.CustomerName)
            .Replace("[SurveySendingDays]", this.CurrentOrderObject.OrderSurvey.SurveySendingDays.ToString())
            .Replace("[SurveyName]", this.CurrentOrderObject.OrderSurvey.Survey.SurveyName);
            return messageContent;
        }

        private void ArrangeActionVisibilitiies()
        {
            if (this.View is DetailView)
            {
                var order = this.View.CurrentObject as Order;
                string orderStatus = order.OrderStatus;

                HideAllActions();

                if (orderStatus == OrderStates.Draft || orderStatus == OrderStates.Draft_NotSaved)
                {
                    MakeActionsVisibleForDraftState();
                }
                else if (orderStatus == OrderStates.SurveySendingWaiting)
                {
                    MakeActionsVisibleForSurveySendingWaiting();
                }
                else if (orderStatus == OrderStates.FeedBackWaiting)
                {
                    MakeActionsVisibleForFeedbackWaiting();
                }
                else if (orderStatus == OrderStates.FeedbackReached)
                {
                    MakeActionsVisibleForFeedbackReached();
                }
            }
        }

        private void MakeActionsVisibleForFeedbackReached()
        {

        }

        private void MakeActionsVisibleForFeedbackWaiting()
        {

        }

        private void MakeActionsVisibleForSurveySendingWaiting()
        {
            this.StopProcessAndEdit.Active.SetItemValue(GeneralKeys.ActionActiveKey, true);
        }

        private void MakeActionsVisibleForDraftState()
        {
            this.StartFeedbackProcess.Active.SetItemValue(GeneralKeys.ActionActiveKey, true);

            _ModificationsController.SaveAction.Active.SetItemValue(GeneralKeys.ActionActiveKey, true);
        }

        private void HideAllActions()
        {
            _ModificationsController.SaveAction.Active.SetItemValue(GeneralKeys.ActionActiveKey, false);

            this.StartFeedbackProcess.Active.SetItemValue(GeneralKeys.ActionActiveKey, false);

            this.StopProcessAndEdit.Active.SetItemValue(GeneralKeys.ActionActiveKey, false);

        }

        private void SetSaveButtonCaption()
        {
            var modificationsController = Frame.GetController<ModificationsController>();
            modificationsController.SaveAction.Caption = "Save as Draft";

            // baska bir action olustur
            // caption degistir
            // save action'i cagir
        }

        private void ResetSaveButtonCaption()
        {
            var modificationsController = Frame.GetController<ModificationsController>();
            modificationsController.SaveAction.Caption = GeneralKeys.SaveButtonDefaultCaption;
        }

        private void StopProcessAndEdit_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            if (this.CurrentOrderObject != null)
            {
                var result = this.CurrentOrderObject.SendOrderEvent(OrderEvents.StopProcessAndEdit);

                ArrangeUI();
            }
        }
    }


}
