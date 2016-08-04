using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution1.Module.BusinessObjects
{
    public class SurveyAnswer
    {
        public int Id { get; set; }

        public virtual QuestionDefinition Question { get; set; }

        public int MultipleChoiceResult { get; set; }

        public string AnswerAsText { get; set; }
    }
}
