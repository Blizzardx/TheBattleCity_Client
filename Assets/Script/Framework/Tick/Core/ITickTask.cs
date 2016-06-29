namespace Framework.Tick
{
    public interface ITickTask
    {
        void Tick();

        void SetLastTickTime(long lastTickTime);
    }

}