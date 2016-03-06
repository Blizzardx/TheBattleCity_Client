using System;
using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    [SerializeField] 
    private float m_fSpeed;

    private Action<Bullet> m_OnDestroyCallBack;
    private MoveBase m_MoveHandler;

    public void SetOnDestroy(Action<Bullet> OnDestroyCallBack)
    {
        m_OnDestroyCallBack = OnDestroyCallBack;
    }
	// Use this for initialization
	void Start ()
    {
        m_MoveHandler = new MoveBase();
	    m_MoveHandler.ResetDir(transform.forward * m_fSpeed, Time.time, transform.position);
	}
	// Update is called once per frame
	void Update ()
	{
	    m_MoveHandler.Update(Time.time);
	    transform.position = m_MoveHandler.GetPosition();
	}
    void OnDestroy()
    {
        if (null != m_OnDestroyCallBack)
        {
            m_OnDestroyCallBack(this);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        Debuger.Log("trigger " + other.name);
    }
}
