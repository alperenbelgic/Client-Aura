using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution1.Module.BusinessObjects
{
    public class OrderSurvey : IBusinessObject, INotifyPropertyChanged
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
        public virtual List<SurveyAnswer> SurverAnswers { get; set; }

        [Browsable(false)]
        public virtual List<ProductAnswer> ProductAnswers { get; set; }

        [Browsable(false)]
        public DateTime? AnswerDate { get; set; }

        [Browsable(false)]
        public bool HasSent { get; set; }


    }
}
