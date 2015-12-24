using System;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.Web;
using System.Collections.Generic;
using DevExpress.ExpressApp.EF;
using Solution1.Module.BusinessObjects;
using System.Data.Common;

namespace Solution1.Web {
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/DevExpressExpressAppWebWebApplicationMembersTopicAll.aspx
    public partial class Solution1AspNetApplication : WebApplication {
        private DevExpress.ExpressApp.SystemModule.SystemModule module1;
        private DevExpress.ExpressApp.Web.SystemModule.SystemAspNetModule module2;
        private Solution1.Module.Solution1Module module3;
        private Solution1.Module.Web.Solution1AspNetModule module4;
        private DevExpress.ExpressApp.Security.SecurityModule securityModule1;
        private DevExpress.ExpressApp.Security.SecurityStrategyComplex securityStrategyComplex1;
        private DevExpress.ExpressApp.Security.AuthenticationStandard authenticationStandard1;
        private DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule objectsModule;
        private DevExpress.ExpressApp.Chart.ChartModule chartModule;
        private DevExpress.ExpressApp.Chart.Web.ChartAspNetModule chartAspNetModule;
        private DevExpress.ExpressApp.ConditionalAppearance.ConditionalAppearanceModule conditionalAppearanceModule;
        private DevExpress.ExpressApp.FileAttachments.Web.FileAttachmentsAspNetModule fileAttachmentsAspNetModule;
        private DevExpress.ExpressApp.HtmlPropertyEditor.Web.HtmlPropertyEditorAspNetModule htmlPropertyEditorAspNetModule;
        private DevExpress.ExpressApp.Kpi.KpiModule kpiModule;
        private DevExpress.ExpressApp.Maps.Web.MapsAspNetModule mapsAspNetModule;
        private DevExpress.ExpressApp.Notifications.NotificationsModule notificationsModule;
        private DevExpress.ExpressApp.Notifications.Web.NotificationsAspNetModule notificationsAspNetModule;
        private DevExpress.ExpressApp.PivotChart.PivotChartModuleBase pivotChartModuleBase;
        private DevExpress.ExpressApp.PivotChart.Web.PivotChartAspNetModule pivotChartAspNetModule;
        private DevExpress.ExpressApp.PivotGrid.PivotGridModule pivotGridModule;
        private DevExpress.ExpressApp.PivotGrid.Web.PivotGridAspNetModule pivotGridAspNetModule;
        private DevExpress.ExpressApp.ReportsV2.ReportsModuleV2 reportsModuleV2;
        private DevExpress.ExpressApp.ReportsV2.Web.ReportsAspNetModuleV2 reportsAspNetModuleV2;
        private DevExpress.ExpressApp.Scheduler.SchedulerModuleBase schedulerModuleBase;
        private DevExpress.ExpressApp.Scheduler.Web.SchedulerAspNetModule schedulerAspNetModule;
        private DevExpress.ExpressApp.ScriptRecorder.ScriptRecorderModuleBase scriptRecorderModuleBase;
        private DevExpress.ExpressApp.ScriptRecorder.Web.ScriptRecorderAspNetModule scriptRecorderAspNetModule;
        private DevExpress.ExpressApp.StateMachine.StateMachineModule stateMachineModule;
        private DevExpress.ExpressApp.TreeListEditors.TreeListEditorsModuleBase treeListEditorsModuleBase;
        private DevExpress.ExpressApp.TreeListEditors.Web.TreeListEditorsAspNetModule treeListEditorsAspNetModule;
        private DevExpress.ExpressApp.Validation.ValidationModule validationModule;
        private DevExpress.ExpressApp.Validation.Web.ValidationAspNetModule validationAspNetModule;
        private DevExpress.ExpressApp.ViewVariantsModule.ViewVariantsModule viewVariantsModule;
        private DevExpress.ExpressApp.Workflow.WorkflowModule workflowModule;

        public Solution1AspNetApplication() {
            InitializeComponent();
        }
        protected override void CreateDefaultObjectSpaceProvider(CreateCustomObjectSpaceProviderEventArgs args) {
            if(args.Connection != null) {
                args.ObjectSpaceProvider = new EFObjectSpaceProvider(typeof(Solution1DbContext), TypesInfo, null, (DbConnection)args.Connection);
            }
            else {
                args.ObjectSpaceProvider = new EFObjectSpaceProvider(typeof(Solution1DbContext), TypesInfo, null, args.ConnectionString);
            }
            args.ObjectSpaceProviders.Add(new NonPersistentObjectSpaceProvider(TypesInfo, null));
        }
        private void Solution1AspNetApplication_DatabaseVersionMismatch(object sender, DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs e) {
#if EASYTEST
            e.Updater.Update();
            e.Handled = true;
#else
            if(System.Diagnostics.Debugger.IsAttached) {
                e.Updater.Update();
                e.Handled = true;
            }
            else {
                string message = "The application cannot connect to the specified database, because the latter doesn't exist or its version is older than that of the application.\r\n" +
                    "This error occurred  because the automatic database update was disabled when the application was started without debugging.\r\n" +
                    "To avoid this error, you should either start the application under Visual Studio in debug mode, or modify the " +
                    "source code of the 'DatabaseVersionMismatch' event handler to enable automatic database update, " +
                    "or manually create a database using the 'DBUpdater' tool.\r\n" +
                    "Anyway, refer to the following help topics for more detailed information:\r\n" +
                    "'Update Application and Database Versions' at http://help.devexpress.com/#Xaf/CustomDocument2795\r\n" +
                    "'Database Security References' at http://help.devexpress.com/#Xaf/CustomDocument3237\r\n" +
                    "If this doesn't help, please contact our Support Team at http://www.devexpress.com/Support/Center/";

                if(e.CompatibilityError != null && e.CompatibilityError.Exception != null) {
                    message += "\r\n\r\nInner exception: " + e.CompatibilityError.Exception.Message;
                }
                throw new InvalidOperationException(message);
            }
#endif
        }
        private void InitializeComponent() {
            this.module1 = new DevExpress.ExpressApp.SystemModule.SystemModule();
            this.module2 = new DevExpress.ExpressApp.Web.SystemModule.SystemAspNetModule();
            this.objectsModule = new DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule();
            this.chartModule = new DevExpress.ExpressApp.Chart.ChartModule();
            this.chartAspNetModule = new DevExpress.ExpressApp.Chart.Web.ChartAspNetModule();
            this.conditionalAppearanceModule = new DevExpress.ExpressApp.ConditionalAppearance.ConditionalAppearanceModule();
            this.fileAttachmentsAspNetModule = new DevExpress.ExpressApp.FileAttachments.Web.FileAttachmentsAspNetModule();
            this.htmlPropertyEditorAspNetModule = new DevExpress.ExpressApp.HtmlPropertyEditor.Web.HtmlPropertyEditorAspNetModule();
            this.kpiModule = new DevExpress.ExpressApp.Kpi.KpiModule();
            this.mapsAspNetModule = new DevExpress.ExpressApp.Maps.Web.MapsAspNetModule();
            this.notificationsModule = new DevExpress.ExpressApp.Notifications.NotificationsModule();
            this.notificationsAspNetModule = new DevExpress.ExpressApp.Notifications.Web.NotificationsAspNetModule();
            this.pivotChartModuleBase = new DevExpress.ExpressApp.PivotChart.PivotChartModuleBase();
            this.pivotChartAspNetModule = new DevExpress.ExpressApp.PivotChart.Web.PivotChartAspNetModule();
            this.pivotGridModule = new DevExpress.ExpressApp.PivotGrid.PivotGridModule();
            this.pivotGridAspNetModule = new DevExpress.ExpressApp.PivotGrid.Web.PivotGridAspNetModule();
            this.reportsModuleV2 = new DevExpress.ExpressApp.ReportsV2.ReportsModuleV2();
            this.reportsAspNetModuleV2 = new DevExpress.ExpressApp.ReportsV2.Web.ReportsAspNetModuleV2();
            this.schedulerModuleBase = new DevExpress.ExpressApp.Scheduler.SchedulerModuleBase();
            this.schedulerAspNetModule = new DevExpress.ExpressApp.Scheduler.Web.SchedulerAspNetModule();
            this.scriptRecorderModuleBase = new DevExpress.ExpressApp.ScriptRecorder.ScriptRecorderModuleBase();
            this.scriptRecorderAspNetModule = new DevExpress.ExpressApp.ScriptRecorder.Web.ScriptRecorderAspNetModule();
            this.stateMachineModule = new DevExpress.ExpressApp.StateMachine.StateMachineModule();
            this.treeListEditorsModuleBase = new DevExpress.ExpressApp.TreeListEditors.TreeListEditorsModuleBase();
            this.treeListEditorsAspNetModule = new DevExpress.ExpressApp.TreeListEditors.Web.TreeListEditorsAspNetModule();
            this.validationModule = new DevExpress.ExpressApp.Validation.ValidationModule();
            this.validationAspNetModule = new DevExpress.ExpressApp.Validation.Web.ValidationAspNetModule();
            this.viewVariantsModule = new DevExpress.ExpressApp.ViewVariantsModule.ViewVariantsModule();
            this.workflowModule = new DevExpress.ExpressApp.Workflow.WorkflowModule();
            this.securityModule1 = new DevExpress.ExpressApp.Security.SecurityModule();
            this.securityStrategyComplex1 = new DevExpress.ExpressApp.Security.SecurityStrategyComplex();
            this.authenticationStandard1 = new DevExpress.ExpressApp.Security.AuthenticationStandard();
            this.module3 = new Solution1.Module.Solution1Module();
            this.module4 = new Solution1.Module.Web.Solution1AspNetModule();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // notificationsModule
            // 
            this.notificationsModule.CanAccessPostponedItems = false;
            this.notificationsModule.NotificationsRefreshInterval = System.TimeSpan.Parse("00:05:00");
            this.notificationsModule.NotificationsStartDelay = System.TimeSpan.Parse("00:00:05");
            this.notificationsModule.ShowNotificationsWindow = true;
            // 
            // pivotChartModuleBase
            // 
            this.pivotChartModuleBase.ShowAdditionalNavigation = true;
            // 
            // reportsModuleV2
            // 
            this.reportsModuleV2.EnableInplaceReports = true;
            this.reportsModuleV2.ReportDataType = typeof(DevExpress.Persistent.BaseImpl.EF.ReportDataV2);
            this.reportsModuleV2.ReportStoreMode = DevExpress.ExpressApp.ReportsV2.ReportStoreModes.XML;
            this.reportsModuleV2.ShowAdditionalNavigation = true;
            // 
            // reportsAspNetModuleV2
            // 
            this.reportsAspNetModuleV2.ReportViewerType = DevExpress.ExpressApp.ReportsV2.Web.ReportViewerTypes.HTML5;
            this.reportsAspNetModuleV2.ShowFormatSpecificExportActions = true;
            // 
            // stateMachineModule
            // 
            this.stateMachineModule.StateMachineStorageType = typeof(DevExpress.ExpressApp.StateMachine.Xpo.XpoStateMachine);
            // 
            // validationModule
            // 
            this.validationModule.AllowValidationDetailsAccess = true;
            this.validationModule.IgnoreWarningAndInformationRules = false;
            // 
            // viewVariantsModule
            // 
            this.viewVariantsModule.ShowAdditionalNavigation = true;
            // 
            // workflowModule
            // 
            this.workflowModule.RunningWorkflowInstanceInfoType = typeof(DevExpress.ExpressApp.Workflow.EF.EFRunningWorkflowInstanceInfo);
            this.workflowModule.StartWorkflowRequestType = typeof(DevExpress.ExpressApp.Workflow.EF.EFStartWorkflowRequest);
            this.workflowModule.UserActivityVersionType = typeof(DevExpress.ExpressApp.Workflow.Versioning.EFUserActivityVersion);
            this.workflowModule.WorkflowControlCommandRequestType = typeof(DevExpress.ExpressApp.Workflow.EF.EFWorkflowInstanceControlCommandRequest);
            this.workflowModule.WorkflowDefinitionType = typeof(DevExpress.ExpressApp.Workflow.EF.EFWorkflowDefinition);
            this.workflowModule.WorkflowInstanceKeyType = typeof(DevExpress.Workflow.EF.EFInstanceKey);
            this.workflowModule.WorkflowInstanceType = typeof(DevExpress.Workflow.EF.EFWorkflowInstance);
            // 
            // securityStrategyComplex1
            // 
            this.securityStrategyComplex1.Authentication = this.authenticationStandard1;
            this.securityStrategyComplex1.RoleType = typeof(DevExpress.Persistent.BaseImpl.EF.Role);
            this.securityStrategyComplex1.UserType = typeof(Solution1.Module.BusinessObjects.TheUser);
            // 
            // authenticationStandard1
            // 
            this.authenticationStandard1.LogonParametersType = typeof(DevExpress.ExpressApp.Security.AuthenticationStandardLogonParameters);
            // 
            // Solution1AspNetApplication
            // 
            this.ApplicationName = "Solution1";
            this.CheckCompatibilityType = DevExpress.ExpressApp.CheckCompatibilityType.DatabaseSchema;
            this.LinkNewObjectToParentImmediately = false;
            this.Modules.Add(this.module1);
            this.Modules.Add(this.module2);
            this.Modules.Add(this.objectsModule);
            this.Modules.Add(this.chartModule);
            this.Modules.Add(this.conditionalAppearanceModule);
            this.Modules.Add(this.validationModule);
            this.Modules.Add(this.kpiModule);
            this.Modules.Add(this.notificationsModule);
            this.Modules.Add(this.pivotChartModuleBase);
            this.Modules.Add(this.pivotGridModule);
            this.Modules.Add(this.reportsModuleV2);
            this.Modules.Add(this.schedulerModuleBase);
            this.Modules.Add(this.scriptRecorderModuleBase);
            this.Modules.Add(this.stateMachineModule);
            this.Modules.Add(this.treeListEditorsModuleBase);
            this.Modules.Add(this.viewVariantsModule);
            this.Modules.Add(this.workflowModule);
            this.Modules.Add(this.module3);
            this.Modules.Add(this.chartAspNetModule);
            this.Modules.Add(this.fileAttachmentsAspNetModule);
            this.Modules.Add(this.htmlPropertyEditorAspNetModule);
            this.Modules.Add(this.mapsAspNetModule);
            this.Modules.Add(this.notificationsAspNetModule);
            this.Modules.Add(this.pivotChartAspNetModule);
            this.Modules.Add(this.pivotGridAspNetModule);
            this.Modules.Add(this.reportsAspNetModuleV2);
            this.Modules.Add(this.schedulerAspNetModule);
            this.Modules.Add(this.scriptRecorderAspNetModule);
            this.Modules.Add(this.treeListEditorsAspNetModule);
            this.Modules.Add(this.validationAspNetModule);
            this.Modules.Add(this.securityModule1);
            this.Modules.Add(this.module4);
            this.Security = this.securityStrategyComplex1;
            this.DatabaseVersionMismatch += new System.EventHandler<DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs>(this.Solution1AspNetApplication_DatabaseVersionMismatch);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
    }
}
