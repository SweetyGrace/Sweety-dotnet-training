namespace OrderApp.Models;

public class Order
{
    public int Id {get; set;}
    public decimal Amount {get; set;}
     
    public OrderStatus Status {get; set;}

    public bool IsVip {get; set;}
}