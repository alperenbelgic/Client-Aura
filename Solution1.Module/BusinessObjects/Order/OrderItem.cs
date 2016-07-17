using DevExpress.ExpressApp.DC;
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
    [XafDefaultProperty("Product.ProductName")]
    public class OrderItem : IIntegrationItem, IBusinessObject
    {
        [Browsable(false)]
        [Key]
        public int Id { get; set; }

        public virtual Product Product { get; set; }

        public int Count { get; set; }

        private string integrationSource;
        public string IntegrationSource
        {
            get
            {
                return integrationSource;
            }

            set
            {
                integrationSource = value;
            }
        }


        private string integrationCode;
        public string IntegrationCode
        {
            get
            {
                return integrationCode;
            }

            set
            {
                integrationCode = value;
            }
        }

    }
}
