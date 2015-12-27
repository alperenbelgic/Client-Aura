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

        public OrderController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.

            _NewObjectViewController = Frame.GetController<NewObjectViewController>();
            _NewObjectViewController.ObjectCreated += _NewObjectViewController_ObjectCreated;

            SetSaveButtonCaption();

            ArrangeActionVisibilitiies();
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
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        private void CompleteOrderAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            
            var order = this.View.CurrentObject as Order;

            if (order != null)
            {
                var result = order.SendOrderEvent(OrderEvents.OrderCreated);

                if (result.Succeeded)
                {
                    order.ObjectSpace.CommitChanges();
                    this.View.Refresh();
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

                    this.Application.ShowInformationBox(informationMessage);
                }
            }
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

        }

        private void MakeActionsVisibleForDraftState()
        {
            var modificationsController = Frame.GetController<ModificationsController>();
            modificationsController.SaveAction.Active.SetItemValue(GeneralKeys.ActionActiveKey, true);
        }

        private void HideAllActions()
        {
            //var modificationsController = Frame.GetController<ModificationsController>();
            //modificationsController.SaveAction.Active.SetItemValue(GeneralKeys.ActionActiveKey, false);

            //var newObjectViewController = Frame.GetController<NewObjectViewController>();
            //newObjectViewController.NewObjectAction.Active.SetItemValue(GeneralKeys.ActionActiveKey, false);
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


    }


}
