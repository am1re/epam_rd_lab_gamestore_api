using Domain.Common;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Order : AuditableEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Comment { get; set; }

        public int PaymentTypeId { get; set; }
        public PaymentType PaymentType { get; set; }
        public int OrderStatusId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}