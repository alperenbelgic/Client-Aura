using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using Solution1.Module.Helper;
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
    public class Order : IIntegrationItem, IBusinessObject, IXafEntityObject, IObjectSpaceLink
    {
        [Browsable(false)]
        [Key]
        public int Id { get; set; }

        public virtual Customer Customer { get; set; }

        public DateTime? OrderDate { get; set; }

        public virtual List<OrderItem> OrderItems { get; set; }

        [Browsable(false)]
        public virtual Company Company { get; set; }

        public string IntegrationSource
        {
            get; set;
        }

        public string IntegrationCode
        {
            get; set;
        }

        private string orderStatus = OrderStates.Draft_NotSaved;
        public string OrderStatus
        {
            get
            {
                return orderStatus;
            }

            set
            {
                orderStatus = value;
            }
        }

        [Browsable(false)]
        public bool IsDeleted { get; set; }

        public int SurveySendingDays { get; set; }


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
            this.OrderStatus = OrderStates.Draft;

            if (this.OrderItems == null)
            {
                this.OrderItems = new List<OrderItem>();
            }
        }

        public void OnSaving()
        {
            SendOrderEvent(OrderEvents.Saved);
        }

        public void OnLoaded()
        {
        }


        public Result<OrderProcessingResultTypes> SendOrderEvent(string _event)
        {
            var exceptionalCases = new List<OrderProcessingResultTypes>();
            switch (_event)
            {
                case OrderEvents.Saved:
                    if (this.OrderStatus == OrderStates.Draft_NotSaved)
                    {
                        this.OrderStatus = OrderStates.Draft;
                    }
                    break;
                case OrderEvents.OrderCreated:
                    if (this.OrderStatus == OrderStates.Draft)
                    {
                        if (this.OrderItems == null || this.OrderItems.Count == 0)
                        {
                            exceptionalCases.Add(OrderProcessingResultTypes.ThereIsNoOrderItem);
                        }
                        if (this.Customer == null)
                        {
                            exceptionalCases.Add(OrderProcessingResultTypes.NoCustomerSelected);
                        }

                        if (exceptionalCases.Count > 0)
                        {
                            return new Result<OrderProcessingResultTypes>()
                            {
                                Reasons = exceptionalCases,
                                Succeeded = false
                            };
                        }
                        else
                        {
                            this.OrderStatus = OrderStates.SurveySendingWaiting;
                        }
                    }
                    break;
                case OrderEvents.SurveySendingTimeOccured:
                    if (this.OrderStatus == OrderStates.SurveySendingWaiting)
                    {
                        this.OrderStatus = OrderStates.FeedBackWaiting;
                    }
                    break;
                case OrderEvents.SurveyAnswered:
                    if (this.OrderStatus == OrderStates.FeedBackWaiting)
                    {
                        this.OrderStatus = OrderStates.FeedbackReached;
                    }
                    break;

                default:
                    break;
            }

            return new Result<OrderProcessingResultTypes>() { Succeeded = true };
        }

        public enum OrderProcessingResultTypes
        {
            Succeeded,
            ThereIsNoOrderItem,
            NoCustomerSelected
        }

        public static void SendSurveys(XafApplication application)
        {
            var dbContext = SystemHelper.GetDbContext();

            var surveySendingOrders =
            dbContext.Orders.Where(o =>
            o.OrderDate.HasValue &&
            o.OrderDate.Value.AddDays(o.SurveySendingDays) < SystemHelper.GetSystemTime()).ToList();
        }
    }

    public static class OrderStates
    {
        public static string Draft_NotSaved = "Draft_NotSaved";
        public static string Draft = "Draft";
        public static string SurveySendingWaiting = "SurveySendingWaiting";
        public static string FeedBackWaiting = "FeedBackWaiting";
        public static string FeedbackReached = "FeedbackReached";
    }

    public static class OrderEvents
    {
        public const string Saved = "Saved";
        public const string OrderCreated = "OrderCreated";
        public const string SurveySendingTimeOccured = "SurveySendingTimeOccured";
        public const string SurveyAnswered = "SurveyAnswered";
    }
}
