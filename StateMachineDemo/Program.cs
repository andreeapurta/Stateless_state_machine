using Models;
using Stateless.Graph;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.X86;
using Action = Models.Action;
using State = Models.State;

Console.WriteLine("This is a State Machine Demonstration - Stateless Nuget");
Console.WriteLine("---------------------------------------------");
var car = new Stateless.StateMachine<State, Action>(State.Stopped);


///for the stopped state we can allow start.For the allow, we use “Permit” method.
/// So, we can Permit the “Car.Action.Start” when the car is stopped.
/// The second parameter is the state that will be applied, in this case, “Car.State.Started”

car.Configure(State.Stopped).Permit(Action.Start, State.Started);
car.Configure(State.Started)
.Permit(Action.Accelerate, State.Running)
//.Ignore(Action.Start) //if the action is start, just ignore and keep the same state
    .PermitReentry(Action.Start) //even if the car ws started permit restart
    .Permit(Action.Stop, State.Stopped);
    //.OnEntry(state => Console.WriteLine($"Entry: {state.Source}, {state.Destination}"))
    //.OnExit(state => Console.WriteLine($"Exit: {state.Source}, {state.Destination}"));

//we want to pass param when Running state is entered 
var triggerWithParam = car.SetTriggerParameters<int>(Action.Accelerate);

car.Configure(State.Running)
    .Permit(Action.Stop, State.Stopped)
    .OnEntryFrom(triggerWithParam, speed => Console.Write($"Speed: {speed}, "))
//triggers an internal transition and performs an action on it
    .InternalTransition(Action.Start, () => Console.WriteLine("Start called while running"));


Console.WriteLine($"Initial State: {car.State}");

car.Fire(Action.Start);
Console.WriteLine($"State: {car.State}");

//Ignore in action -this will be ignored because the car is already started above
//car.Fire(Action.Start);
//Console.WriteLine($"State: {car.State}");

//InternalTransition in action- example
//car.Fire(Action.Accelerate);
car.Fire(triggerWithParam, 50);  //- trigger with params example
Console.WriteLine($"State: {car.State}");

car.Fire(Action.Stop);
Console.WriteLine($"State: {car.State}");

