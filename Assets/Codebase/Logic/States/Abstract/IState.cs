namespace Codebase.Logic.States.Abstract
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}