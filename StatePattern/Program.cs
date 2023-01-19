using StatePattern;
using Action = Models.Action;

Console.WriteLine("This is a State Pattern Demonstration Pattern");
Console.WriteLine("---------------------------------------------");

var car = new Car();

Console.WriteLine($"Initial State: {car.CurrentState}");

car.FireAction(Action.Start);
Console.WriteLine($"State: {car.CurrentState}");

car.FireAction(Action.Accelerate);
Console.WriteLine($"State: {car.CurrentState}");

car.FireAction(Action.Stop);
Console.WriteLine($"State: {car.CurrentState}");

car.FireAction(Action.Start);
Console.WriteLine($"State: {car.CurrentState}");
