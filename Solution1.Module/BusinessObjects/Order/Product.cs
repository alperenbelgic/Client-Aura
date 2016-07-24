using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using Solution1.Module.BusinessObjects.General;
using Solution1.Module.Helper;
using Solution1.Module.NonPersistentBusinessObjects.SurveyRenderers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution1.Module.BusinessObjects
{

    [DefaultClassOptions]
    [XafDefaultProperty("ProductName")]
    public class Product : IIntegrationItem, IBusinessObject, IXafEntityObject, IObjectSpaceLink, IHaveIsDeletedMember
    {
        [Browsable(false)]
        [Key]
        public int Id { get; set; }

        private string integrationCode;
        private string integrationSource;


        public string ProductName { get; set; }

        [Browsable(false)]
        public virtual Company Company { get; set; }

        public virtual List<ProductQuestionDefinition> SurveyQuestions { get; set; }

        public string IntegrationCode
        {
            get
            {
                return integrationCode;
            }

            set
            {
                integrationCode = value;
            }
        }

        public string IntegrationSource
        {
            get
            {
                return integrationSource;
            }

            set
            {
                integrationSource = value;
            }
        }

        private IObjectSpace objectSpace = null;
        [NotMapped]
        [Browsable(false)]
        public IObjectSpace ObjectSpace
        {
            get
            {
                return objectSpace;
            }

            set
            {
                objectSpace = value;
            }
        }

        [Browsable(false)]
        public bool IsDeleted { get; set; }

        public void OnCreated()
        {
            if (this.SurveyQuestions == null)
            {
                this.SurveyQuestions = new List<ProductQuestionDefinition>();
            }

            if (this.Company == null)
            {
                this.Company = UserHelper.GetUsersCompany(this.ObjectSpace);
            }
        }

        public void OnSaving()
        {
        }

        public void OnLoaded()
        {
        }
    }
}
