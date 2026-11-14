using Dumpify;

class Program
{
  static void Main()  
  {
    var order1 = new Order(Currency.USD);
    order1.AddItem("Oranges",new Money(2.99m, Currency.USD));
    order1.AddItem("Apples", new Money(1.99m, Currency.USD));
    Console.WriteLine(order1.TotalPrice()); 
    // order1.AddItem("Books", new Money(10.99m, Currency.EUR));
  }
}


public class Order(Currency currency)
{
  private bool isPending = true;
  private decimal _totalPrice;
  private List<OrderItem> Items = new(); 
  private Currency DefaultCurrency = currency;

  public void CloseOrder()
  {
    isPending = false;
  }

  public void AddItem(string name, Money money)
  {
    if(this.isPending==false) throw new InvalidOperationException("The order isn't pending"); 
    if(DefaultCurrency!=money.Currency) throw new ArgumentException("Invalid Currency");

    Console.WriteLine(money.Amount);
    Items.Add(new OrderItem(name, money));
    _totalPrice += money.Amount;
  }
  public decimal TotalPrice() => _totalPrice;
}

public record OrderItem(string Name, Money money);

public enum Currency
{
  USD,
  EUR 
}
public readonly record struct Money
{
  public Currency Currency { get; } 
  public decimal Amount { get; }
  public Money(decimal Amount, Currency Currency)
  {
    if(Amount < 0) throw new ArgumentOutOfRangeException();
    this.Amount = Amount;
    this.Currency = Currency;
  }

}
