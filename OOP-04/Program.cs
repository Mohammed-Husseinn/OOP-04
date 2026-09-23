#region Theoretical Questions

//Q1: a) What is Abstraction in Object-Oriented Programming?
//Abstraction means showing only the important details of an object and hiding unnecessary implementation details.
//In Object-Oriented Programming, abstraction helps us focus on what an object doesin stead of focusing on how it does it internally.



//Exmple:

using System.ComponentModel;
using System.Numerics;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

abstract class Shipment
{
    public abstract decimal EstimatedCost { get; }

    public abstract void PrintShipment();
}
//In this example, every shipment must have an estimated cost and must be printable, but each shipment type can implement these details in its own way.

// b) Why is abstraction considered one of the four pillars of OOP?
//Abstraction is considered one of the four pillars of OOP because it helps make programs easier to design, understand, and maintain.
//Abstraction allows programmers to:
//-Hide complex implementation details
//- Focus on important behavior
//- Reduce code complexity
//- Reuse common design ideas
//- Work with general types instead of depending on specific classes
//For example, a program can work with a general `Shipment` type without needing to know whether the object is a `StandardShipment`, `ExpressShipment`, or `InternationalShipment`.



//Q2: a) What is the difference between an Abstract Class and an Interface?
//An abstract class is  a class that cannot be created directly. It can contain both implemented members and abstract members.

abstract class Shipment
{
    public string TrackingCode { get; set; }

    public abstract void PrintShipment();
}

//An interface defines a contract that classes must follow. It describes behavior that a class must provide.

interface ITrackable
{
    string TrackingCode { get; }

    void Track();
}

// b) When would you choose an Interface instead of an Abstract Class?

//Use an interface when you want to define behavior that different classes can share, even if those classes are not closely related.


interface ITrackable
{
    void Track();
}

//A `Shipment`, `Driver`, or `DeliveryCenter` could all implement `ITrackable`, even though they are different types.
//Choose an interface when :

//- Different classes need the same behavior
//- You do not need shared fields or constructors
//- You want a flexible design
//- A class already inherits from another class
//-You want a class to support multiple capabilities




//c) Can a class inherit from multiple abstract classes? Can it implement multiple interfaces?

//In C#, a class cannot inherit from multiple abstract classes**. A class can inherit from only one class.


class ExpressShipment : Shipment
{
}

//But a class can implement multiple interfaces.

class ExpressShipment : Shipment, ITrackable, IInsurable
{
}


//Multiple abstract classes: No
//Multiple interfaces: Yes










#endregion








#region  Practical

//Question 1 - Convert Shipment into an Abstract Class
//The `Shipment` class was converted into an abstract class. This means we cannot create a direct object from `Shipment`, but child classes such as `StandardShipment`, `ExpressShipment`, and `InternationalShipment` can still inherit from it.
//All existing properties and validation rules are kept.

public abstract class Shipment
{
    private string _trackingCode = string.Empty;
    private string _description = string.Empty;
    private decimal _weight;
    private decimal _deliveryFee;

    public string TrackingCode
    {
        get => _trackingCode;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Tracking code cannot be empty.");
            }

            _trackingCode = value;
        }
    }

    public string Description
    {
        get => _description;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Description cannot be empty.");
            }

            _description = value;
        }
    }

    public decimal Weight
    {
        get => _weight;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Weight must be greater than zero.");
            }

            _weight = value;
        }
    }

    public decimal DeliveryFee
    {
        get => _deliveryFee;
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Delivery fee cannot be negative.");
            }

            _deliveryFee = value;
        }
    }

    public DeliveryAddress Destination { get; set; }

    public Shipment(string trackingCode, string description, decimal weight, decimal deliveryFee, DeliveryAddress destination)
    {
        TrackingCode = trackingCode;
        Description = description;
        Weight = weight;
        DeliveryFee = deliveryFee;
        Destination = destination;
    }
}

//Question 2 - Create Abstract Members
//Inside `Shipment`, `EstimatedCost` was changed into an abstract property and `PrintShipment()` was changed into an abstract method.


public abstract decimal EstimatedCost { get; }

    public abstract void PrintShipment();





// Because these members are abstract, every non-abstract child class must provide its own implementation.

    //This means:
    //-Each shipment type calculates its own estimated cost.
    //- Each shipment type prints its own information.
    //Example:
public class StandardShipment : Shipment
{
    public StandardShipment(string trackingCode, string description, decimal weight, decimal deliveryFee, DeliveryAddress destination)
        : base(trackingCode, description, weight, deliveryFee, destination)
    {
    }

    public override decimal EstimatedCost => DeliveryFee + (Weight * 5);

    public override void PrintShipment()
    {
        Console.WriteLine("Standard Shipment");
        PrintBaseShipmentDetails();
    }
}


//The abstract base class also keeps a protected helper method so child classes can reuse the common print logic without making `PrintShipment()` non-abstract.



protected void PrintBaseShipmentDetails()
{
    Console.WriteLine($"Tracking Code: {TrackingCode}");
    Console.WriteLine($"Description: {Description}");
    Console.WriteLine($"Weight: {Weight} kg");
    Console.WriteLine($"Delivery Fee: {DeliveryFee:C}");
    Console.WriteLine($"Destination: {Destination}");
    Console.WriteLine($"Estimated Cost: {EstimatedCost:C}");
}

// Question 3 - Update All Shipment Types

//`StandardShipment`, `ExpressShipment`, and `InternationalShipment` all inherit from the abstract `Shipment` class.

//Because `Shipment` contains the abstract members `EstimatedCost` and `PrintShipment()`, each shipment type must implement them using its own calculation and output.



// StandardShipment

//`StandardShipment` uses the normal cost formula and prints standard shipment information.

public class StandardShipment : Shipment
{
    public StandardShipment(string trackingCode, string description, decimal weight, decimal deliveryFee, DeliveryAddress destination)
        : base(trackingCode, description, weight, deliveryFee, destination)
    {
    }

    public override decimal EstimatedCost => DeliveryFee + (Weight * 5);

    public override void PrintShipment()
    {
        Console.WriteLine("Standard Shipment");
        PrintBaseShipmentDetails();
    }
}


// ExpressShipment
//`ExpressShipment` adds `ExtraFee` to the estimated cost and prints the extra fee with the inherited shipment information.
```csharp
public class ExpressShipment : Shipment
{
    private decimal _extraFee;

    public decimal ExtraFee
    {
        get => _extraFee;
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Extra fee cannot be negative.");
            }

            _extraFee = value;
        }
    }

    public override decimal EstimatedCost => DeliveryFee + (Weight * 5) + ExtraFee;

    public ExpressShipment(string trackingCode, string description, decimal weight, decimal deliveryFee, DeliveryAddress destination, decimal extraFee)
        : base(trackingCode, description, weight, deliveryFee, destination)
    {
        ExtraFee = extraFee;
    }

    public override void PrintShipment()
    {
        Console.WriteLine("Express Shipment");
        PrintBaseShipmentDetails();
        Console.WriteLine($"Extra Fee: {ExtraFee:C}");
    }
}

//### InternationalShipment
//`InternationalShipment` adds `CustomsFee` to the estimated cost and prints the destination country and customs fee.

public class InternationalShipment : Shipment
{
    private string _destinationCountry = string.Empty;
    private decimal _customsFee;

    public string DestinationCountry
    {
        get => _destinationCountry;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Destination country cannot be empty.");
            }

            _destinationCountry = value;
        }
    }

    public decimal CustomsFee
    {
        get => _customsFee;
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Customs fee cannot be negative.");
            }

            _customsFee = value;
        }
    }

    public override decimal EstimatedCost => DeliveryFee + (Weight * 5) + CustomsFee;

    public InternationalShipment(
        string trackingCode,
        string description,
        decimal weight,
        decimal deliveryFee,
        DeliveryAddress destination,
        string destinationCountry,
        decimal customsFee)
        : base(trackingCode, description, weight, deliveryFee, destination)
    {
        DestinationCountry = destinationCountry;
        CustomsFee = customsFee;
    }

    public override void PrintShipment()
    {
        Console.WriteLine("International Shipment");
        PrintBaseShipmentDetails();
        Console.WriteLine($"Destination Country: {DestinationCountry}");
        Console.WriteLine($"Customs Fee: {CustomsFee:C}");
    }
}


//Question 4 - Create ITrackable
//`ITrackable` is an interface that contains one method:



public interface ITrackable
{
    string GetTrackingStatus();
}


//The abstract `Shipment` class implements `ITrackable`, so every concrete shipment type must provide its own tracking status.

public abstract class Shipment : ITrackable
{
    public abstract string GetTrackingStatus();
}

 StandardShipment

public override string GetTrackingStatus()
{
    return $"Shipment {TrackingCode} is Ready.";
}


 ExpressShipment


public override string GetTrackingStatus()
{
    return $"Shipment {TrackingCode} is Out for Delivery.";
}

 InternationalShipment
public override string GetTrackingStatus()
{
    return $"Shipment {TrackingCode} has been Delivered.";
}


//Example outputs:
//Shipment SH001 is Ready.
//Shipment SH002 is Out for Delivery.
//Shipment SH003 has been Delivered.

// Question 5 - Create IInsurable
//`IInsurable` is an interface that contains one method:

public interface IInsurable
{
    decimal CalculateInsurance();
}


 StandardShipment


public class StandardShipment : Shipment, IInsurable
{
    public decimal CalculateInsurance()
    {
        return EstimatedCost * 0.05m;
    }
}


 ExpressShipment

public class ExpressShipment : Shipment, IInsurable
{
    public decimal CalculateInsurance()
    {
        return EstimatedCost * 0.08m;
    }
}


InternationalShipment

public class InternationalShipment : Shipment, IInsurable
{
    public decimal CalculateInsurance()
    {
        return EstimatedCost * 0.12m;
    }
}


//Question 6 - Create DeliveryReport
//`DeliveryReport` uses interface parameters. This allows it to work with any class that implements `ITrackable` or `IInsurable`.



public class DeliveryReport
{
    public void PrintShipment(ITrackable shipment)
    {
        Console.WriteLine(shipment.GetTrackingStatus());
    }

    public void PrintInsurance(IInsurable shipment)
    {
        Console.WriteLine($"Insurance Cost: {shipment.CalculateInsurance():C}");
    }
}


//The method `PrintShipment(ITrackable shipment)` prints the tracking status of any trackable shipment.
//The method `PrintInsurance(IInsurable shipment)` prints the insurance cost of any insurable shipment.

//Example:




DeliveryReport report = new DeliveryReport();

report.PrintShipment(standardShipment);
report.PrintInsurance(standardShipment);

report.PrintShipment(expressShipment);
report.PrintInsurance(expressShipment);

report.PrintShipment(internationalShipment);
report.PrintInsurance(internationalShipment);



////Question 7 - Update DeliveryCenter
//The existing `DeliveryCenter` is reused. The shipment array, `AddShipment()`, `RemoveShipment()`, and both indexers are kept.

//A new method named `PrintTrackingStatuses()` was added. It loops through all stored shipments and prints their tracking status using the `ITrackable` interface.

public void PrintTrackingStatuses()
{
    foreach (ITrackable trackableShipment in Shipments)
    {
        Console.WriteLine(trackableShipment.GetTrackingStatus());
    }
}

//Interface Polymorphism Examples

ITrackable Polymorphism

//The declared type is `ITrackable`, but the actual object can be `StandardShipment`, `ExpressShipment`, or `InternationalShipment`.

ITrackable[] trackableItems =
{
    standardShipment,
    expressShipment,
    internationalShipment
}
;

foreach (ITrackable item in trackableItems)
{
    Console.WriteLine(item.GetTrackingStatus());
}


//At runtime, C# calls the correct `GetTrackingStatus()` method based on the real object type.

 IInsurable Polymorphism

The declared type is `IInsurable`, but each object calculates insurance using its own formula.


IInsurable[] insurableItems =
{
    standardShipment,
    expressShipment,
    internationalShipment
};

foreach (IInsurable item in insurableItems)
{
    Console.WriteLine($"Insurance Cost: {item.CalculateInsurance():C}");
}


//Question 8 - Main() Checklist
//The `Main()` method demonstrates the required abstract class and interface behavior.

//Important parts from `Main()`:

StandardShipment standardShipment = CreateStandardShipment();
ExpressShipment expressShipment = CreateExpressShipment();
InternationalShipment internationalShipment = CreateInternationalShipment();

deliveryCenter.AddShipment(standardShipment);
deliveryCenter.AddShipment(expressShipment);
deliveryCenter.AddShipment(internationalShipment);

deliveryCenter.PrintAllShipments();
deliveryCenter.PrintTrackingStatuses();

//Printing insurance values:

DeliveryReport deliveryReport = new DeliveryReport();

deliveryReport.PrintInsurance(standardShipment);
deliveryReport.PrintInsurance(expressShipment);
deliveryReport.PrintInsurance(internationalShipment);

//Using an `ITrackable[]` array:

ITrackable[] trackableItems =
{
    standardShipment,
    expressShipment,
    internationalShipment
};

foreach (ITrackable item in trackableItems)
{
    Console.WriteLine(item.GetTrackingStatus());
}

//Using an `IInsurable[]` array:

IInsurable[] insurableItems =
{
    standardShipment,
    expressShipment,
    internationalShipment
};

foreach (IInsurable item in insurableItems)
{
    Console.WriteLine($"Insurance Cost: {item.CalculateInsurance():C}");
}
```












#endregion










