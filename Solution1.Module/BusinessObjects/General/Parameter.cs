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

    public class Parameter 
    {
        [Browsable(false)]
        [Key]
        public int Id { get; set; }

        public string ParameterGroupKey { get; set; }

        public string ParameterKey { get; set; }

        public string ParameterName { get; set; }

    }
}
