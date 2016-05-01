using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution1.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class Order : IIntegrationItem, IBusinessObject, IXafEntityObject
    {
        [Browsable(false)]
        [Key]
        public int Id { get; set; }

        public virtual Customer Customer { get; set; }

        public DateTime? OrderDate { get; set; }

        public virtual List<OrderItem> OrderItems { get; set; }

        public virtual Company Company { get; set; }

        public string IntegrationSource
        {
            get; set;
        }

        public string IntegrationCode
        {
            get; set;
        }

        public void OnCreated()
        {
            if (this.OrderItems == null)
            {
                this.OrderItems = new List<OrderItem>();
            }
        }

        public void OnSaving()
        {
        }

        public void OnLoaded()
        {
        }
    }
}
