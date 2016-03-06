using System;
using UnityEngine;
using System.Collections.Generic;


public class Player : MonoBehaviour
{
    [SerializeField]
    private Transform m_TransformGun;
    [SerializeField]
    private Transform m_TransformFirePos;
    [SerializeField]
    private Transform m_TransformFireLightPos;
    [SerializeField]
    private Animator m_GunAnimator;

    private Action m_OnDestroy;
    private MoveBase m_MoveHandler;
    private float m_fCurrentSpeed;
    private bool m_bIsStop;

    private void Start()
    {
        m_MoveHandler = new MoveBase();
        m_MoveHandler.ResetDir(Vector3.zero, Time.time, transform.position);
    }
    public void SetOnDestroyCallBack(Action ondestroy)
    {
        m_OnDestroy = ondestroy;
    }
    private void OnDestroy()
    {
        if (null != m_OnDestroy)
        {
            m_OnDestroy();
        }
    }
    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }
    public void Move(Vector2 dir)
    {
        if (null == m_MoveHandler)
        {
            return;
        }

        //set dir
        Vector3 newDir = new Vector3(dir.x, transform.forward.y, dir.y);

        if (CheckDir(newDir))
        {
            Debuger.Log("new dir " + newDir);
            if (newDir == Vector3.zero)
            {
                m_bIsStop = true;
                m_MoveHandler.ResetDir(Vector3.zero, Time.time, transform.position);
            }
            else
            {
                m_bIsStop = false;   
                transform.forward = newDir;
                m_MoveHandler.ResetDir(newDir, Time.time - Time.deltaTime, transform.position);
                
            }
            OnMoveChangedEvent();
        }
    }
    public void Fire(Vector3 dir)
    {
        dir.y = 0;
        m_TransformGun.LookAt(dir);
        m_GunAnimator.SetTrigger("Fire");
        BulletManager.Instance.CreateBullet("Bullet_0", m_TransformFirePos.position, (dir-m_TransformFirePos.position).normalized);
    }
    public void Update()
    {
        if (null == m_MoveHandler)
        {
            return;
        }
        m_MoveHandler.Update(Time.time);
        transform.position = m_MoveHandler.GetPosition();
    }
    protected virtual void OnMoveChangedEvent()
    {
        
    }
    private bool CheckDir(Vector3 dir)
    {
        if (dir == Vector3.zero)
        {
            if (m_bIsStop)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        return dir != transform.forward;
    }
}
