#region Theoretical Questions

//Q1: a) What is Abstraction in Object-Oriented Programming?
//Abstraction means showing only the important details of an object and hiding unnecessary implementation details.
//In Object-Oriented Programming, abstraction helps us focus on what an object doesin stead of focusing on how it does it internally.



//Exmple:

using System.Numerics;

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







