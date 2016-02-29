using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletHandler 
{
    private BulletRender                            m_BulletRender;
    private Bulletcontrol                           m_BulletControl;

    public void Intialize(Vector3 initPos,Vector3 initSpeed,int type,int hurt,int mark,BulletRender renderer,Material material,List<SubPkg_BulletTrajectoryElement> trajectoryStore)
    {
        m_BulletControl                 = new Bulletcontrol();
        m_BulletRender                  = renderer;

        m_BulletControl.Initialize(initPos, initSpeed, type, hurt, mark, TimeManager.GetDuringTimeStartFire(), trajectoryStore);
        m_BulletRender.Initialize(material);
        m_BulletRender.SetPosition(m_BulletControl.GetPosition());
        m_BulletRender.SetForward(initSpeed);
    }
    public void Destructor()
    {
        m_BulletControl = null;
        m_BulletRender = null;
    }
    public void FixedUpdate()
    {
        m_BulletControl.FixedUpdate();
        m_BulletRender.SetPosition(m_BulletControl.GetPosition());
        if (m_BulletControl.GetIsNeedRefreshDir())
        {
            m_BulletRender.SetForward(m_BulletControl.GetSpeedDir());
            m_BulletControl.ResetRefreshDirFlat();
        }
    }
    public bool IsDestroyed()
    {
        return m_BulletControl.GetIsNeedDestroy();
    }
    public GameObject GetBulletObjet()
    {
        return m_BulletRender.GetBulletObject();
    }
    public Vector3 GetPosition()
    {
        return m_BulletControl.GetPosition();
    }
}
