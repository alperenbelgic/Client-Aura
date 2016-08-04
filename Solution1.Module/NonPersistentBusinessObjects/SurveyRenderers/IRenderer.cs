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
        public string SurveyTitle { get; set; }
        public string Logo { get; set; }

        private string RenderQuestions()
        {
            var stringBuilder = new StringBuilder();
            foreach (var question in Questions)
            {
                stringBuilder.Append(question.Render());
            }
            return stringBuilder.ToString();
        }

        public string Render()
        {
            var surveyContent = new StringBuilder(SurveyTemplate);

            string questionContent = RenderQuestions();
            surveyContent = surveyContent.Replace("[content]", questionContent);

            surveyContent = surveyContent.Replace("[surveyTitle]", this.SurveyTitle);

            surveyContent = surveyContent.Replace("[imageContent]", this.Logo);

            return surveyContent.ToString();
        }

        private const string SurveyTemplate =
            @"

<div class=""survey"" >
    <div class=""surveyImageWrapper""><div class=""surveyImage""> <img src=""/assets/images/your-logo.png""/></div></div>
    <div class=""surveyTitleWrapper""><div class=""surveyTitle"">[surveyTitle]</div></div>
    [content]
</div>
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
<div class=""questionContainer""   id=""[questionId]"" >
    <div class=""questionLabel"" >[questionText]</div>
    <div class=""answerContainer"">[answerContainer]</div>
    <div style=""clear:both;""></div>
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
            options.Reverse();
            for (int i = 0; i < options.Count; i++)
            {
                int optionNumber = 5 - i;
                string option = options[i];

                string content =
                     OptionTemplate
                     .Replace("[questionId]", this.QuestionId)
                     .Replace("[optionText]", option)
                     .Replace("[value]", this.QuestionId)
                     .Replace("[optionNumber]", optionNumber.ToString());

                if (SelectedOptionNumber == optionNumber)
                {
                    content = content.Replace("[checked]", @" checked=""checked"" ");
                }
                else
                {
                    content = content.Replace("[checked]", "");
                }

                optionsContent.Append(content);
            }

            return WrapperTemplate.Replace("[content]", optionsContent.ToString());
        }

        public const string OptionTemplate =
@"

<ul>
  <li>
    <input type=""radio"" name=""[questionId]"" value=""[value]"" id=""[questionId]_[optionNumber]"" [checked] />
    <label for=""[questionId]_[optionNumber]"">[optionText]</label>
    <div class=""check""></div>
  </li>
</ul>

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
<textarea rows=""[rows]"" class=""ca-text-area form-control"" >[value]</textarea>
";
    }
}
