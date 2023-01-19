using Models;
using Action = Models.Action;

namespace StatePattern
{
    public class Car
    {
        private State state = State.Stopped;
        public State CurrentState => state; //return state

        public void FireAction(Models.Action action)
        {
            state = (state, action) switch
            {
                (State.Stopped, Action.Start) => State.Started,
                (State.Started, Action.Accelerate) => State.Running,
                (State.Started, Action.Stop) => State.Stopped,
                (State.Running, Action.Stop) => State.Stopped,
                _ => state
            };
        }
    }
}
