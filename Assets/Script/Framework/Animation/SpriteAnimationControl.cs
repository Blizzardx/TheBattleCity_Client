using System.Collections.Generic;
using UnityEngine;
using System.Collections;
public class SpriteAnimationElement
{ 
    public SpriteAnimationElement(GameObject root)
    {
        if (null == root)
        {
            Debuger.Log("Error to initialize animation");
            return;
        }
        m_ObjectRoot = root;
        m_AnimPlayer = m_ObjectRoot.GetComponent<UI2DSpriteAnimation>();
        m_Sprite = m_ObjectRoot.GetComponent<UI2DSprite>();
        m_fLength = (float) (m_AnimPlayer.frames.Length)/30.0f;
    }

    public void Play()
    {
        m_Sprite.sprite2D = m_AnimPlayer.frames[0];
        m_ObjectRoot.SetActive(true);
    }
    public void Stop()
    {
        m_ObjectRoot.SetActive(false);
    }
    public float GetAnimLength()
    {
        return m_fLength;
    }

    private GameObject              m_ObjectRoot;
    private UI2DSpriteAnimation     m_AnimPlayer;
    private UI2DSprite              m_Sprite;
    private float                   m_fLength;
}
public class SpriteAnimationControl
{
    private GameObject m_ObjectRoot;
    private Dictionary<string, SpriteAnimationElement> m_AnimationStore;
    private SpriteAnimationElement m_CurrentStep;
    
    public void Initialize(GameObject root)
    {
        if (null == root)
        {
            return;
        }
        m_ObjectRoot = root;
        m_AnimationStore = new Dictionary<string, SpriteAnimationElement>(m_ObjectRoot.transform.childCount);

        for (int i = 0; i < m_ObjectRoot.transform.childCount; ++i)
        {
            SpriteAnimationElement elem = new SpriteAnimationElement(m_ObjectRoot.transform.GetChild(i).gameObject);
            m_AnimationStore.Add(m_ObjectRoot.transform.GetChild(i).gameObject.name, elem);

            //deactive
            elem.Stop();
        }
    }
    public float Play(string animName)
    {
        SpriteAnimationElement elem = null;
        if (m_AnimationStore.TryGetValue(animName, out elem))
        {
            Stop();

            m_CurrentStep = elem;
            m_CurrentStep.Play();
            return m_CurrentStep.GetAnimLength();
        }
        else
        {
            Debuger.LogError("can't play anim" + animName);
        }
        return 0.0f;
    }
    public void Stop()
    { 
        // stop current
        if (null != m_CurrentStep)
        {
            //reset to zero frame
            m_CurrentStep.Stop();
        }
    }
}