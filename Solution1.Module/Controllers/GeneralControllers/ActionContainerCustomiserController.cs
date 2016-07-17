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

namespace Solution1.Module.Controllers.GeneralControllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class ActionContainerCustomiserController : ViewController
    {

        //FillActionContainersController _FillActionContainersController = null;
        //ActionControlsSiteController _ActionControlsSiteController = null;


        public ActionContainerCustomiserController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }


        protected override void OnAfterConstruction()
        {
            base.OnAfterConstruction();
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.

            //_ActionControlsSiteController = Frame.GetController<ActionControlsSiteController>();
            //_ActionControlsSiteController.CustomizeContainerActions += _FillActionContainersController_CustomizeContainerActions;

            //_FillActionContainersController = Frame.GetController<FillActionContainersController>();
            //_FillActionContainersController.CustomizeContainerActions += _FillActionContainersController_CustomizeContainerActions;


            //Frame.GetController<ModificationsController>().SaveAction.Category = "Unspecified";
        }


        //private void _FillActionContainersController_CustomizeContainerActions(object sender, CustomizeContainerActionsEventArgs e)
        //{
        //    CustomiseRootDetailViewActionsContainer(e);
        //}

        //private void CustomiseRootDetailViewActionsContainer(CustomizeContainerActionsEventArgs e)
        //{
        //    //if (this.View is DetailView && 
        //    //    (this.View as DetailView).ViewEditMode ==  ViewEditMode.Edit &&
        //    //    this.View.IsRoot
        //    //    )
        //    //{
        //    //    var saveAction = Frame.GetController<ModificationsController>().SaveAction;
        //    //    var deleteAction = Frame.GetController<DeleteObjectsViewController>().DeleteAction;
        //    //    var newAction = Frame.GetController<NewObjectViewController>().NewObjectAction;

        //    //    if (e.Category != "Unspecified")
        //    //    {
        //    //        bool saveActionRemoved = e.ContainerActions.Remove(saveAction);
        //    //        bool deleteActionRemoved = e.ContainerActions.Remove(deleteAction);
        //    //        bool newActionRemoved = e.ContainerActions.Remove(newAction);
        //    //    }
        //    //    else
        //    //    {
        //    //        e.ContainerActions.Add(saveAction);
        //    //        e.ContainerActions.Add(deleteAction);
        //    //        e.ContainerActions.Add(newAction);
        //    //    } 
        //    //}

        //    //this.View.Refresh();
            
        //}


        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            //_FillActionContainersController.CustomizeContainerActions -= _FillActionContainersController_CustomizeContainerActions;
            //_ActionControlsSiteController.CustomizeContainerActions -= _FillActionContainersController_CustomizeContainerActions;

            //Frame.GetController<ModificationsController>().SaveAction.Category = "Save";


            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
