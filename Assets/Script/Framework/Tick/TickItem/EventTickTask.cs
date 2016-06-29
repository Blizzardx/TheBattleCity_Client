using Framework.Event;

namespace Framework.Tick
{
    class EventTickTask: AbstractTickTask
    {
        protected override bool FirstRunExecute()
        {
            return false;
        }

        protected override int GetTickTime()
        {
            return TickTaskConstant.TICK_ASYNC;
        }

        protected override void Beat()
        {
            EventDispatcher.Instance.Update();
        }
    }
}
