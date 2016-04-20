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
            //string name = "MyName";
            //EntityObject1 theObject = ObjectSpace.FindObject<EntityObject1>(CriteriaOperator.Parse("Name=?", name));
            //if(theObject == null) {
            //    theObject = ObjectSpace.CreateObject<EntityObject1>();
            //    theObject.Name = name;
            //}
            TheUser sampleUser = ObjectSpace.FindObject<TheUser>(new BinaryOperator("UserName", "User"));
            if (sampleUser == null)
            {
                sampleUser = ObjectSpace.CreateObject<TheUser>();
                sampleUser.UserName = "User";
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
    }
}
