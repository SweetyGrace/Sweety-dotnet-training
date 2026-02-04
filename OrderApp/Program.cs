
using Microsoft.VisualBasic;
using OrderApp.Models;
namespace OrderApp.Services;


public class Program
{
    static void Main(string[] args)
    {
        var orders = new List<Order>
        {
            new Order {Id = 1, Amount = 12000, Status= OrderStatus.Completed, IsVip = true},
            new Order {Id = 2, Amount = 8000,  Status= OrderStatus.Pending, IsVip = false},
            new Order {Id = 3, Amount = 20000, Status= OrderStatus.Failed, IsVip = true},
            new Order {Id = 4, Amount = 15000, Status= OrderStatus.Completed, IsVip = false},
        };
        var completedOrders = OrderService.GetCompleteOrders(orders);
        var totalAmount =    OrderService.GetTotalAmount(orders);
        var firstFailedOrder = OrderService.GetFirstFailedOrder(orders);

        Console.WriteLine("Completed Orders: " + string.Join(", ", completedOrders.Select(o => $"Order {o.Id}")));
        Console.WriteLine("Total Amount: " + totalAmount);
        Console.WriteLine("First Failed Order: " + (firstFailedOrder != null ? $"Order {firstFailedOrder.Id}" : "None"));
        foreach (var order in orders)
        {
            var discount = OrderService.CalculateDiscount(order);
            Console.WriteLine($"Order ID: {order.Id}, Discount: {discount}");
        }

    }
}