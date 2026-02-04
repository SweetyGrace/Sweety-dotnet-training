namespace OrderApp.Services;

using Microsoft.VisualBasic;
using OrderApp.Models;
using OrderApp.Services;

public class OrderService
{
    public static List<Order> GetCompleteOrders(List<Order> orders)
    => orders.Where(orderItem => orderItem.Status == OrderStatus.Completed).ToList();


    public static string GetCompletedOrdersFormatted(List<Order> orders)
    => string.Join(", ", GetCompleteOrders(orders).Select(o => $"Order {o.Id}"));

    public static decimal GetTotalAmount(List<Order> orders)
   => orders.Sum(orderItem => orderItem.Amount);

   public static Order? GetFirstFailedOrder(List<Order> orders)
   => orders.FirstOrDefault(orderItem => orderItem.Status == OrderStatus.Failed);

   public static decimal CalculateDiscount(Order order)
    {
        decimal ? discountAmount = 0;
        if (order.IsVip)
        {
            discountAmount = discountAmount + 10;
        }
        if(order.Amount > 15000)
        {
            discountAmount = discountAmount + 5;
        }
        return order.Amount * ((discountAmount ?? 0) / 100);
    }
}