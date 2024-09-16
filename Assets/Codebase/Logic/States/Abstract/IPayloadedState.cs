namespace Codebase.Logic.States.Abstract
{
    public interface IPayloadedState<in T> : IExitableState
    {
        void Enter(T payload);
    }
}