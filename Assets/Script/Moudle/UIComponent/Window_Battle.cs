using UnityEngine;
using System.Collections;

public class Window_Battle : WindowBase
{
    private Button                  m_ButtonReady;
    private PkgCToS_ReadyToBattle   m_PkgReadytobattle;
    private bool                    m_bCountting;
    private GameObject              m_CounttingPanel;
    private UILabel                 m_LabelCountting;

    public override void OnOpen(System.Action OnFinshedCallBack, object paramter)
    {
        base.OnOpen(OnFinshedCallBack, paramter);

        m_ButtonReady = FindChildComponent<Button>("Button_Ready");
        m_LabelCountting = FindChildComponent<UILabel>("Label_Countting");
        m_CounttingPanel = FindChildComponent<Transform>("Panel_Countting").gameObject;

        m_bCountting = false;
        ShowCounttingPanel(m_bCountting);

        m_ButtonReady.AddCallBack(OnClickReady);

        m_PkgReadytobattle = new PkgCToS_ReadyToBattle();
        m_PkgReadytobattle.PlayerName = PlayerData.GetInstance.PlayerName;
        ClientMessageCenter.GetInstance.RegistMessage(MessageID.STC_BeginBattle, BattleBegin);

        WindowManager.GetInstance.RegisterToUpdateStore(Update);
        OnFinshedCallBack();
    }
    public override void OnClose()
    {
        base.OnClose();
    }
    public override void OnDestroy()
    {
        base.OnDestroy();
        ClientMessageCenter.GetInstance.UnregistMessage(MessageID.STC_BeginBattle, BattleBegin);
        WindowManager.GetInstance.UnRegisterFromUpdateStore(Update);
    }
    public void OnClickReady()
    {
        //m_PkgReadytobattle
        ClientSocketManager.GetInstance.SendMsgToRemoteServer(new MessageObject(MessageID.CTS_ReadyToBattle, m_PkgReadytobattle));
        BattleScene.GetInstance.ChangeBattleSceneStatusTo(BattleScene.BattleSceneStatus.Ready);
    }
    public void BattleBegin(MessageObject msg)
    {
        //when battle begin
        PkgSToC_BeginBattle tmpMsg = msg.msgValue as PkgSToC_BeginBattle;
        TimeManager.ResetStartTime(tmpMsg.BeginTimeUTC);
        BattleScene.GetInstance.ChangeBattleSceneStatusTo(BattleScene.BattleSceneStatus.Battle);
        m_bCountting = true;
        ShowCounttingPanel(m_bCountting);
    }
    private void Update()
    {
        //check 
        if( m_bCountting )
        {
            if( TimeManager.GetDuringTimeStartFire() > 0 )
            {
                //start battle
                m_bCountting = false;
                ShowCounttingPanel(m_bCountting);
                BattleScene.GetInstance.ChangeBattleSceneStatusTo(BattleScene.BattleSceneStatus.Battle);
            }
            else
            {
                //show 
                m_LabelCountting.text = (TimeManager.GetDuringTimeStartFire() * -1.0f).ToString();
            }
        }
    }
    private void ShowCounttingPanel(bool status)
    {
        m_CounttingPanel.SetActive(status);
    }
}
