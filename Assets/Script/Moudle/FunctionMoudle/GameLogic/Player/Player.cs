using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class TankFireInfo
{
    public Animator m_GunAnimator;
    public Transform m_TransformGun;
    public List<Transform> m_TransformFirePos;
    public List<Transform> m_TransformFireLightPos;
}
public class Player : MonoBehaviour
{
    [SerializeField] private List<TankFireInfo> m_FireInfo;
    [SerializeField] private float      m_FireCd;
    [SerializeField] private Transform  m_TransformRayRoot;
    [SerializeField] private float      m_fSpeed;
    private List<Transform>         m_TransformRayPoingList;
    private Action<int>             m_OnDestroyCallBack;
    private Action          m_OnDeadCallBack;
    private MoveBase        m_MoveHandler;
    private float           m_fCurrentSpeed;
    private bool            m_bIsStop;
    private float           m_fCurrentCd;
    private Vector3         m_FowordMoveDir;
    private int             m_iHp;
    private int m_iMaxHp;
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
    public void SetOnDestroyCallBack(Action<int> ondestroy)
    {
        m_OnDestroyCallBack = ondestroy;
    }
    public void SetOnDeadCallBack(Action onDead)
    {
        m_OnDeadCallBack = onDead;
    }
    private void OnDestroy()
    {
        if (null != m_OnDestroyCallBack)
        {
            m_OnDestroyCallBack(this.GetInstanceID());
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
            m_FowordMoveDir = newDir.normalized * m_fSpeed;
            newDir = m_FowordMoveDir;
            return true;
        }
        return false;
    }
    public List<Vector3> GetFirePos(Vector3 dir)
    {
        List<Vector3> resList = new List<Vector3>(m_FireInfo.Count);

        for (int i = 0; i < m_FireInfo.Count; ++i)
        {
            TankFireInfo elem = m_FireInfo[i];
            Vector3 currentGunForward = elem.m_TransformGun.forward;
            elem.m_TransformGun.LookAt(dir);
            foreach (var elemFirePos in elem.m_TransformFirePos)
            {
                resList.Add(elemFirePos.position);
            }
            //reset
            elem.m_TransformGun.forward = currentGunForward;
        }

        return resList;
    }
    public bool TryFire()
    {
        return m_fCurrentCd <= 0.0f;
    }
    public void BeginFireCd()
    {
        m_fCurrentCd = m_FireCd;
    }
    public void Fire(Vector3 dir)
    {
        //reset y
        dir.y = 0;

        //set anim
        for (int i = 0; i < m_FireInfo.Count; ++i)
        {
            TankFireInfo elem = m_FireInfo[i];
            elem.m_TransformGun.LookAt(dir);
            elem.m_GunAnimator.SetTrigger("Fire");
        }
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
    //Speed
    public void SetSpeed(float speed)
    {
        m_fSpeed = speed;
    }

    public void AddSpeed(float speed)
    {
        m_fSpeed += speed;
    }

    public void SubSpeed(float speed)
    {
        m_fSpeed -= speed;
    }
    public float GetSpeed()
    {
        return m_fSpeed;
    }
    //HP
    public void SetMaxHp(int hp)
    {
        m_iMaxHp = hp;
    }
    public void SetHp(int hp)
    {
        m_iHp = hp;
    }
    public void AddHp(int value)
    {
        m_iHp += value;
    }
    public void SubHp(int value)
    {
        m_iHp -= value;
        CheckDead();
    }
    public int GetHp()
    {
        return m_iHp;
    }
    public float GetHpValue()
    {
        return (float)(m_iHp) / (float)(m_iMaxHp);
    }
    //
    public void CheckDead()
    {
        if (m_iHp <= 0)
        {
            if (null != m_OnDeadCallBack)
            {
                m_OnDeadCallBack();
            }
        }
    }
    public MoveBase GetMoveInfo()
    {
        return m_MoveHandler;
    }
}
