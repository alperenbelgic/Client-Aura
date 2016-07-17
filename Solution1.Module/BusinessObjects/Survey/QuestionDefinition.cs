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
    public class QuestionDefinition : IBusinessObject
    {
        [Browsable(false)]
        [Key]
        public int Id { get; set; }

        public string QuestionText { get; set; }

        public virtual Parameter AnswerSelectionType { get; set; }

        public virtual MultipleChoiceOptionsDefinition MultipleChoiceOptionsDefinition { get; set; }
    }

    [DefaultClassOptions]
    public class ProductQuestionDefinition : IBusinessObject
    {

        [Browsable(false)]
        [Key]
        public int Id { get; set; }

        public string QuestionText { get; set; }

        public virtual Parameter AnswerSelectionType { get; set; }

        public virtual MultipleChoiceOptionsDefinition MultipleChoiceOptionsDefinition { get; set; }

        public virtual Product Product { get; set; }
    }
}
