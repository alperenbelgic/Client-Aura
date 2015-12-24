using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Base.General;
using DevExpress.Persistent.BaseImpl.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution1.Module.BusinessObjects
{
    [DefaultProperty("FullName")]
    [DefaultClassOptions]
    public class TheUser
    : DevExpress.Persistent.BaseImpl.EF.User
    {
        public DateTime? Birthday
        {
            get;

            set;
        }

        public virtual Company Company { get; set; }

        [FieldSize(255)]
        public string Email
        {
            get;

            set;
        }

        public string FirstName
        {
            get;

            set;
        }

        private string fullName;

        [NotMapped]
        [SearchMemberOptions(SearchMemberMode.Exclude)]
        public string FullName
        {
            get
            {
                return fullName;
            }
        }

        public string LastName
        {
            get;

            set;
        }

        public string MiddleName
        {
            get;

            set;
        }

        public void SetFullName(string fullName)
        {
            this.fullName = fullName;
        }

        private string GetFullName() { return string.Format("{0} {1} {2}", this.FirstName, this.MiddleName, this.LastName); }
    }
}
