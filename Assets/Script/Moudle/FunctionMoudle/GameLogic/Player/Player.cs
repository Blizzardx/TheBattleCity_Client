using System;
using UnityEngine;
using System.Collections.Generic;


public class Player : MonoBehaviour
{
    [SerializeField] private Transform m_TransformGun;
    [SerializeField] private Transform m_TransformFirePos;
    [SerializeField] private Transform m_TransformFireLightPos;
    [SerializeField] private Animator   m_GunAnimator;
    [SerializeField] private float      m_FireCd;
    [SerializeField] private Transform  m_TransformRayRoot;
    private List<Transform>     m_TransformRayPoingList;
    private Action          m_OnDestroy;
    private MoveBase        m_MoveHandler;
    private float           m_fCurrentSpeed;
    private bool            m_bIsStop;
    private float           m_fCurrentCd;
    private Vector3         m_FowordMoveDir;

    //move
    private float m_fMoveTime;
    private float m_fCurrentSpendTime;
    private void Start()
    {
        m_bIsStop = true;
        m_MoveHandler = new MoveBase();
        m_MoveHandler.ResetDir(Vector3.zero, Time.time, transform.position);
        m_TransformRayPoingList = new List<Transform>();
        for (int i = 0; i < m_TransformRayRoot.childCount; ++i)
        {
            m_TransformRayPoingList.Add(m_TransformRayRoot.GetChild(i));
        }
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
    public void DoMove(Vector3 newDir,Vector3 position)
    {
        if (newDir == Vector3.zero)
        {
            m_bIsStop = true;
            m_MoveHandler.ResetDir(Vector3.zero, Time.time, position);
        }
        else
        {
            m_bIsStop = false;
            transform.forward = newDir;
            m_MoveHandler.ResetDir(newDir, Time.time - Time.deltaTime, position);
            //caculate time
            float distance = CheckMoveRaycast(newDir);
            ResetTime(distance / newDir.magnitude - Time.deltaTime);
        }
        OnMoveChangedEvent();
    }
    public bool TryMove(Vector2 dir,ref Vector3 newDir)
    {
        if (null == m_MoveHandler)
        {
            return false;
        }

        //set dir
        newDir = new Vector3(dir.x, transform.forward.y, dir.y);

        if (CheckDir(newDir))
        {
            m_FowordMoveDir = newDir.normalized;
            return true;
        }
        return false;
    }
    public Vector3 GetFirePos(Vector3 dir)
    {
        Vector3 res = Vector3.zero;
        Vector3 currentGunForward = m_TransformGun.forward;
        m_TransformGun.LookAt(dir);
        res = m_TransformFirePos.position;

        //reset
        m_TransformGun.forward = currentGunForward;
        return res;
    }
    public bool TryFire()
    {
        return m_fCurrentCd <= 0.0f;
    }
    public void Fire(Vector3 dir)
    {
        if (m_fCurrentCd > 0.0f)
        {
            return;
        }
        m_fCurrentCd = m_FireCd;
        dir.y = 0;
        m_TransformGun.LookAt(dir);
        m_GunAnimator.SetTrigger("Fire");
        //BulletManager.Instance.CreateBullet("Bullet_0", m_TransformFirePos.position,
        //    (dir - m_TransformFirePos.position).normalized);
    }
    public void Update()
    {
        if (null != m_MoveHandler)
        {
            //check move time
            m_fCurrentSpendTime += Time.deltaTime;
            if (!m_bIsStop && m_fCurrentSpendTime > m_fMoveTime)
            {
                //  trigger to stop
                DoMove(Vector3.zero, transform.position);
            }
            else
            {
                m_MoveHandler.Update(Time.time);
                transform.position = m_MoveHandler.GetPosition();
            }
        }
        if (m_fCurrentCd > 0.0f)
        {
            m_fCurrentCd -= Time.deltaTime;
        }
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
        bool res = dir != m_FowordMoveDir;
        if (res)
        {
            float distance = CheckMoveRaycast(dir);
            if (distance <= 0.1f)
            {
                return false;
            }
        }
        return res;
    }
    private void OnTriggerEnter(Collider other)
    {
        Debuger.Log("player " + other.name);
        Wall wall = other.GetComponent<Wall>();
    }
    private float CheckMoveRaycast(Vector3 dir)
    {
        m_TransformRayRoot.forward = dir.normalized;
        float distance = 100.0f;
        for (int i = 0; i < m_TransformRayPoingList.Count; ++i)
        {
            Vector3 or = m_TransformRayPoingList[i].position;
            Ray ray = new Ray(or, dir);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, 100, 1 << (LayerMask.NameToLayer("Wall"))))
            {
                float tmpdistance = Vector3.Distance(or, hitInfo.point);
                if (i == 0 || tmpdistance <= distance)
                {
                    distance = tmpdistance;
                }
            }
        }
        return distance;
    }
    private void ResetTime(float time)
    {
        m_fMoveTime = time;
        m_fCurrentSpendTime = 0.0f;
    }
}
