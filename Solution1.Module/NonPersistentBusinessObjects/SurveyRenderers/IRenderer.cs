using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution1.Module.NonPersistentBusinessObjects.SurveyRenderers
{
    public interface IRenderer
    {
        string Render();
    }

    public class SurveyRenderer : ISurveyRenderer
    {
        public IList<IQuestionRenderer> Questions { get; set; }

        public string Render()
        {
            var stringBuilder = new StringBuilder();
            foreach (var question in Questions)
            {
                stringBuilder.Append(question.Render());
            }
            return SurveyTemplate.Replace("[content]", stringBuilder.ToString());
        }

        private const string SurveyTemplate =
            @"
<div class=""survey"" >[content]</div>
";
    }

    public interface ISurveyRenderer : IRenderer
    {
        IList<IQuestionRenderer> Questions { get; set; }
    }

    public class QuestionRenderer : IQuestionRenderer
    {
        public IAnswerRenderer AnswerRenderer { get; set; }

        public string QuestionText { get; set; }

        public string QuestionId { get; set; }

        public string Render()
        {
            var answerContent = new StringBuilder(this.AnswerRenderer.Render());
            var template = new StringBuilder(Template);

            template = template.Replace("[questionId]", this.QuestionId);
            template = template.Replace("[questionText]", this.QuestionText);
            template = template.Replace("[answerContainer]", answerContent.ToString());

            return template.ToString();
        }

        private const string Template =
@"
<div class=""questionContainer""  name=""[questionId]"" id=""[questionId]"" >
    <div class=""questionLabel"" >[questionText]</div>
    <div class=""answerContainer"">[answerContainer]</div>
</div>
";
    }

    public interface IQuestionRenderer : IRenderer
    {
        string QuestionText { get; set; }

        string QuestionId { get; set; }

        IAnswerRenderer AnswerRenderer { get; set; }
    }

    public interface IAnswerRenderer : IRenderer
    {

    }

    public class MultipleChoiceRenderer : IAnswerRenderer
    {
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public string Option5 { get; set; }

        public int? SelectedOptionNumber { get; set; }

        public string QuestionId { get; set; }


        public string Render()
        {
            var optionsContent = new StringBuilder();
            var options = new List<string> { Option1, Option2, Option3, Option4, Option5 };
            for (int i = 0; i < options.Count; i++)
            {
                int optionNumber = i + 1;
                string option = options[i];

                string content =
                     OptionTemplate
                     .Replace("[questionId]", this.QuestionId)
                     .Replace("[optionText]", option)
                     .Replace("[value]", this.QuestionId);

                if (SelectedOptionNumber == optionNumber)
                {
                    content = content.Replace("[content]", @" checked=""checked"" ");
                }
                else
                {
                    content = content.Replace("[content]", "");
                }

                optionsContent.Append(content);
            }

            return WrapperTemplate.Replace("[content]", optionsContent.ToString());
        }

        public const string OptionTemplate =
@"
<div class=""multipleChoiceOption"" >
    <input type=""radio"" name=""[questionId]"" value=""[value]"" [checked] />
    <div class=""optionText"" >[optionText]</div>
</div>
";

        public const string WrapperTemplate =
@"
<div class=""answerWrapper"" >[content] </div>
";
    }

    public class MultiLineTextRenderer : IAnswerRenderer
    {
        public int Rows = 5;
        public int Columns = 80;
        public string Value { get; set; }

        public string Render()
        {
            return MultiLineTextTemplate
                .Replace("[rows]", this.Rows.ToString())
                .Replace("[cols]", this.Columns.ToString())
                .Replace("[value]", this.Value)
                ;
        }



        public const string MultiLineTextTemplate = @"
<textarea rows=""[rows]"" cols=""[cols]]"">
[value]
</textarea>
";
    }
}
