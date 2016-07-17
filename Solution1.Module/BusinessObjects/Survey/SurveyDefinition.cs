using DevExpress.ExpressApp;
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
    public class SurveyDefinition : IBusinessObject, IXafEntityObject
    {
        [Browsable(false)]
        [Key]
        public int Id { get; set; }

        public string SurveyName { get; set; }

        public bool IsDefault { get; set; }

        public bool AddProductQuestions { get; set; }

        public virtual Company Company { get; set; }

        public virtual List<QuestionDefinition> Questions { get; set; }

        public void OnCreated()
        {
            if (this.Questions == null)
            {
                this.Questions = new List<QuestionDefinition>();
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
