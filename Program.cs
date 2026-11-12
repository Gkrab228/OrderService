using Dumpify;

class Program
{
  static void Main()  
  {
    var order1 = new Order(Currency.USD);
    order1.AddItem("Oranges",2.99m);
    Console.WriteLine(order1.GetCostofOrder()); 
  }
}


public class Order(Currency currency, bool isPending=true)
{
  private bool isPending = isPending;

  private List<OrderItem> Items = new(); 

  private Money money = new(currency);

  public void AddItem(string name, decimal value)
  {
    if(this.isPending==false) throw new ArgumentException("The order isn't pending"); 
    if(value < 0) throw new ArgumentOutOfRangeException("Value can't be negative");

    Items.Add(new OrderItem(value, name));
  }

  public decimal GetCostofOrder()
  {
    decimal sum=0;
    foreach(var order in Items)
    {
      sum = order.Value;
    }
    return sum; 
  }
}

public record OrderItem(decimal Value, string Name);


public enum Currency
{
  USD,
  EUR 
}
readonly record struct Money
{
  public Currency DefaultCurrency { get; } 

  public Money(Currency DefaultCurrency)
  {

    this.DefaultCurrency = DefaultCurrency;

  }
  readonly public Dictionary<Currency, decimal> ConversionRate = new()
  {
    {Currency.USD, 1},
    {Currency.EUR, 0.89m}
  };

}
