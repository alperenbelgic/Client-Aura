using DevExpress.ExpressApp;
using Solution1.Module.BusinessObjects;
using Solution1.Module.Helper;
using Solution1.Module.NonPersistentBusinessObjects.SurveyRenderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Survey.UI
{
    public partial class SurveyPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Client Aura Survey";
            literalSurveyContent.Text = GetSurveyPageContent();
            orderGuid.Value = GetPassedGuid().ToString();
        }

        private Guid? GetPassedGuid()
        {
            bool idContains = this.Request.QueryString.AllKeys.Any(key => key == "id");
            if (!idContains)
            {
                return null;
            }
            string rawId = this.Request.QueryString["id"];
            Guid id;
            var parsed = Guid.TryParse(rawId, out id);

            if (parsed)
            {
                return id;
            }
            else
            {
                return null;
            }
        }

        private static Order GetOrder(Guid id, IObjectSpace objectSpace)
        {
            return Order.GetOrderByGuid(id, objectSpace);
        }

        private string GetSurveyPageContent()
        {
            var id = GetPassedGuid();
            if (id == null)

            {
                throw new NotImplementedException();
            }

            var order = GetOrder(id.Value, null);

            if (order == null)
            {
                throw new NotImplementedException();
            }

            var surveyRenderer = SurveyRenderFactory.CreateOrderSurveyRenderer(order);

            return surveyRenderer.Render();
        }

        [WebMethod]
        public static Dictionary<string, string> Submit(SubmitSurveyInput submitSurveyInput)
        {
            Guid id;
            var parsed = Guid.TryParse(submitSurveyInput.OrderGuid, out id);
            if (parsed)
            {
                var objectSpace = SystemHelper.GetObjectSpace();
                var order = Order.GetOrderByGuid(id, objectSpace);

                if (order != null)
                {
                    order.OrderSurvey.AnswerDate = DateTime.Now;
                    order.OrderSurvey.HasSent = true;

                    foreach (var question in submitSurveyInput.Questions)
                    {
                        if (question.QuestionClass == "order")
                        {
                            if (question.QuestionType == "multiple")
                            {
                                if (question.SelectedOptionNumber.HasValue)
                                {
                                    order.OrderSurvey.AddMultipleChoiceOrderAnswer(question.QuestionId, question.SelectedOptionNumber.Value);
                                }
                            }
                            else if (question.QuestionType == "text")
                            {
                                order.OrderSurvey.AddTextOrderAnswer(question.QuestionId, question.QuestionAnswerText);
                            }
                        }
                        else if (question.QuestionClass == "product")
                        {
                            if (question.QuestionType == "multiple")
                            {
                                if (question.SelectedOptionNumber.HasValue && question.ProductId.HasValue)
                                {
                                    order.OrderSurvey.AddMultipleChoiceProductAnswer(question.QuestionId, question.ProductId.Value, question.SelectedOptionNumber.Value);
                                }
                            }
                            else if (question.QuestionType == "text")
                            {
                                if (question.ProductId.HasValue)
                                {
                                    order.OrderSurvey.AddTextProductSurveyAnswer(question.QuestionId, question.ProductId.Value, question.QuestionAnswerText);
                                }
                            }
                        }
                    }

                    objectSpace.CommitChanges();
                }
            }

            return new Dictionary<string, string>() {
                {"Succeeded", "true" },
                { "Message", "message"}
            };
        }
    }

    [Serializable]
    public class SubmitSurveyInput
    {

        public string OrderGuid { get; set; }

        public List<Question> Questions { get; set; }

    }

    [Serializable]
    public class Question
    {

        public string HtmlId;
        public string QuestionType;//"multiple", "text" etc
        public string QuestionClass;// "order", "product"
        public int QuestionId;
        public int? SelectedOptionNumber;
        public string QuestionAnswerText;
        public int? ProductId;


    }
}