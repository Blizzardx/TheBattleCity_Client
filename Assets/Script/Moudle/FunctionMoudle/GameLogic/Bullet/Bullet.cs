using System;
using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    [SerializeField] 
    private float m_fSpeed;

    private Action<Bullet> m_OnDestroyCallBack;
    private Action<Bullet> m_OnCollectionCallBack;
    private MoveBase m_MoveHandler;

    public void SetOnDestroy(Action<Bullet> OnDestroyCallBack)
    {
        m_OnDestroyCallBack = OnDestroyCallBack;
    }

    public void SetOnCollection(Action<Bullet> onCollectionCallBack)
    {
        m_OnCollectionCallBack = onCollectionCallBack;
    }
	// Use this for initialization
	public void Reset ()
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
        Wall wall = other.GetComponent<Wall>();
        if (null != wall)
        {
            wall.HandlerBullet(this);
            
        }
    }
    public void CollectionBullet()
    {
        if (null != m_OnCollectionCallBack)
        {
            m_OnCollectionCallBack(this);
        }
        else
        {
            GameObject.Destroy(gameObject);
        }
    }
}
