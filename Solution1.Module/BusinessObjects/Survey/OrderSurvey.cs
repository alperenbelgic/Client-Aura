using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution1.Module.BusinessObjects
{
    public class OrderSurvey : IBusinessObject
    {
        [Browsable(false)]
        [Key]
        public int Id { get; set; }
        
        public virtual SurveyDefinition Survey { get; set; }

        public int SurveySendingDays { get; set; }
    }
}
