using Solution1.Module.BusinessObjects;
using Solution1.Module.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution1.Module.NonPersistentBusinessObjects.SurveyRenderers
{
    public class SurveyRenderFactory
    {
        public static ISurveyRenderer CreateOrderSurveyRenderer(Order order)
        {
            var surveyRenderer = new SurveyRenderer();
            surveyRenderer.Questions = new List<IQuestionRenderer>();

            foreach (var question in order.OrderSurvey.Survey.Questions)
            {
                IQuestionRenderer questionRenderer = new QuestionRenderer();
                questionRenderer.QuestionId = question.Id.ToString();
                questionRenderer.QuestionText = question.QuestionText;

                IAnswerRenderer answerRenderer = null;
                switch (question.AnswerType.ParameterKey)
                {
                    case ParameterHelper.AnswerTypes.MultipleChoice:
                        answerRenderer = new MultipleChoiceRenderer()
                        {
                            Option1 = question.MultipleChoiceOptionsDefinition.Option1,
                            Option2 = question.MultipleChoiceOptionsDefinition.Option2,
                            Option3 = question.MultipleChoiceOptionsDefinition.Option3,
                            Option4 = question.MultipleChoiceOptionsDefinition.Option4,
                            Option5 = question.MultipleChoiceOptionsDefinition.Option5,
                            QuestionId = question.Id.ToString(),
                            SelectedOptionNumber = GetMultipleChoiceResult(order, question)

                        };

                        break;
                    case ParameterHelper.AnswerTypes.Text:
                        answerRenderer = new MultiLineTextRenderer()
                        {
                            Value = GetTextAnswer(order, question)
                        };
                        break;
                    default:
                        break;
                }

                if (answerRenderer != null)
                {
                    questionRenderer.AnswerRenderer = answerRenderer;
                    surveyRenderer.Questions.Add(questionRenderer);
                }

            }

            if (order.OrderSurvey.Survey.AddProductQuestions)
            {
                foreach (var orderItem in order.OrderItems)
                {

                    foreach (var question in orderItem.Product.SurveyQuestions)
                    {
                        IQuestionRenderer questionRenderer = new QuestionRenderer();

                        questionRenderer.QuestionId = question.Id.ToString();
                        questionRenderer.QuestionText = question.QuestionText;

                        IAnswerRenderer answerRenderer = null;
                        switch (question.AnswerType.ParameterKey)
                        {
                            case ParameterHelper.AnswerTypes.MultipleChoice:
                                answerRenderer = new MultipleChoiceRenderer()
                                {
                                    Option1 = question.MultipleChoiceOptionsDefinition.Option1,
                                    Option2 = question.MultipleChoiceOptionsDefinition.Option2,
                                    Option3 = question.MultipleChoiceOptionsDefinition.Option3,
                                    Option4 = question.MultipleChoiceOptionsDefinition.Option4,
                                    Option5 = question.MultipleChoiceOptionsDefinition.Option5,
                                    QuestionId = question.Id.ToString(),
                                    SelectedOptionNumber = GetMultipleChoiceResult(order, question)
                                };

                                break;
                            case ParameterHelper.AnswerTypes.Text:
                                answerRenderer = new MultiLineTextRenderer()
                                {
                                    Value = GetTextAnswer(order, question)
                                };
                                break;
                            default:
                                break;
                        }

                        if (answerRenderer != null)
                        {
                            questionRenderer.AnswerRenderer = answerRenderer;
                            surveyRenderer.Questions.Add(questionRenderer);
                        }
                    } 
                }
            }

            return surveyRenderer;
        }

        private static int? GetMultipleChoiceResult(Order order, ProductQuestionDefinition question)
        {
            try
            {
                var productAnswer =
                order.OrderSurvey.ProductAnswers.FirstOrDefault(pa => pa.Question.Id == question.Id);

                if (productAnswer == null)
                {
                    return null;
                }
                else
                {
                    return productAnswer.MultipleChoiceResult;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static int? GetMultipleChoiceResult(Order order, QuestionDefinition question)
        {
            try
            {
                var answer =
                order.OrderSurvey.SurveyAnswers.FirstOrDefault(pa => pa.Question.Id == question.Id);

                if (answer == null)
                {
                    return null;
                }
                else
                {
                    return answer.MultipleChoiceResult;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static string GetTextAnswer(Order order, ProductQuestionDefinition question)

        {
            try
            {
                var productAnswer =
                order.OrderSurvey.ProductAnswers.FirstOrDefault(pa => pa.Question.Id == question.Id);

                if (productAnswer == null)
                {
                    return null;
                }
                else
                {
                    return productAnswer.AnswerAsText;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static string GetTextAnswer(Order order, QuestionDefinition question)
        {
            try
            {
                var answer =
                order.OrderSurvey.SurveyAnswers.FirstOrDefault(pa => pa.Question.Id == question.Id);

                if (answer == null)
                {
                    return null;
                }
                else
                {
                    return answer.AnswerAsText;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

    }


}
