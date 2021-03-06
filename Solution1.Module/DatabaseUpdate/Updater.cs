﻿using System;
using System.Linq;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.EF;
using DevExpress.Persistent.BaseImpl.EF;
using Solution1.Module.BusinessObjects;
using System.Collections.Generic;

namespace Solution1.Module.DatabaseUpdate
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppUpdatingModuleUpdatertopic.aspx
    public class Updater : ModuleUpdater
    {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
            base(objectSpace, currentDBVersion)
        {
        }
        public override void UpdateDatabaseAfterUpdateSchema()
        {
            base.UpdateDatabaseAfterUpdateSchema();

            CreateParameters();

          

            var companies = CreateCompaniesProductsCustomers();

            CreateMultipleChoiceOptionsDefinition(companies[0]);
            CreateSurveyDefinition(companies[0]);

            TheUser sampleUser = ObjectSpace.FindObject<TheUser>(new BinaryOperator("UserName", "User"));
            if (sampleUser == null)
            {
                sampleUser = ObjectSpace.CreateObject<TheUser>();
                sampleUser.UserName = "User";
                sampleUser.Company = companies[0];
                sampleUser.SetPassword("p0");
            }
            Role defaultRole = CreateDefaultRole();
            sampleUser.Roles.Add(defaultRole);

            TheUser userAdmin = ObjectSpace.FindObject<TheUser>(new BinaryOperator("UserName", "Admin"));
            if (userAdmin == null)
            {
                userAdmin = ObjectSpace.CreateObject<TheUser>();
                userAdmin.UserName = "Admin";
                // Set a password if the standard authentication type is used
                userAdmin.SetPassword("p0");
            }
            // If a role with the Administrators name doesn't exist in the database, create this role
            Role adminRole = ObjectSpace.FindObject<Role>(new BinaryOperator("Name", "Administrators"));
            if (adminRole == null)
            {
                adminRole = ObjectSpace.CreateObject<Role>();
                adminRole.Name = "Administrators";
            }
            adminRole.IsAdministrative = true;

            userAdmin.Roles.Add(adminRole);

            ObjectSpace.CommitChanges(); //This line persists created object(s).

            // sil burayi sonra
            int i = 33;
            int b = i * i;
        }

        private void CreateParameters()
        {
            // Answer Types
            var multipleChoice = ObjectSpace.FindObject<Parameter>(
                CriteriaOperator.And(new BinaryOperator("ParameterGroupKey", "AnswerType"), new BinaryOperator("ParameterKey", "MultipleChoice")));

            if (multipleChoice == null)
            {
                multipleChoice = ObjectSpace.CreateObject<Parameter>();
                multipleChoice.ParameterGroupKey = "AnswerType";
                multipleChoice.ParameterKey = "MultipleChoice";
                multipleChoice.ParameterName = "Multiple Choice";
            }

            var textQuestionType = ObjectSpace.FindObject<Parameter>(
                CriteriaOperator.And(new BinaryOperator("ParameterGroupKey", "AnswerType"), new BinaryOperator("ParameterKey", "Text")));
            if (textQuestionType == null)
            {
                textQuestionType = ObjectSpace.CreateObject<Parameter>();
                textQuestionType.ParameterGroupKey = "AnswerType";
                textQuestionType.ParameterKey = "Text";
                textQuestionType.ParameterName = "Text";
            }
        }

        public override void UpdateDatabaseBeforeUpdateSchema()
        {
            base.UpdateDatabaseBeforeUpdateSchema();
        }
        private Role CreateDefaultRole()
        {
            Role defaultRole = ObjectSpace.FindObject<Role>(new BinaryOperator("Name", "Default"));
            if (defaultRole == null)
            {
                defaultRole = ObjectSpace.CreateObject<Role>();
                defaultRole.Name = "Default";

                SetDefaultRolePermissions(defaultRole);


            }
            return defaultRole;
        }
        private TypePermissionObject CreateUserPermissions()
        {
            TypePermissionObject userPermissions = CreateTypePermission<TheUser>(false, false);
            userPermissions.ObjectPermissions.Add(CreateUserObjectPermission());
            userPermissions.MemberPermissions.Add(CreateUserMemberPermission("StoredPassword", "[ID] = CurrentUserId()"));
            userPermissions.MemberPermissions.Add(CreateUserMemberPermission("ChangePasswordOnFirstLogon", "[ID] = CurrentUserId()"));
            return userPermissions;
        }
        private TypePermissionObject CreateTypePermission<T>(bool allowRead, bool allowWrite)
        {
            TypePermissionObject typePermissions = ObjectSpace.CreateObject<TypePermissionObject>();
            typePermissions.TargetType = typeof(T);
            typePermissions.AllowWrite = allowWrite;
            typePermissions.AllowRead = allowRead;
            return typePermissions;
        }
        private SecuritySystemMemberPermissionsObject CreateUserMemberPermission(string member, string criteria)
        {
            SecuritySystemMemberPermissionsObject memberPermission = ObjectSpace.CreateObject<SecuritySystemMemberPermissionsObject>();
            memberPermission.Members = member;
            memberPermission.AllowWrite = true;
            memberPermission.Criteria = criteria;
            return memberPermission;
        }
        private SecuritySystemObjectPermissionsObject CreateUserObjectPermission()
        {
            SecuritySystemObjectPermissionsObject objectPermission = ObjectSpace.CreateObject<SecuritySystemObjectPermissionsObject>();
            objectPermission.Criteria = "[ID] = CurrentUserId()";
            objectPermission.AllowRead = true;
            objectPermission.AllowNavigate = false;
            return objectPermission;
        }

        //private void SetUserAndRoleTypePermissions(Role defaultRole)
        //{
        //    defaultRole.SetTypePermissions<Role>(SecurityOperations.FullAccess, DevExpress.ExpressApp.Security.Strategy.SecuritySystemModifier.Deny);
        //    defaultRole.SetTypePermissions<TheUser>(SecurityOperations.FullAccess, DevExpress.ExpressApp.Security.Strategy.SecuritySystemModifier.Deny);
        //}

        private void SetDefaultRolePermissions(Role defaultRole)
        {
            defaultRole.SetTypePermissionsRecursively<object>(SecurityOperations.FullAccess, DevExpress.ExpressApp.Security.Strategy.SecuritySystemModifier.Deny);

            defaultRole.SetTypePermissionsRecursively<IBusinessObject>(SecurityOperations.FullAccess, DevExpress.ExpressApp.Security.Strategy.SecuritySystemModifier.Allow);

            defaultRole.SetTypePermissionsRecursively<Parameter>(SecurityOperations.Read, DevExpress.ExpressApp.Security.Strategy.SecuritySystemModifier.Allow);

            defaultRole.TypePermissions.Add(CreateUserPermissions());
            defaultRole.TypePermissions.Add(CreateTypePermission<Role>(false, false));
        }

        private List<Company> CreateCompaniesProductsCustomers()
        {
            var kliksaCompany = ObjectSpace.FindObject<Company>(new BinaryOperator("CompanyName", "Kliksa"));
            if (kliksaCompany == null)
            {
                kliksaCompany = ObjectSpace.CreateObject<Company>();

                kliksaCompany.CompanyName = "Kliksa";
                kliksaCompany.IsCompanyActive = true;
                ObjectSpace.CommitChanges();

                var sonyTvProduct = ObjectSpace.CreateObject<Product>();
                sonyTvProduct.ProductName = "Sony Bravia 40' TV";
                sonyTvProduct.Company = kliksaCompany;
                sonyTvProduct.IntegrationSource = "User Interface";

                var bekoRefrigiratorProduct = ObjectSpace.CreateObject<Product>();
                bekoRefrigiratorProduct.ProductName = "Beko Nofrost Refrigerator";
                bekoRefrigiratorProduct.Company = kliksaCompany;
                bekoRefrigiratorProduct.IntegrationSource = "User Interface";


                var kliksaCustomerAlperenBelgic = ObjectSpace.CreateObject<Customer>();
                kliksaCustomerAlperenBelgic.CustomerName = "Alperen Belgic";
                kliksaCustomerAlperenBelgic.Company = kliksaCompany;
                kliksaCustomerAlperenBelgic.Email = "alperenbelgic@outlook.com";
                kliksaCustomerAlperenBelgic.IntegrationSource = "User Interface";

                var kliksaCustomerAyferBelgic = ObjectSpace.CreateObject<Customer>();
                kliksaCustomerAyferBelgic.CustomerName = "Ayfer Belgic";
                kliksaCustomerAyferBelgic.Company = kliksaCompany;
                kliksaCustomerAyferBelgic.Email = "ayferbelgic@hotmail.com";
                kliksaCustomerAyferBelgic.IntegrationSource = "User Interface";

                ObjectSpace.CommitChanges();
            }

            var leraFrescaCompany = ObjectSpace.FindObject<Company>(new BinaryOperator("CompanyName", "Lera Fresca"));
            if (leraFrescaCompany == null)
            {
                leraFrescaCompany = ObjectSpace.CreateObject<Company>();

                leraFrescaCompany.CompanyName = "Lera Fresca";
                leraFrescaCompany.IsCompanyActive = true;

                ObjectSpace.CommitChanges();

                var iceCreamProduct = ObjectSpace.CreateObject<Product>();
                iceCreamProduct.ProductName = "Lera Fresca Ice Cream ";
                iceCreamProduct.Company = leraFrescaCompany;
                iceCreamProduct.IntegrationSource = "User Interface";

                var milkBottledProduct = ObjectSpace.CreateObject<Product>();
                milkBottledProduct.ProductName = "Milk Bottled 1lt";
                milkBottledProduct.Company = leraFrescaCompany;
                milkBottledProduct.IntegrationSource = "User Interface";


                var leraFrescaCustomerPelinMezrea = ObjectSpace.CreateObject<Customer>();
                leraFrescaCustomerPelinMezrea.CustomerName = "Pelin Elif Mezrea Belgic";
                leraFrescaCustomerPelinMezrea.Company = leraFrescaCompany;
                leraFrescaCustomerPelinMezrea.Email = "pelinelifmezrea@gmail.com";
                leraFrescaCustomerPelinMezrea.IntegrationSource = "User Interface";

                var leraFrescaCustomerBarisBugraBelgic = ObjectSpace.CreateObject<Customer>();
                leraFrescaCustomerBarisBugraBelgic.CustomerName = "Baris Bugra Belgic";
                leraFrescaCustomerBarisBugraBelgic.Company = leraFrescaCompany;
                leraFrescaCustomerBarisBugraBelgic.Email = "barisbugra@yahoo.com";
                leraFrescaCustomerBarisBugraBelgic.IntegrationSource = "User Interface";

                ObjectSpace.CommitChanges();
            }


            return new List<Company>() {
                kliksaCompany,
                leraFrescaCompany
            };
        }

        private void CreateMultipleChoiceOptionsDefinition(Company company)
        {
            var mcod1 = this.ObjectSpace.FindObject<MultipleChoiceOptionsDefinition>(CriteriaOperator.And(
                new BinaryOperator("Option1", "1"),
                new BinaryOperator("Company.Id", company.Id)
                ));
            if (mcod1 == null)
            {
                mcod1 = this.ObjectSpace.CreateObject<MultipleChoiceOptionsDefinition>();
                mcod1.Option1 = "1";
                mcod1.Option2 = "2";
                mcod1.Option3 = "3";
                mcod1.Option4 = "4";
                mcod1.Option5 = "5";
                mcod1.Company = company;
            }
        }

        public void CreateSurveyDefinition(Company company)
        {
            var surveyDefintion = this.ObjectSpace.FindObject<SurveyDefinition>(CriteriaOperator.And(
                new BinaryOperator("Company.Id", company.Id),
                new BinaryOperator("SurveyName", "Default")));
            if (surveyDefintion == null)
            {
                surveyDefintion = this.ObjectSpace.CreateObject<SurveyDefinition>();
                surveyDefintion.Company = company;
                surveyDefintion.IsDefault = true;
                surveyDefintion.SurveyName = "Default";
                surveyDefintion.AddProductQuestions = true;
                surveyDefintion.Questions = new List<QuestionDefinition>();

                var question1 = this.ObjectSpace.CreateObject<QuestionDefinition>();
                question1.QuestionText = "How likely are you to recommend [CompanyNameHere] to a friend or colleague?";
                question1.AnswerType = this.ObjectSpace.FindObject<Parameter>(new BinaryOperator("ParameterKey", "MultipleChoice"));
                question1.MultipleChoiceOptionsDefinition = this.ObjectSpace.FindObject<MultipleChoiceOptionsDefinition>(new BinaryOperator("Option1", "1"));

                surveyDefintion.Questions.Add(question1);
            }
        }


    }
}
