using System;
using System.Data;
using System.Linq;
using System.Data.Entity;
using System.Data.Common;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.ComponentModel;
using DevExpress.ExpressApp.EF.Updating;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.Persistent.BaseImpl.EF.Kpi;
using DevExpress.Persistent.BaseImpl.EF.StateMachine;
using DevExpress.ExpressApp.Workflow.EF;
using DevExpress.ExpressApp.Workflow.Versioning;
using DevExpress.Workflow.EF;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace  Solution1.Module.BusinessObjects {
	public class Solution1DbContext : DbContext {
		public Solution1DbContext(String connectionString)
			: base(connectionString) {
		}
		public Solution1DbContext(DbConnection connection)
			: base(connection, false) {
		}
		public DbSet<ModuleInfo> ModulesInfo { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<TypePermissionObject> TypePermissionObjects { get; set; }

		public DbSet<Event> Events { get; set; }
		public DbSet<Resource> Resources { get; set; }
		public DbSet<FileData> FileData { get; set; }
		public DbSet<Analysis> Analysis { get; set; }
		public DbSet<HCategory> HCategories { get; set; }
        public DbSet<KpiDefinition> KpiDefinition { get; set; }
        public DbSet<KpiInstance> KpiInstance { get; set; }
        public DbSet<KpiHistoryItem> KpiHistoryItem { get; set; }
        public DbSet<KpiScorecard> KpiScorecard { get; set; }
		public DbSet<StateMachine> StateMachines { get; set; }
		public DbSet<StateMachineState> StateMachineStates { get; set; }
		public DbSet<StateMachineTransition> StateMachineTransitions { get; set; }
		public DbSet<StateMachineAppearance> StateMachineAppearances { get; set; }
		public DbSet<ReportDataV2> ReportDataV2 { get; set; }
		public DbSet<ModelDifference> ModelDifferences { get; set; }
		public DbSet<ModelDifferenceAspect> ModelDifferenceAspects { get; set; }
		public DbSet<EFWorkflowDefinition> EFWorkflowDefinition { get; set; }
        public DbSet<EFStartWorkflowRequest> EFStartWorkflowRequest { get; set; }
        public DbSet<EFRunningWorkflowInstanceInfo> EFRunningWorkflowInstanceInfo { get; set; }
        public DbSet<EFWorkflowInstanceControlCommandRequest> EFWorkflowInstanceControlCommandRequest { get; set; }
        public DbSet<EFInstanceKey> EFInstanceKey { get; set; }
        public DbSet<EFTrackingRecord> EFTrackingRecord { get; set; }
        public DbSet<EFWorkflowInstance> EFWorkflowInstance { get; set; }
        public DbSet<EFUserActivityVersion> EFUserActivityVersion { get; set; }


        public DbSet<Customer> Customers { get; set; }
        public DbSet<TheUser> Users { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}