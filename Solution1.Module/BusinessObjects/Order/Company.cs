using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution1.Module.BusinessObjects
{
    [DefaultProperty("FullName")]
    [DefaultClassOptions]

    public class Company : IBusinessObject
    {

        [Key]
        [Browsable(false)]
        public int Id { get; set; }

        public string  CompanyName { get; set; }

        public string PhoneNumber { get; set; }

        public string WebSite { get; set; }

        public string  Address { get; set; }

        public string Email { get; set; }

        public bool IsCompanyActive { get; set; }

        public int SurveySendingDays { get; set; }

    }
}
