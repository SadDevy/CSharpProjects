namespace Timers
{
    public interface ICoutDownNotifier
    {
        void Init();
        void Run();
        void Unsubscribe();
    }
}
