using Dumpify;

class Program
{
  static void Main()  
  {
    var order1 = new Order();
    order1.AddItem("Oranges",2.99m);
    
  }
}


public class Order(bool isPending=true)
{
  private bool isPending = isPending;

  private List<OrderItem> Items = new(); 

  private Money money = new Money(); 


  public void AddItem(string name, decimal value)
  {
    if(this.isPending==false) throw new ArgumentException("The order isn't pending"); 
    if(value < 0) throw new ArgumentOutOfRangeException("Value can't be negative");

    Items.Add(new OrderItem(value, name));
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
