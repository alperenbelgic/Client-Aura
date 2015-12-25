using DevExpress.ExpressApp;
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
    public class QuestionDefinition : IBusinessObject
    {
        [Browsable(false)]
        [Key]
        public int Id { get; set; }

        public string QuestionText { get; set; }

        public virtual Parameter AnswerType { get; set; }

        public virtual MultipleChoiceOptionsDefinition MultipleChoiceOptionsDefinition { get; set; }
    }

    [DefaultClassOptions]
    public class ProductQuestionDefinition : IBusinessObject
    {

        [Browsable(false)]
        [Key]
        public int Id { get; set; }

        public string QuestionText { get; set; }


        public virtual Parameter AnswerType { get; set; }

        public virtual MultipleChoiceOptionsDefinition MultipleChoiceOptionsDefinition { get; set; }

        [Browsable(false)]
        public virtual Company Company { get; set; }

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
