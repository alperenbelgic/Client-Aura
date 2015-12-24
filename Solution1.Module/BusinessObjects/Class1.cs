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
    public class Class1
    {
        [Browsable(false)]
        [Key]
        public int Id { get; protected set; }

        public int MyProperty { get; set; }

        public string MyProperty2 { get; set; }
    }
}
