using UnityEngine;
using System.Collections;

public class TankControl
{
    private Vector3 m_InitPosition;
    private Vector3 m_Position;
    private Vector3 m_Speed;
    private Vector3 m_LastSpeed;
    public  float   m_ResetSpeedTime;
    
    public void FixedUpdate()
    {
        //update Positon
        m_Position = m_InitPosition + m_Speed * (TimeManager.GetDuringTimeStartFire() - m_ResetSpeedTime);
        m_LastSpeed = m_Speed;

    }
    public Vector3 Speed
    {
        get
        {
            return m_Speed;
        }
        set
        {
            m_Speed = value;
        }
    }
    public void SetNetworkSpeed(Vector3 InitPosition,Vector3 newSpeed,double setTime)
    {
        //float deltaT = (float)setTime - TimeManager.GetDuringTimeStartFire();
        //m_Position = deltaT * (newSpeed - m_Speed);

        m_InitPosition = InitPosition;
        m_ResetSpeedTime = (float)setTime;
        m_Speed = newSpeed;
    }
    public Vector3 Position
    {
        get
        {
            return m_Position;
        }
        set
        {
            m_Position = value;
        }
    }
    public Vector3 InitPosition
    {
        get
        {
            return m_InitPosition;
        }
        set
        {
            m_InitPosition = value;
        }
    }

    public bool GetIsNeedRefreshHandler()
    {
        return m_LastSpeed != m_Speed;
    }
}
