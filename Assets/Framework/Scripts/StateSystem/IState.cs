namespace Framework.StateSystem
{
    public interface IState
    {
        void Tick();
        void OnEnter();
        void OnExit();
    }
}