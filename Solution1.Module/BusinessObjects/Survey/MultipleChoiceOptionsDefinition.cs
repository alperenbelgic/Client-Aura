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
    public class MultipleChoiceOptionsDefinition : IBusinessObject
    {
        [Browsable(false)]
        [Key]
        public int Id { get; set; }

        [NonPersistentDc]
        public string OptionsContent
        {
            get
            {
                return string.Format("{0}-{1}-{2}-{3}-{4}", Option1, Option2, Option3, Option4, Option5);
            }
        }

        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public string Option5 { get; set; }
    }
}
