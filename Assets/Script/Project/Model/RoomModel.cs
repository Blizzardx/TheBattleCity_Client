using System.Collections.Generic;
using NetWork.Auto;
using UnityEngine;

public class PlayerInfo
{
    public void SetData(NetWork.Auto.PlayerInfo info)
    {
        Uid = info.Uid;
        name = info.Name;
        hp = info.Hp;
        meshName = info.MeshName;
        positionId = info.PositionId;
    }
    public int Uid;
    public string name;
    public int hp;
    public string meshName;
    public int positionId;
}
partial class RoomModel : ModelBase
{
    public const int KeyRoomName = 0;
    public const int KeyMapName = 1;
    public const int KeyMemberCount = 2;
    public const int KeyRoomList = 3;
    public const int KeyPlayerList = 4;

    private string m_strRoomName;
    private string m_strMapName;
    private int m_iMemberCount;
    private List<RoomInfo> m_RoomList;
    private List<PlayerInfo> m_PlayerList;
    private PlayerInfo m_RoomSelfPlayer;
    
    protected override void OnCreate()
    {
        Debug.Log("init player model");

        // register data handler
        RegisterHandler();

        // register handler
        RegisterPermisionHandler();
    }
    private void RegisterHandler()
    {
        RegisterDataHandler(KeyRoomName, SetRoomName);
        RegisterDataHandler(KeyMapName, SetMapName);
        RegisterDataHandler(KeyMemberCount, SetMemberCount);
        RegisterDataHandler(KeyRoomList, SetRoomList);
        RegisterDataHandler(KeyPlayerList, SetPlayerList);
    }
    private void RegisterPermisionHandler()
    {
        RegisterPermisionKey(HandlerManager.Instance.GetHandler<RoomHandler>(RoomHandler.Index));
    }
    #region get
    public string GetRoomName()
    {
        return m_strRoomName;
    }
    public string GetMapName()
    {
        return m_strMapName;
    }
    public int GetMemberCount()
    {
        return m_iMemberCount;
    }
    public PlayerInfo GetPlayerInfo(int uid)
    {
        for (int i = 0; i < m_PlayerList.Count; ++i)
        {
            if (m_PlayerList[i].Uid == uid)
            {
                return m_PlayerList[i];
            }
        }
        return null;
    }
    public List<PlayerInfo> GetPlayerInfoList()
    {
        return m_PlayerList;
    }
    public List<RoomInfo> GetRoomList()
    {
        return m_RoomList;
    }
    public PlayerInfo GetSelf()
    {
        return m_RoomSelfPlayer;
    }
    #endregion

    #region set
    // set
    private void SetRoomName(object obj)
    {
        m_strRoomName = (string)(obj);
    }
    private void SetMapName(object obj)
    {
        m_strMapName = (string)(obj);
    }
    private void SetMemberCount(object obj)
    {
        m_iMemberCount = (int)(obj);
    }
    private void SetRoomList(object obj)
    {
        m_RoomList = obj as List<RoomInfo>;
    }
    private void SetPlayerList(object obj)
    {
        if (null == m_PlayerList)
        {
            m_PlayerList = new List<PlayerInfo>();
        }
        NetWork.Auto.PlayerInfo info = obj as NetWork.Auto.PlayerInfo;
        PlayerInfo elem = new PlayerInfo();
        elem.SetData(info);

        m_PlayerList.Add(elem);

        for (int i = 0; i < m_PlayerList.Count; ++i)
        {
            if (m_PlayerList[i].Uid == ModelManager.Instance.GetModel<PlayerModel>(PlayerModel.Index).GetPlayerUid())
            {
                m_RoomSelfPlayer = m_PlayerList[i];
                // mark self player
                break;
            }
        }
    }
    #endregion

}