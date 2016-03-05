using UnityEngine;
using System.Collections;

public class PlayerController
{
    private Player m_Player;
    
    public void CreatePlayer(string name)
    {
        // try load player
        var obj = ResourceManager.Instance.LoadBuildInResource<GameObject>(name, AssetType.Tank);
        if (null == obj)
        {
            Debuger.LogWarning("can't laod tank " + name);
            return;
        }
        obj = GameObject.Instantiate(obj);
        ComponentTool.Attach(null, obj.transform);

        m_Player = obj.GetComponent<Player>();
        if (null == m_Player)
        {
            Debuger.LogWarning("player instance is not attach player script " + name);
            GameObject.Destroy(obj);
            return;
        }

        m_Player.SetOnDestroyCallBack(OnDestroy);
    }

    public Player GetPlayer()
    {
        return m_Player;
    }

    private void OnDestroy()
    {
        m_Player = null;
    }

}
