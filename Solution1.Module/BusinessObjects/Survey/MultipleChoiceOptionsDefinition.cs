using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using Solution1.Module.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution1.Module.BusinessObjects
{
    [DefaultClassOptions]
    [XafDefaultProperty("OptionsContent")]
    public class MultipleChoiceOptionsDefinition : IBusinessObject, IXafEntityObject, IObjectSpaceLink
    {
        [Browsable(false)]
        [Key]
        public int Id { get; set; }

        [NonPersistentDc]
        public string OptionsContent
        {
            get
            {
                if (Id == 0)
                {
                    return "";
                }
                return string.Format("{0}-{1}-{2}-{3}-{4}", Option1, Option2, Option3, Option4, Option5);
            }
        }

        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public string Option5 { get; set; }

        [Browsable(false)]
        public Company Company { get; set; }

        private IObjectSpace objectSpace = null;
        [NotMapped]
        [Browsable(false)]
        public IObjectSpace ObjectSpace
        {
            get
            {
                return objectSpace;
            }

            set
            {
                objectSpace = value;
            }
        }


        public void OnCreated()
        {
            if (this.Company == null)
            {
                this.Company = UserHelper.GetUsersCompany(this.ObjectSpace);
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
