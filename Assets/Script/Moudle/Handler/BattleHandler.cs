using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Common;
using Framework.Event;
using Framework.Message;
using Framework.Network;
using NetWork.Auto;

class BattleHandler:HandlerBase
{
    private BattleManager m_BattleMgr;

    public override void OnCreate()
    {
        base.OnCreate();
        MessageDispatcher.Instance.RegistMessage(MessageIdConstants.SC_BattleEnd, OnBattleEnd);
        MessageDispatcher.Instance.RegistMessage(MessageIdConstants.SC_BattleBegin, OnBattleBegin);
        MessageDispatcher.Instance.RegistMessage(MessageIdConstants.SC_BattleEnd, OnBattleEnd);
        MessageDispatcher.Instance.RegistMessage(MessageIdConstants.SC_BattleLogicFrame, OnBattleLogicFrame);
        MessageDispatcher.Instance.RegistMessage(MessageIdConstants.SC_BeginLoadBattle, OnBeginLoadBattle);
    }

    public void BattleLoadEnd()
    {
        CSBattleLoadEnd msg = new CSBattleLoadEnd();

        NetworkManager.Instance.SendMsgToServer(msg);
    }

    private void OnBattleBegin(IMessage obj)
    {
        EventDispatcher.Instance.BroadcastAsync(EventIdDefine.BattleBegin);
    }
    private void OnBattleEnd(IMessage obj)
    {
        EventDispatcher.Instance.BroadcastAsync(EventIdDefine.BattleEnd);
    }
    private void OnBattleLogicFrame(IMessage obj)
    {
        MessageElement msgElement = obj as MessageElement;
        SCBattleLogicFrame msg = msgElement.GetMessageBody() as SCBattleLogicFrame;

        m_BattleMgr.OnRecieveCmd(msg);
    }
    private void OnBeginLoadBattle(IMessage obj)
    {
        EventDispatcher.Instance.BroadcastAsync(EventIdDefine.BeginLoadBattle);
        SceneManager.Instance.LoadScene<SceneOnlineBattle>();
    }
}