using UnityEngine;
using System.Collections;

public class MapManager:Singleton<MapManager>
{
    private MapItem m_CurrentMap;

    public void LoadMap(string name)
    {
        if (null != m_CurrentMap)
        {
            GameObject.Destroy(m_CurrentMap.gameObject);
        }

        var obj = ResourceManager.Instance.LoadBuildInResource<GameObject>(name, AssetType.Map);
        if (obj == null)
        {
            Debuger.LogWarning("can't laod map " + name);
            return;
        }
        obj = GameObject.Instantiate(obj) as GameObject;
        ComponentTool.Attach(null, obj.transform);

        m_CurrentMap = obj.GetComponent<MapItem>();
        if (null == m_CurrentMap)
        {
            Debuger.Log("map do not attach script mapitem " + name);
            GameObject.Destroy(obj);
            return;
        }
    }

    public MapItem GetCurrentMapInfo()
    {
        return m_CurrentMap;
    }
}
