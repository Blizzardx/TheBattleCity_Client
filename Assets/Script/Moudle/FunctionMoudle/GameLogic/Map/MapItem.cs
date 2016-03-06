using System;
using UnityEngine;
using System.Collections.Generic;

public class MapItem : MonoBehaviour
{
    [SerializeField]
    private List<Transform> m_PlayerPos;

    [SerializeField]
    private List<Wall> m_WallList;

    [SerializeField] 
    private Camera m_SceneCamera;

    public List<Transform> GetPlayerPosition()
    {
        return m_PlayerPos;
    }

    public Camera GetSceneCamera()
    {
        return m_SceneCamera;
    }
}
