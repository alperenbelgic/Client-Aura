using Solution1.Module.BusinessObjects;
using Solution1.Module.NonPersistentBusinessObjects.SurveyRenderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Survey.UI
{
    public partial class SurveyPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            literalSurveyContent.Text = GetSurveyPageContent();
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

        private Order GetOrder(Guid id)
        {
            return Order.GetOrderByGuid(id);
        }

        private string GetSurveyPageContent()
        {
            var id = GetPassedGuid();
            if (id == null)

            {
                throw new NotImplementedException();
            }

            var order = GetOrder(id.Value);

            if (order == null)
            {
                throw new NotImplementedException();
            }

            var surveyRenderer = SurveyRenderFactory.CreateOrderSurveyRenderer(order);

            return surveyRenderer.Render();
        }
    }
}