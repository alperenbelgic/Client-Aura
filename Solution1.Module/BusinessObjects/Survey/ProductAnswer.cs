using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution1.Module.BusinessObjects
{
    public class ProductAnswer
    {
        public int Id { get; set; }

        public virtual ProductQuestionDefinition Question { get; set; }

        public virtual Product Product { get; set; }

        public int MultipleChoiceResult { get; set; }

        public string  AnswerAsText { get; set; }
    }
}
