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
    public class Customer : IIntegrationItem, IBusinessObject
    {
        [Browsable(false)]
        [Key]
        public int Id { get; set; }

        public string CustomerName { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string TelephoneNumber { get; set; }

        public string IntegrationSource
        {
            get; set;
        }

        public string IntegrationCode
        {
            get; set;
        }
        
    }
}
