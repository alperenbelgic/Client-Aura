using System;
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

            var companies = CreateCompaniesProductsCustomers();

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


    }
}
