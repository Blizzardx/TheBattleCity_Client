using UnityEngine;
using System.Collections;

public class MoveBase
{
    private float m_iInitTime;
    private Vector3 m_vInitPos;
    private Vector3 m_vDir;
    private Vector3 m_vCurrentPos;

    /// <summary>
    /// call on update
    /// </summary>
    /// <param name="time"></param>
    public virtual void Update(float time)
    {
        // move
        m_vCurrentPos = (time - m_iInitTime)*m_vDir + m_vInitPos;
    }
    /// <summary>
    /// call when Reset move direction
    /// </summary>
    /// <param name="dir"></param>
    /// <param name="time"></param>
    /// <param name="currentPos"></param>
    public virtual void ResetDir(Vector3 dir, float time, Vector3 currentPos)
    {
        m_iInitTime = time;
        m_vDir = dir;
        m_vInitPos = currentPos;
        m_vCurrentPos = currentPos;
    }
    /// <summary>
    /// get current position
    /// </summary>
    /// <returns></returns>
    public virtual Vector3 GetPosition()
    {
        return m_vCurrentPos;
    }
}
