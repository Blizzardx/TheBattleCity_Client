using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerData
{
    static private PlayerData m_Instance;
    static public PlayerData GetInstance
    {
        get
        {
            if( null == m_Instance )
            {
                m_Instance = new PlayerData();
            }
            return m_Instance;
        }
    }
	private PlayerData()
    {

    }
    public void Initialize()
    {
        ClientMessageCenter.GetInstance.RegistMessage(MessageID.STC_RoomDetail, RoomDetial);
    }
    private void RoomDetial(MessageObject eb)
    {
        BattleRoomDetail = (eb.msgValue as PkgStoC_RoomDetail).RoomDetailList;

    }
    //public data
    public string PlayerName;
    public List<SubPkg_RoomDetail> BattleRoomDetail;
    public PlayMode m_PlayMode;
}
