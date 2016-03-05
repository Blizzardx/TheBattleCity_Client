using System;
using UnityEngine;
using System.Collections.Generic;

public class MapItem : MonoBehaviour
{
    [SerializeField]
    private List<Transform> m_PlayerPos;

    [SerializeField]
    private List<Wall> m_WallList;


    public List<Transform> GetPlayerPosition()
    {
        return m_PlayerPos;
    }
}
