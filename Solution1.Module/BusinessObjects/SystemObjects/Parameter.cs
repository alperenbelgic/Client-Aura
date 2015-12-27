using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution1.Module.BusinessObjects.SystemObjects
{
    public class Parameter
    {
        [Browsable(false)]
        [Key]
        public int Id { get; set; }

        public string ParameterGroup { get; set; }

        public string ParameterName { get; set; }

        public string StringValue { get; set; }

        public int IntValue { get; set; }

        public bool BoolValue { get; set; }

        public decimal DecimalValue { get; set; }
    }
}
