using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class GenItemInfo
{
    public List<int> m_ItemList;
    public List<int> m_PosList;
    public int itemMaxCount;
    public int pertimeGenItemCountMin;
    public int pertimeGenItemCountMax;
    public int deltaTime;
    public int initItemCount;
}
public class MapItem : MonoBehaviour
{
    [SerializeField]
    private List<Transform> m_PlayerPos;

    [SerializeField]
    private List<Wall> m_WallList;

    [SerializeField] 
    private Camera m_SceneCamera;

    [SerializeField]
    private List<GameObject> m_ItemPosList;

    [SerializeField]
    private GenItemInfo m_GenItemInfo;

    public List<Transform> GetPlayerPosition()
    {
        return m_PlayerPos;
    }

    public Camera GetSceneCamera()
    {
        return m_SceneCamera;
    }
    public GenItemInfo GetGenItemInfo()
    {
        return m_GenItemInfo;
    }

    internal bool GetItemPositionById(int positionId, out Vector3 pos)
    {
        if (positionId < 0 || positionId >= m_ItemPosList.Count)
        {
            pos = Vector3.zero;
            return false;
        }
        pos = m_ItemPosList[positionId].transform.position;
        return true;
    }
}
