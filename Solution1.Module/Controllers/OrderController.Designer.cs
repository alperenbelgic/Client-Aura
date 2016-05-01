namespace Solution1.Module.Controllers
{
    partial class OrderController
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.CompleteOrderAction = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // CompleteOrderAction
            // 
            this.CompleteOrderAction.Caption = "Complete Order";
            this.CompleteOrderAction.ConfirmationMessage = null;
            this.CompleteOrderAction.Id = "CompleteOrderAction";
            this.CompleteOrderAction.TargetObjectType = typeof(Solution1.Module.BusinessObjects.Order);
            this.CompleteOrderAction.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.CompleteOrderAction.ToolTip = null;
            this.CompleteOrderAction.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.CompleteOrderAction.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.CompleteOrderAction_Execute);
            // 
            // OrderController
            // 
            this.Actions.Add(this.CompleteOrderAction);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction CompleteOrderAction;
    }
}
