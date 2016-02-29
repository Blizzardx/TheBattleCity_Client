using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class ExplosionHandler : MonoBehaviour 
{
    private List<Texture>   m_AnimStore;
    private int             m_nIndex;
    private float           m_fPlaySpeed;
    private float           m_fStartTime;
    private Action<ExplosionHandler> m_DestroyCallback;
    private bool m_bActiveStatus;
	void Update () 
    {
        if (!m_bActiveStatus)
        {
            return;
        }
        m_fStartTime += Time.deltaTime;
        if( m_fStartTime >= m_fPlaySpeed)
        {
            m_fStartTime = 0.0f;
            ++m_nIndex;
            if (m_nIndex == m_AnimStore.Count)
            {
                m_bActiveStatus = false;
                m_DestroyCallback(this);
            }
            else
            {
                GetComponent<Renderer>().material.mainTexture = m_AnimStore[m_nIndex];
            }
        }
	}
    public void Initialize(Vector3 pos,Material material, List<Texture> animation,Action<ExplosionHandler> destroyCallback)
    {
        m_AnimStore = animation;
        m_DestroyCallback = destroyCallback;

        m_bActiveStatus = true;
        m_fPlaySpeed = 0.02f;
        m_nIndex = 0;
        pos.y = transform.position.y;
        
        transform.position = pos;
        material.mainTexture = m_AnimStore[m_nIndex];
        GetComponent<Renderer>().material = material;

        m_fStartTime = 0.0f;
    }
    public void Destructor()
    {

    }
}
