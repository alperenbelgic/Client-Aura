using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
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

    public class QuestionDefinition : IQuestion, IBusinessObject, IHaveIsDeletedMember
    {
        [Browsable(false)]
        [Key]
        public int Id { get; set; }

        [RuleRequiredField(CustomMessageTemplate = "Question Text should not be empty.", SkipNullOrEmptyValues = false, TargetContextIDs = "Save")]
        public string QuestionText { get; set; }

        [RuleRequiredField(CustomMessageTemplate = "Answer Type should not be empty.", SkipNullOrEmptyValues = false, TargetContextIDs = "Save")]
        public virtual Parameter AnswerType { get; set; }

        [RuleRequiredField(CustomMessageTemplate = "Multiple Choice Options Definition should not be empty.", SkipNullOrEmptyValues = false, TargetContextIDs = "Save")]
        public virtual MultipleChoiceOptionsDefinition MultipleChoiceOptionsDefinition { get; set; }

        [Browsable(false)]
        public bool IsDeleted { get; set; }
    }

    [DefaultClassOptions]
    public class ProductQuestionDefinition : IQuestion,  IBusinessObject, IXafEntityObject, IObjectSpaceLink, IHaveIsDeletedMember
    {

        [Browsable(false)]
        [Key]
        public int Id { get; set; }

        [RuleRequiredField(CustomMessageTemplate = "Question Text should not be empty.", SkipNullOrEmptyValues = false, TargetContextIDs = "Save")]
        public string QuestionText { get; set; }

        [RuleRequiredField(CustomMessageTemplate = "Answer Type should not be empty.", SkipNullOrEmptyValues = false, TargetContextIDs = "Save")]
        public virtual Parameter AnswerType { get; set; }

        [RuleRequiredField(CustomMessageTemplate = "Multiple Choice Options Definition should not be empty.", SkipNullOrEmptyValues = false, TargetContextIDs = "Save")]
        public virtual MultipleChoiceOptionsDefinition MultipleChoiceOptionsDefinition { get; set; }

        [Browsable(false)]
        public virtual List<Product> Products { get; set; }

        [Browsable(false)]
        public virtual Company Company { get; set; }

        [Browsable(false)]
        public bool IsDeleted { get; set; }

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


        public void OnCreated()
        {
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

    public interface IQuestion
    {
        int Id { get; set; }

        string QuestionText { get; set; }

        Parameter AnswerType { get; set; }

        MultipleChoiceOptionsDefinition MultipleChoiceOptionsDefinition { get; set; }

    }
}
