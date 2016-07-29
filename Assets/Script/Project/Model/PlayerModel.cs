using UnityEngine;
using System.Collections;

public class PlayerModel : ModelBase
{
    public const int KeyPlayerId  = 0;

    private int m_iPlayerUid;

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
        RegisterDataHandler(KeyPlayerId, SetPlayerId);
        
    }
    private void RegisterPermisionHandler()
    {
        RegisterPermisionKey(HandlerManager.Instance.GetHandler<RoomHandler>());
    }

    #region get
    public int GetPlayerUid()
    {
        return m_iPlayerUid;
    }
    #endregion

    #region set
    // set
    private void SetPlayerId(object obj)
    {
        if (null == obj)
        {
            Debug.LogWarning("woring param ");
            return;
        }
        m_iPlayerUid = (int) (obj);
    }
    #endregion

}
