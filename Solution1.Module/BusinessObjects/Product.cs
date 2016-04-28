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
    [XafDefaultProperty("ProductName")]
    public class Product : IIntegrationItem, IBusinessObject
    {
        [Browsable(false)]
        [Key]
        public int Id { get; set; }

        private string integrationCode;
        private string integrationSource;


        public string ProductName { get; set; }

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
    }
}
