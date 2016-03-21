using System;
using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    [SerializeField] 
    private float m_fSpeed;

    [SerializeField]
    private int m_fHurtValue;

    private Action<Bullet> m_OnDestroyCallBack;
    private Action<Bullet> m_OnCollectionCallBack;
    private MoveBase m_MoveHandler;
    private int m_iFirePlayerId;

    public void SetOnDestroy(Action<Bullet> OnDestroyCallBack)
    {
        m_OnDestroyCallBack = OnDestroyCallBack;
    }
    public void SetFirePlayerUId(int id)
    {
        m_iFirePlayerId = id;
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
        // create explosion
        ExplosionManager.Instance.CreateExplosion("Explosion_0", transform.position);
    }
    void OnTriggerEnter(Collider other)
    {
        Debuger.Log("trigger " + other.name);
        Wall wall = other.GetComponent<Wall>();
        if (null != wall)
        {
            wall.HandlerBullet(this);  
        }
        Player player = other.GetComponent<Player>();
        if(null != player)
        {
            //trigger hit
            PlayerController control = PlayerController.GetPlayerController(other.gameObject.GetInstanceID());
            if(null != control && control.GetUid() != m_iFirePlayerId)
            {
                control.TriggerHurt(m_fHurtValue);
                CollectionBullet();
            }
        }
    }
    public void CollectionBullet()
    {
        Explosion();
        if (null != m_OnCollectionCallBack)
        {
            m_OnCollectionCallBack(this);
        }
        else
        {
            GameObject.Destroy(gameObject);
        }
    }
    private void Explosion()
    {

    }
    
}
