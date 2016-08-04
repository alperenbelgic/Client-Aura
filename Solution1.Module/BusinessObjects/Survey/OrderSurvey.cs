using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution1.Module.BusinessObjects
{
    public class OrderSurvey : IBusinessObject, INotifyPropertyChanged, IXafEntityObject, IObjectSpaceLink
    {
        [Browsable(false)]
        [Key]
        public int Id { get; set; }

        private SurveyDefinition _Survey = null;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public virtual SurveyDefinition Survey
        {
            get
            {
                return this._Survey;
            }
            set
            {
                this._Survey = value;
                OnPropertyChanged("Survey");
            }
        }

        public int SurveySendingDays { get; set; }

        [Browsable(false)]
        public virtual List<SurveyAnswer> SurveyAnswers { get; set; }

        [Browsable(false)]
        public virtual List<ProductAnswer> ProductAnswers { get; set; }

        [Browsable(false)]
        public DateTime? AnswerDate { get; set; }

        [Browsable(false)]
        public bool HasSent { get; set; }

        public IObjectSpace ObjectSpace
        {
            get; set;
        }

        public void AddTextOrderAnswer(int questionId, string text)
        {
            // TODO: check if this order's survey definition contains this kind of question


            // check if the list null
            ConstructOrderAnswersIfNull();

            // chekc if the answer is already created
            SurveyAnswer surveyAnswer = null;
            Construct_If_Order_Answer_Is_Not_Created_Yet(questionId, out surveyAnswer);

            // assign the value
            surveyAnswer.AnswerAsText = text;
        }

        public void AddMultipleChoiceOrderAnswer(int questionId, int selectedOption)
        {
            // TODO: check if this order's survey definition contains this kind of question
            ConstructOrderAnswersIfNull();

            SurveyAnswer surveyAnswer = null;
            Construct_If_Order_Answer_Is_Not_Created_Yet(questionId, out surveyAnswer);

            surveyAnswer.MultipleChoiceResult = selectedOption;
        }

        public void AddTextProductSurveyAnswer(int productQuestionId, int productId, string text)
        {
            // TODO: check if this order's survey definition contains this kind of question

            ConstructProductAnswersIfNull();
            ProductAnswer productAnswer = null;
            Construct_If_Product_Answer_Is_Not_Created_Yet(productQuestionId, productId, out productAnswer);

            productAnswer.AnswerAsText = text;
        }

        public void AddMultipleChoiceProductAnswer(int productQuestionId, int productId, int selecedOption)
        {
            // TODO: check if this order's survey definition contains this kind of question

            ConstructProductAnswersIfNull();
            ProductAnswer productAnswer = null;
            Construct_If_Product_Answer_Is_Not_Created_Yet(productQuestionId, productId, out productAnswer);

            productAnswer.MultipleChoiceResult = selecedOption;
        }

        private void ConstructOrderAnswersIfNull()
        {
            if (SurveyAnswers == null)
            {
                SurveyAnswers = new List<SurveyAnswer>();
            }
        }
        private void ConstructProductAnswersIfNull()
        {
            if (ProductAnswers == null)
            {
                ProductAnswers = new List<ProductAnswer>();
            }
        }

        private void Construct_If_Order_Answer_Is_Not_Created_Yet(int surveyQuestionId, out SurveyAnswer answer)
        {
            answer = this.SurveyAnswers.FirstOrDefault(sa => sa.Question.Id == surveyQuestionId);
            if (null == answer)
            {
                var question = this.ObjectSpace.FindObject(typeof(QuestionDefinition), new BinaryOperator("Id", surveyQuestionId)) as QuestionDefinition;

                answer = this.ObjectSpace.CreateObject<SurveyAnswer>();
                answer.Question = question;

                this.SurveyAnswers.Add(answer);
            }
        }
        private void Construct_If_Product_Answer_Is_Not_Created_Yet(int productQuestionId, int productId, out ProductAnswer productAnswer)
        {
            productAnswer = this.ProductAnswers.FirstOrDefault(pa => pa.Question.Id == productQuestionId && pa.Product.Id == productId);
            if (null == productAnswer)
            {
                var product = this.ObjectSpace.FindObject(typeof(Product), new BinaryOperator("Id", productId)) as Product;

                var question = this.ObjectSpace.FindObject(typeof(ProductQuestionDefinition), new BinaryOperator("Id", productQuestionId)) as ProductQuestionDefinition;

                productAnswer = this.ObjectSpace.CreateObject<ProductAnswer>();
                productAnswer.Product = product;
                productAnswer.Question = question;

                this.ProductAnswers.Add(productAnswer);
            }
        }

        public void OnCreated()
        {
        }

        public void OnSaving()
        {
        }

        public void OnLoaded()
        {
        }
    }
}
