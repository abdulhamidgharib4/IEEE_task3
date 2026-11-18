
public class Address
{
    public string City { get; set; } = "";

    public Address() { }
    public Address(string city) { City = city; }
}

public class Customer
{
    public string Name { get; set; } = "";
    public Address Address { get; set; } = new Address();

    public Customer() { }

    public Customer(Customer other)
    {
        Name = other.Name;
        Address = new Address(other.Address.City); 
    }

    public Customer ShallowCopy()
    {
        return (Customer)MemberwiseClone();
    }

   
    public Customer DeepCopy()
    {
        return new Customer
        {
            Name = Name,
            Address = new Address(Address.City)
        };
    }
}

//Task 2 types
public class Animal
{
    public virtual string Speak() => "Animal sound";
    public string Type() => "Animal";
}

public class Dog : Animal
{
    public override string Speak() => "Woof";
    public new string Type() => "Dog";
}

public static class Printer
{
    public static void Print(object o) => Console.WriteLine("Print(object) was called");
    public static void Print(string s) => Console.WriteLine("Print(string) was called");
}

//Task 3 types
public interface IFlyable { string Move(); }
public interface ISwimmable { string Move(); }
public interface IWalkable { string Move(); }

public class Duck : IFlyable, ISwimmable, IWalkable
{
    string IFlyable.Move() => "The duck is flying";
    string ISwimmable.Move() => "The duck is swimming";
    string IWalkable.Move() => "The duck is walking";
}

//Task 4 types 
public record Money(decimal Amount, string Currency);

public class Money2
{
    public decimal Amount { get; }
    public string Currency { get; }

    public Money2(decimal amount, string currency)
    {
        Amount = amount;
        Currency = currency;
    }
}

public class Program
{
    public static void Main()
    {
        #region Task 1
        Console.WriteLine("Task 1: Shallow vs Deep Copy");

        var original = new Customer { Name = "Ahmed", Address = new Address("Cairo") };

        var shallow = original.ShallowCopy();
        var deep = original.DeepCopy();
        var deep2 = new Customer(original);

        original.Address.City = "Alexandria";

        Console.WriteLine($"Original: {original.Address.City}");
        Console.WriteLine($"Shallow : {shallow.Address.City}");
        Console.WriteLine($"Deep    : {deep.Address.City}");
        Console.WriteLine($"Deep (copy constructor): {deep2.Address.City}");
        Console.WriteLine();
        #endregion

        #region Task 2
        Console.WriteLine(" Task 2: Early vs Late Binding");

        Animal animal = new Dog();
        Console.WriteLine(animal.Speak());
        Console.WriteLine(animal.Type());

        object text = "hello";
        Printer.Print(text);
        Printer.Print((dynamic)text);
        Console.WriteLine();
        #endregion

        #region Task 3
        Console.WriteLine("Task 3: Interfaces");

        var duck = new Duck();

        IFlyable flyer = duck;
        ISwimmable swimmer = duck;
        IWalkable walker = duck;

        Console.WriteLine(flyer.Move());
        Console.WriteLine(swimmer.Move());
        Console.WriteLine(walker.Move());
        Console.WriteLine();
        #endregion

        #region Task 4
        Console.WriteLine("Task 4: Records");

        var m1 = new Money(100m, "USD");
        var m2 = new Money(100m, "USD");

        Console.WriteLine("record Money");
        Console.WriteLine($"m1 == m2               : {m1 == m2}");
        Console.WriteLine($"ReferenceEquals(m1, m2): {ReferenceEquals(m1, m2)}");
        Console.WriteLine($"HashCode m1            : {m1.GetHashCode()}");
        Console.WriteLine($"HashCode m2            : {m2.GetHashCode()}");
        Console.WriteLine($"ToString               : {m1}");

        var c1 = new Money2(100m, "USD");
        var c2 = new Money2(100m, "USD");

        Console.WriteLine("class Money2");
        Console.WriteLine($"c1 == c2               : {c1 == c2}");
        Console.WriteLine($"ReferenceEquals(c1, c2): {ReferenceEquals(c1, c2)}");
        Console.WriteLine($"HashCode c1            : {c1.GetHashCode()}");
        Console.WriteLine($"HashCode c2            : {c2.GetHashCode()}");
        Console.WriteLine($"ToString               : {c1}");

        var m3 = m1 with { Currency = "EUR" };
        Console.WriteLine($"with expression       : {m3}  (m1 is still {m1})");

        var set = new HashSet<Money> { m1, m2 };
        Console.WriteLine($"HashSet<Money> Count (m1, m2)    : {set.Count}");
        set.Add(m3);
        Console.WriteLine($"HashSet<Money> Count (after m3)  : {set.Count}");
        Console.WriteLine();
        #endregion
    }

   
}