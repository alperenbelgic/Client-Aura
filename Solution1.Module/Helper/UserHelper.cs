using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.EF;
using Solution1.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution1.Module.Helper
{
    public class UserHelper
    {
        public static TheUser GetCurrentUser()
        {
            return SecuritySystem.CurrentUser as TheUser;
        }

        public static Company GetUsersCompany(IObjectSpace objectSpace)
        {
            var user = GetCurrentUser();

            if (user == null)
            {
                return null;
            }

            try
            {
                int companyId = user.Company.Id;

                var company = objectSpace.GetObjectByKey<Company>(companyId);

                return company;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static SurveyDefinition GetUsersCompaniesDefaultSurvey(IObjectSpace objectSpace)
        {
            var usersCompany = GetUsersCompany(objectSpace);
            var surveyDefinition = objectSpace.FindObject<SurveyDefinition>(
                CriteriaOperator.And(
                    new BinaryOperator("Company.Id", usersCompany.Id),
                    new BinaryOperator("IsDefault", true)
                    ));

            if (surveyDefinition == null)
            {
                surveyDefinition = objectSpace.FindObject<SurveyDefinition>(
                CriteriaOperator.And(
                    new BinaryOperator("Company.Id", usersCompany.Id)
                    ));
            }

            return surveyDefinition;
        }

    }
}
