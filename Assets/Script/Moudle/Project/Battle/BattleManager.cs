using Framework.Common;
using Framework.Event;
using NetWork.Auto;

public class BattleManager
{
    #region public interface
    public void Initialize()
    {
        EventDispatcher.Instance.RegistEvent(EventIdDefine.BeginLoadBattle, OnRecieveBeginLoadBattle);
        EventDispatcher.Instance.RegistEvent(EventIdDefine.BattleBegin,OnRecieveBattleBegin);
        EventDispatcher.Instance.RegistEvent(EventIdDefine.BattleEnd, OnRecieveBattleEnd);
        EventDispatcher.Instance.RegistEvent(EventIdDefine.BattleLogicFrame, OnRecieveLogicFrame);
    }
    #endregion

    #region event
    private void OnRecieveLogicFrame(EventElement obj)
    {
        OnRecieveCmd(obj.eventParam as SCBattleLogicFrame);
    }
    private void OnRecieveBattleEnd(EventElement obj)
    {
        Distructor();
    }
    private void OnRecieveBattleBegin(EventElement obj)
    {
        BattleBegin();
    }
    private void OnRecieveBeginLoadBattle(EventElement obj)
    {
        BeginLoadBattle();
    }
    #endregion

    #region system function
    private void Distructor()
    {
        
    }
    private void OnRecieveCmd(SCBattleLogicFrame msg)
    {
        
    }
    private void BeginLoadBattle()
    {
        SceneManager.Instance.LoadScene<SceneOnlineBattle>();
    }
    private void BattleBegin()
    {
        
    }
    #endregion
}