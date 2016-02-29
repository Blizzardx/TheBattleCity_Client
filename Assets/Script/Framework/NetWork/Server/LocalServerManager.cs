using System.Net;
using UnityEngine;
using System.Collections;

public class LocalServerManager
{
    #region singleton
    static private LocalServerManager m_Instance;
    public static LocalServerManager GetInstance
    {
        get
        {
            if (null == m_Instance)
            {
                m_Instance = new LocalServerManager();
                m_Instance.Initialize();
            }
            return m_Instance;
        }
    }
    private LocalServerManager()
    {
        
    }
    #endregion

    private const string    m_strGameType = "TheBattleCity";
    private bool            m_bIsActivedLocalServer;
    private HostData[]      m_HostList;
    private LANServerLogic m_LogicHandler;

    #region public interface
    public bool IsActivedLAN { get; set; }
    public void StartLocalServer()
    {
        ServerSocketManager.GetInstance.StartServer();

        //register to update store
        GameManager.GetInstance.RegisterToUpdateStore(ServerSocketManager.GetInstance.BasicUpdate);

        m_LogicHandler = new LANServerLogic();
        m_LogicHandler.InitLAVServerLogic();
        m_LogicHandler.RegisterMsgHandler();
        IsActivedLAN = true;
    }
    public void CloseLocalServer()
    {
        ServerSocketManager.GetInstance.CloseServer();

        //register to update store
        GameManager.GetInstance.UnRegisterFromUpdateStore(ServerSocketManager.GetInstance.BasicUpdate);
        m_LogicHandler.UnRegisterMsgHandler();
        m_LogicHandler = null;
        IsActivedLAN = false;
    }
    public void CreateRoom(string roomName, string desc)
    {
        StartLocalServer();
        RegisterToLAN(roomName, desc);
    }
    public IPEndPoint ConnetcToRoom(string roomName)
    {
        foreach (HostData elem in m_HostList)
        {
            if (elem.gameName == roomName)
            {
                return new IPEndPoint(IPAddress.Parse(elem.ip[0]), ServerSocketManager.m_nLocalServerPort);
            }
        }
        return null;
    }
    public HostData[] GetRoomList()
    {
        RefreshHostList();
        return GetHostList();
    }
    #endregion

    #region system function
    private void Initialize()
    {
        ServerSocketManager.GetInstance.Initialize();
        MasterServer.RequestHostList(m_strGameType);
    }
    private HostData[] GetHostList()
    {
        return m_HostList;
    }
    private void RegisterToLAN(string RoomName,string desc)
    {
        Network.InitializeServer(32, ServerSocketManager.m_nLocalServerPort, !Network.HavePublicAddress());
        MasterServer.RegisterHost(m_strGameType, RoomName, desc);
    }
    private void RefreshHostList()
    {
        m_HostList = MasterServer.PollHostList();
    }
    #endregion
}
