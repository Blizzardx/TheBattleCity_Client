using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bulletcontrol
{
    private int         m_Type;
    private int         m_Mark;
    private int         m_Hurt;
    private Vector3     m_Speed;
    private Vector3     m_Position;
    private Vector3     m_InitPosition;
    public  float       m_ResetTime;
    private List<SubPkg_BulletTrajectoryElement> m_TrajectoryStore;
    private int         m_nCurrentIndex;
    private bool        m_bIsNeedDestroy;
    private bool        m_bIsNeedRefreshDir;
    public void Initialize(Vector3 initPos, Vector3 initSpeed, int type, int hurt, int mark, float time, List<SubPkg_BulletTrajectoryElement> trajectoryStore)
    {
        float deltaTime = TimeManager.GetDuringTimeStartFire() - time;
        m_Position = initPos + initSpeed * deltaTime;

        m_Type      = type;
        m_Hurt      = hurt;
        m_InitPosition = initPos;
        m_Speed     = initSpeed;
        m_ResetTime = time;
        m_TrajectoryStore = trajectoryStore;
        m_nCurrentIndex = 1;
        m_bIsNeedDestroy = false;
        ResetRefreshDirFlat();
    }
    public void FixedUpdate()
    {
        float currentTime = TimeManager.GetDuringTimeStartFire();
        if (currentTime >= m_TrajectoryStore[m_nCurrentIndex].StartTime)
        {
            if (m_nCurrentIndex + 1 == m_TrajectoryStore.Count)
            {
                m_bIsNeedDestroy = true;
            }
            else
            {
                m_InitPosition = ComponentTool.ConvertSubVec3ToVector3(m_TrajectoryStore[m_nCurrentIndex].Position);
                m_ResetTime = (float)(m_TrajectoryStore[m_nCurrentIndex].StartTime);
                m_Speed = ComponentTool.ConvertSubVec3ToVector3(m_TrajectoryStore[m_nCurrentIndex].Speed);
                ++m_nCurrentIndex;
                m_bIsNeedRefreshDir = true;
            }
        }
        m_Position = m_InitPosition + m_Speed * (currentTime - m_ResetTime);
    }
    public Vector3 GetPosition()
    {
        return m_Position;
    }
    public Vector3 GetSpeedDir()
    {
        return m_Speed.normalized;
    }
    public void ResetRefreshDirFlat()
    {
        m_bIsNeedRefreshDir = false;
    }
    public int GetBulletType()
    {
        return m_Type;
    }
    public int GetHurt()
    {
        return m_Hurt;
    }
    public bool GetIsNeedDestroy()
    {
        return m_bIsNeedDestroy;
    }
    public bool GetIsNeedRefreshDir()
    {
        return m_bIsNeedRefreshDir;
    }
}
