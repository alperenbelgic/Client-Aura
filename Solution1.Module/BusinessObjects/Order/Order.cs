using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using Solution1.Module.BusinessObjects.General;
using Solution1.Module.Helper;
using Solution1.Module.NonPersistentBusinessObjects;
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
    [XafDefaultProperty("OrderStatusUserFriendly")]
    [DefaultClassOptions]
    public class Order : IIntegrationItem, IBusinessObject, IXafEntityObject, IObjectSpaceLink, IHaveIsDeletedMember
    {
        [Browsable(false)]
        [Key]
        public int Id { get; set; }

        [Browsable(false)]
        public Guid Guid { get; set; }

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
        [Browsable(false)]
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

        public virtual OrderSurvey OrderSurvey { get; set; }


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

        [NotMapped]
        [NonPersistentDc]
        public string OrderStatusUserFriendly
        {
            get
            {
                return OrderStatesUserFriendlyContent.Values[this.OrderStatus];
            }
        }

        public void OnCreated()
        {
            this.OrderStatus = OrderStates.Draft;

            if (this.OrderItems == null)
            {
                this.OrderItems = new List<OrderItem>();
            }

            if (this.OrderSurvey == null)
            {
                this.OrderSurvey = this.ObjectSpace.CreateObject<OrderSurvey>();

                var defaultSurveyDefinition = UserHelper.GetUsersCompaniesDefaultSurvey(this.ObjectSpace);

                if (defaultSurveyDefinition != null)
                {
                    this.OrderSurvey.Survey = defaultSurveyDefinition;
                    this.OrderSurvey.SurveySendingDays = defaultSurveyDefinition.SurveySendingDays;
                }
            }

            if (this.Guid == default(Guid))
            {
                this.Guid = Guid.NewGuid();
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
                        else
                        {
                            if (string.IsNullOrWhiteSpace(this.Customer.Email))
                            {
                                exceptionalCases.Add(OrderProcessingResultTypes.CustomerHasNoEmailAddress);
                            }
                        }
                        if (this.OrderSurvey.Survey == null)
                        {
                            exceptionalCases.Add(OrderProcessingResultTypes.NoSurveySelected);
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
                case OrderEvents.StopProcessAndEdit:
                    if (this.OrderStatus == OrderStates.SurveySendingWaiting)
                    {
                        this.OrderStatus = OrderStates.Draft;
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
            NoCustomerSelected,
            NoSurveySelected,
            CustomerHasNoEmailAddress
        }

        public static List<Order> GetSurveySendingOrders()
        {
            var dbContext = SystemHelper.GetDbContext();

            var surveySendingOrders =
            dbContext.Orders.Where(
                order =>
            !order.IsDeleted &&
            order.OrderStatus == OrderStates.SurveySendingWaiting &&
            order.OrderDate.HasValue &&
            order.OrderDate.Value.AddDays(order.OrderSurvey.SurveySendingDays) > SystemHelper.GetSystemTime())
            .ToList();

            return surveySendingOrders;
        }

       

        public static Order GetOrderByGuid(Guid guid)
        {
            var dbContext = SystemHelper.GetDbContext();

            var order =
            dbContext.Orders.FirstOrDefault(o => o.Guid == guid);

            return order;
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

    public static class OrderStatesUserFriendlyContent
    {
        public static Dictionary<string, string> Values = new Dictionary<string, string>()
        {
            { OrderStates.Draft_NotSaved, "Draft" },
            { OrderStates.Draft, "Draft" },
            { OrderStates.SurveySendingWaiting, "Waiting Survey Sending Time" },
            { OrderStates.FeedBackWaiting, "Survey Sent, Feedback Waiting" },
            { OrderStates.FeedbackReached, "Feedback Reached" }
        };
    }



    public static class OrderEvents
    {
        public const string Saved = "Saved";
        public const string OrderCreated = "OrderCreated";
        public const string SurveySendingTimeOccured = "SurveySendingTimeOccured";
        public const string SurveyAnswered = "SurveyAnswered";
        public const string StopProcessAndEdit = "StopProcessAndEdit";
    }
}
