using System.Collections.Generic;
using Common.Component;
using Common.Tool;
using UnityEngine;
using System;
using Framework.Asset;

public class AudioPlayer:Singleton<AudioPlayer>
{
    public class AudioCallBackElement
    {
        public AudioCallBackElement(Action<string> fun, string name)
        {
            this.name = name;
            this.callBackFunc = fun;
        }
        public Action<string> callBackFunc;
        public string name;

        public void Execute()
        {
            if (null == callBackFunc)
            {
                Debug.Log("Execute call back " + name + " but ,callback is null !");
                return;
            }
            Debug.Log("Execute call back " + name);
            callBackFunc(name);
        }
    }
    public class AudioElementStruct
    {
        public GameObject   m_Root;
        public AudioClip    m_AudioClip;
        public AudioSource m_AudioSource;
        public bool         m_bIsLoop;
        public Action<string> m_OnFinishedCallBack;
        public float m_fCurrentPlayedLength;
        public float m_fAudioLength;
    }

    private List<AudioElementStruct>    m_AudioClipList;
    private GameObject                  m_AudioRoot;
    private Stack<AudioElementStruct>   m_AudioStorePool;
    private AudioSource                 m_RecordAudioSource;
    private const int                   m_iPoolSize = 10;
    private List<AudioCallBackElement> m_AudioCallBacktmpList; 

    public AudioPlayer()
    {
        m_AudioRoot = ComponentTool.FindChild("AudioPlayer", null);
        if (null == m_AudioRoot)
        {
            // create new and do not destroy on load
            m_AudioRoot = new GameObject();
            m_AudioRoot.name = "AudioPlayer";
            m_AudioRoot.AddComponent<DoNotDestory>();
        }
        m_AudioClipList = new List<AudioElementStruct>(m_iPoolSize);
        m_AudioStorePool = new Stack<AudioElementStruct>(m_iPoolSize);
        m_AudioCallBacktmpList = new List<AudioCallBackElement>();

        InitAudioPool();
    }
    public void PlayAudio(string resource,Vector3 postion,bool isLoop,Action<string> onFinishedCallBack = null)
    {
        Debug.Log("Play audio: " + resource);

        CheckResource();

        AudioElementStruct elem = CreateElement(resource, postion, isLoop);

        CheckCallBack(elem, onFinishedCallBack);

        if (null != elem)
        {
            m_AudioClipList.Add(elem);
        }
    }
    public void PlayAudio(string resource, Transform parent, bool isLoop, Action<string> onFinishedCallBack = null)
    {
        Debug.Log("Play audio: " + resource);

        CheckResource();

        AudioElementStruct elem = CreateElement(resource, parent, isLoop);

        CheckCallBack(elem, onFinishedCallBack);

        if (null != elem)
        {
            m_AudioClipList.Add(elem);
        }
    }
    public void PlayAudio(AudioClip clip, Vector3 postion, bool isLoop, Action<string> onFinishedCallBack = null)
    {
        Debug.Log("Play audio: " + clip.name);

        AudioElementStruct elem = null;
        if (m_AudioStorePool.Count > 0)
        {
            elem = m_AudioStorePool.Pop();
        }
        else
        {
            elem = new AudioElementStruct();
            elem.m_Root = new GameObject("AudioClip");
            ComponentTool.Attach(m_AudioRoot.transform, elem.m_Root.transform);
            elem.m_AudioSource = elem.m_Root.AddComponent<AudioSource>();
        }

        elem.m_AudioClip = clip;
        if (elem.m_AudioClip == null)
        {
            return;
        }
        elem.m_Root.transform.position = postion;
        elem.m_AudioSource.clip = elem.m_AudioClip;
        elem.m_AudioSource.loop = isLoop;
        elem.m_bIsLoop = isLoop;
        elem.m_AudioSource.Play();
        elem.m_Root.name = clip.name;

        CheckCallBack(elem, onFinishedCallBack);

        if (null != elem)
        {
            m_AudioClipList.Add(elem);
        }
    }
    public AudioSource GetRecordAudioSource()
    {
        if(null == m_RecordAudioSource)
        {
            var obj = new GameObject("AudioClip");
            ComponentTool.Attach(m_AudioRoot.transform, obj.transform);
            m_RecordAudioSource = obj.AddComponent<AudioSource>();
        }
        return m_RecordAudioSource;
    }
    public void StopAudio(string resource)
    {
        for (int i = 0; i < m_AudioClipList.Count; )
        {
            AudioElementStruct elem = m_AudioClipList[i];
            if (elem.m_Root.name == resource)
            {
                CollectionAudio(elem);
                m_AudioClipList.RemoveAt(i);
            }
            else
            {
                ++i;
            }
        }
    }
    public void StopAllAudio()
    {
        for (int i = 0; i < m_AudioClipList.Count;++i)
        {
            AudioElementStruct elem = m_AudioClipList[i];
            CollectionAudio(elem);
        }
        m_AudioClipList.Clear();
    }
    public bool IsPlayingAudio(string name)
    {
        foreach(var elem in m_AudioClipList)
        {
            if(elem.m_Root.name == name)
            {
                return elem.m_AudioSource.isPlaying;
            }
        }
        return false;
    }
    private AudioElementStruct CreateElement(string resource, Vector3 postion, bool isLoop)
    {
        AudioElementStruct elem = null;
        if (m_AudioStorePool.Count > 0)
        {
            elem = m_AudioStorePool.Pop();
        }
        else
        {
            elem = new AudioElementStruct();
            elem.m_Root = new GameObject("AudioClip");
            ComponentTool.Attach(m_AudioRoot.transform, elem.m_Root.transform);
            elem.m_AudioSource = elem.m_Root.AddComponent<AudioSource>();
        }

        elem.m_AudioClip = AssetManager.Instance.LoadAsset<AudioClip>(resource);
        if (elem.m_AudioClip == null)
        {
            Debug.LogError("there is no audio : " + resource);
            return null;
        }
        elem.m_Root.transform.position = postion;
        elem.m_AudioSource.clip = elem.m_AudioClip;
        elem.m_AudioSource.loop = isLoop;
        elem.m_bIsLoop = isLoop;
        elem.m_AudioSource.Play();
        elem.m_Root.name = resource;
        return elem;
    }
    private AudioElementStruct CreateElement(string resource, Transform parent, bool isLoop)
    {
        AudioElementStruct elem = null;
        if (m_AudioStorePool.Count > 0)
        {
            elem = m_AudioStorePool.Pop();
        }
        else
        {
            elem = new AudioElementStruct();
            elem.m_Root = new GameObject("AudioClip");
            ComponentTool.Attach(parent, elem.m_Root.transform);
            elem.m_AudioSource = elem.m_Root.AddComponent<AudioSource>();
        }

        elem.m_AudioClip = AssetManager.Instance.LoadAsset<AudioClip>(resource);
        if (elem.m_AudioClip == null)
        {
            return null;
        }
        elem.m_AudioSource.clip = elem.m_AudioClip;
        elem.m_AudioSource.loop = isLoop;
        elem.m_bIsLoop = isLoop;
        elem.m_AudioSource.Play();
        elem.m_Root.name = resource;
        return elem;
    }
    private void InitAudioPool()
    {
        for (int i = 0; i < m_iPoolSize; ++i)
        {
            AudioElementStruct elem = new AudioElementStruct();
            elem.m_AudioClip = null;
            elem.m_Root = new GameObject("AudioClip");
            ComponentTool.Attach(m_AudioRoot.transform, elem.m_Root.transform);
            elem.m_Root.transform.position = Vector3.zero;
            elem.m_AudioSource = elem.m_Root.AddComponent<AudioSource>();
            elem.m_AudioSource.clip = elem.m_AudioClip;
            elem.m_AudioSource.playOnAwake = false;
            m_AudioStorePool.Push(elem);
        }
    }
    private void CheckResource()
    {
        for (int i = 0; i < m_AudioClipList.Count;)
        {
            AudioElementStruct elem = m_AudioClipList[i];
            if(elem.m_Root == null)
            {
                m_AudioClipList.RemoveAt(i);
                continue;
            }
            if (!elem.m_bIsLoop && !elem.m_AudioSource.isPlaying && elem.m_OnFinishedCallBack == null)
            {
                CollectionAudio(elem);
                m_AudioClipList.RemoveAt(i);
            }
            else
            {
                ++i;
            }
        }
    }
    private void CollectionAudio(AudioElementStruct elem)
    {
        if(elem.m_Root==null)
        {
            return;
        }
        elem.m_AudioSource.Stop();
        elem.m_AudioSource.clip = null;
        if (elem.m_Root.transform.parent.gameObject != m_AudioRoot)
        {
            GameObject.Destroy(elem.m_Root);
        }
        else
        {
            if (m_AudioStorePool.Count > m_iPoolSize)
            {
                GameObject.Destroy(elem.m_Root);
            }
            else
            {
                elem.m_AudioClip = null;
                m_AudioStorePool.Push(elem);
            }
        }
    }
    public void Destructor()
    {
        for (int i = 0; i < m_AudioClipList.Count;++i )
        {
            CollectionAudio(m_AudioClipList[i]);
        }
        m_AudioClipList.Clear();
    }
    private void CheckCallBack(AudioElementStruct elem,Action<string> callBack)
    {
        if (null == callBack || null == elem)
        {
            return;
        }

        elem.m_OnFinishedCallBack = callBack;
        elem.m_fAudioLength = elem.m_AudioClip.length;
        elem.m_fCurrentPlayedLength = 0.0f;
    }
    private void CheckHavCallBackAudio()
    {
        bool hav = false;
        for (int i = 0; i < m_AudioClipList.Count;++i)
        {
            AudioElementStruct elem = m_AudioClipList[i];
            if (elem.m_OnFinishedCallBack != null)
            {
                hav = true;
                break;
            }
        }

        if (!hav)
        {
            Debug.Log("No audio call back,clear store");
        }
    }
    public void Update()
    {
        float deltaTime = Time.deltaTime;
        for (int i = 0; i < m_AudioClipList.Count; )
        {
            AudioElementStruct elem = m_AudioClipList[i];
            if (elem.m_OnFinishedCallBack == null)
            {
                ++i;
                continue;
            }
            elem.m_fCurrentPlayedLength += deltaTime;
            if (elem.m_fCurrentPlayedLength >= elem.m_fAudioLength)
            {
                //elem.m_OnFinishedCallBack(elem.m_Root.name);
                m_AudioCallBacktmpList.Add(new AudioCallBackElement(elem.m_OnFinishedCallBack, elem.m_Root.name));

                if (elem.m_bIsLoop)
                {
                    elem.m_fCurrentPlayedLength = elem.m_fCurrentPlayedLength - elem.m_fAudioLength;
                }
                else
                {
                    elem.m_fCurrentPlayedLength = 0.0f;
                }
                if (!elem.m_bIsLoop)
                {
                    elem.m_AudioSource.Stop();
                    elem.m_OnFinishedCallBack = null;
                    CollectionAudio(elem);
                    m_AudioClipList.Remove(elem);
                    CheckHavCallBackAudio();
                }
                else
                {
                    ++i;
                }
                
            }
            else
            {
                ++i;
            }
        }

        if (m_AudioCallBacktmpList.Count > 0)
        {
            //execute call back
            for (int i = 0; i < m_AudioCallBacktmpList.Count; ++i)
            {
                m_AudioCallBacktmpList[i].Execute();
            }
            m_AudioCallBacktmpList.Clear();
        }
        
    }
}
