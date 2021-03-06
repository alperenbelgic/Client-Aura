﻿namespace Solution1.Module.Controllers
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
            this.StartFeedbackProcess = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.StopProcessAndEdit = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // StartFeedbackProcess
            // 
            this.StartFeedbackProcess.Caption = "Start Feedback Process";
            this.StartFeedbackProcess.ConfirmationMessage = null;
            this.StartFeedbackProcess.Id = "StartFeedbackProcess";
            this.StartFeedbackProcess.TargetObjectType = typeof(Solution1.Module.BusinessObjects.Order);
            this.StartFeedbackProcess.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.StartFeedbackProcess.ToolTip = null;
            this.StartFeedbackProcess.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.StartFeedbackProcess.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.StartFeedbackProcessAction_Execute);
            // 
            // StopProcessAndEdit
            // 
            this.StopProcessAndEdit.Caption = "Stop Process and Edit";
            this.StopProcessAndEdit.ConfirmationMessage = null;
            this.StopProcessAndEdit.Id = "StopProcessAndEdit";
            this.StopProcessAndEdit.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.StopProcessAndEdit.ToolTip = null;
            this.StopProcessAndEdit.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.StopProcessAndEdit.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.StopProcessAndEdit_Execute);
            // 
            // OrderController
            // 
            this.Actions.Add(this.StartFeedbackProcess);
            this.Actions.Add(this.StopProcessAndEdit);
            this.TargetObjectType = typeof(Solution1.Module.BusinessObjects.Order);
            this.TypeOfView = typeof(DevExpress.ExpressApp.View);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction StartFeedbackProcess;
        private DevExpress.ExpressApp.Actions.SimpleAction StopProcessAndEdit;
    }
}
