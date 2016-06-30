using UnityEngine;

class RoomModel : ModelBase
{
    public const int KeyRoomName = 0;
    public const int KeyMapName = 1;
    public const int KeyMemberCount = 2;

    private string m_strRoomName;
    private string m_strMapName;
    private int m_iMemberCount;


    protected override void OnCreate()
    {
        Debug.Log("init player model");

        // register id to data
        RegisterId();

        // register data handler
        RegisterHandler();

        // register handler
        RegisterPermisionHandler();
    }
    private void RegisterId()
    {
        RegisterIdToData(KeyRoomName, m_strRoomName);
        RegisterIdToData(KeyMapName, m_strMapName);
        RegisterIdToData(KeyMemberCount, m_iMemberCount);
    }
    private void RegisterHandler()
    {
        RegisterDataHandler(KeyRoomName, SetRoomName);
        RegisterDataHandler(KeyMapName, SetMapName);
        RegisterDataHandler(KeyMemberCount, SetMemberCount);
    }
    private void RegisterPermisionHandler()
    {
        RegisterPermisionKey(HandlerManager.Instance.GetHandler<RoomHandler>());
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
    #endregion

}