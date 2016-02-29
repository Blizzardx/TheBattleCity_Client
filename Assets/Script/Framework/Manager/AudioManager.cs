using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using System;

    /// <summary>
    /// Audio manager
    /// </summary>
public class AudioManager
{
    //private members
    private GameObject                                                                  m_AudioSourceRoot;
    private Dictionary<UISoundAudioDefine.AudioUISoundType, AudioClip>                  m_UISoundClipStore;
    private Dictionary<EffectAudioDefine.AudioEffectSoundType, AudioClip>               m_EffectSoundClipStore;
    private Dictionary<BackgroundAudioDefine.AudioBackgroundSoundType, AudioClip>       m_BackgroundSoundClipStore;
    private List<AudioSource>                                                           m_EffectAudioSourceStore;
    private List<AudioSource>                                                           m_UIAudioSourceStore;
    private List<BackgroundAudioDefine.AudioBackgroundSoundType>                        m_BattleBgMusicStore; //use for loop play when fighting
    private AudioSource                                                                 m_BgAudioSource;
    private float                                                                       m_fUIVolume;
    private float                                                                       m_fEffectVolume;
    private float                                                                       m_fBgVolume;
    private bool                                                                        m_bIsMuteEffect;
    private bool                                                                        m_bIsMuteUI;
    private bool                                                                        m_bIsMuteBg;
    private int                                                                         m_StoreSizeMax      = 5;
    private string                                                                      m_strUIMuteTag      = "UIMute";     // 0-mute 1-don't mute
    private string                                                                      m_strBgMuteTag      = "BgMute";     // 0-mute 1-don't mute
    private string                                                                      m_strEffectMuteTag  = "EffectMute"; // 0-mute 1-don't mute
    private bool                                                                        m_bIsRandoming      = false;
    private bool                                                                        m_bIsLoadingNextBgMusic;
    static private AudioManager                                                         m_Instance          = null;

    //public interface
    public static AudioManager GetInstance
    {
        get
        {
            if (null == m_Instance)
            {
                m_Instance = new AudioManager();
            }
            return m_Instance;
        }
    }
    public void SetUIVolume(float volume)
    {
        m_fUIVolume = volume;
        ClearAudioStore();
        foreach (AudioSource elem in m_UIAudioSourceStore)
        {
            elem.volume = m_fUIVolume;
        }
    }
    public void SetEffectVolume(float volume)
    {
        m_fEffectVolume = volume;
        ClearAudioStore();
        foreach (AudioSource elem in m_EffectAudioSourceStore)
        {
            elem.volume = m_fEffectVolume;
        }
    }
    public void SetBgVolume(float volume)
    {
        m_fBgVolume = volume;
        m_BgAudioSource.volume = m_fBgVolume;
    }
    public void MuteAll(bool status)
    {
        MuteEffectSound(status);
        MuteUISound(status);
        MuteBackgroundSound(status);
    }
    public void MuteEffectSound(bool status)
    {
        m_bIsMuteEffect = status;
        ClearAudioStore();
        foreach (AudioSource elem in m_EffectAudioSourceStore)
        {
            elem.mute = status;
        }
        PlayerPrefs.SetInt(m_strEffectMuteTag, m_bIsMuteEffect ? 2 : 1);
    }
    public void MuteUISound(bool status)
    {
        m_bIsMuteUI = status;
        ClearAudioStore();
        foreach (AudioSource elem in m_UIAudioSourceStore)
        {
            elem.mute = status;
        }
        PlayerPrefs.SetInt(m_strUIMuteTag, m_bIsMuteUI ? 2 : 1);
    }
    public void MuteBackgroundSound(bool status)
    {
        m_bIsMuteBg = status;
        m_BgAudioSource.mute = status;
        PlayerPrefs.SetInt(m_strBgMuteTag, m_bIsMuteBg ? 2 : 1);
    }
    public void PlayUISound(UISoundAudioDefine.AudioUISoundType type)
    {
        if (m_bIsMuteUI)
        {
            return;
        }

        GetAudioClipByType(type, (AudioClip tmpClip) => 
        {
            if (null != tmpClip)
            {
                AudioPlayUISound(tmpClip);
            }
        });
        
    }
    public void PlayEffectSound(EffectAudioDefine.AudioEffectSoundType type,Vector3 targetRoot)
    {
        if (m_bIsMuteEffect)
        {
            return;
        }

        GetAudioClipByType(type, (AudioClip clip) =>
        {
            if (null != clip)
            {
                AudioPlayEffectSound(clip, targetRoot);
            }
        });
        
    }
    public void PlayBackgroundSound(BackgroundAudioDefine.AudioBackgroundSoundType type)
    {
        m_bIsLoadingNextBgMusic = true;

        GetAudioClipByType(type, (AudioClip tmpClip) =>
        {
            if (type == BackgroundAudioDefine.AudioBackgroundSoundType.Battle_0 ||
                type == BackgroundAudioDefine.AudioBackgroundSoundType.Battle_1 ||
                type == BackgroundAudioDefine.AudioBackgroundSoundType.Battle_2 ||
                type == BackgroundAudioDefine.AudioBackgroundSoundType.Battle_3 ||
                type == BackgroundAudioDefine.AudioBackgroundSoundType.Battle_4
                )
            {
                m_bIsRandoming = true;
            }
            else
            {
                m_bIsRandoming = false;
            }
            m_bIsLoadingNextBgMusic = false;
            if (null != tmpClip)
            {
                AudioPlayBg(tmpClip);
            }
        });
        
    }
    public bool GetIsMuteEffect()
    {
        return m_bIsMuteEffect;
    }
    public bool GetIsMuteUI()
    {
        return m_bIsMuteUI;
    }
    public bool GetIsMuteBg()
    {
        return m_bIsMuteBg;
    }
    public void ClearAudioStore()
    {
        for (int i = 0; i < m_EffectAudioSourceStore.Count; )
        {
            if (!m_EffectAudioSourceStore[i].isPlaying)
            {
                AudioSource tmp = m_EffectAudioSourceStore[i];
                m_EffectAudioSourceStore.RemoveAt(i);
                GameObject.Destroy(tmp.gameObject);
            }
            else
            {
                ++i;
            }
        }
        for (int i = 0; i < m_UIAudioSourceStore.Count; )
        {
            if (!m_UIAudioSourceStore[i].isPlaying)
            {
                AudioSource tmp = m_UIAudioSourceStore[i];
                m_UIAudioSourceStore.RemoveAt(i);
                GameObject.Destroy(tmp.gameObject);
            }
            else
            {
                ++i;
            }
        }
    }
    public void BasicUpdate()
    {
        if (m_bIsRandoming && m_BgAudioSource != null && !m_BgAudioSource.isPlaying && !m_bIsLoadingNextBgMusic)
        {
            //random play
            BackgroundAudioDefine.AudioBackgroundSoundType newBg = m_BattleBgMusicStore[UnityEngine.Random.Range(0, m_BattleBgMusicStore.Count)];
            PlayBackgroundSound(newBg);
        }
    }
    //system property
    private AudioManager()
    {
    }
    public void Initialize()
    {
        m_fUIVolume     = 1.0f; //default volume 100%
        m_fEffectVolume = 1.0f; //default volume 100%
        m_fBgVolume     = 0.6f; //default volume 60%

        m_AudioSourceRoot = GameObject.FindGameObjectWithTag("AudioPlayer");
        m_UISoundClipStore = new Dictionary<UISoundAudioDefine.AudioUISoundType, AudioClip>();
        m_EffectSoundClipStore = new Dictionary<EffectAudioDefine.AudioEffectSoundType, AudioClip>();
        m_BackgroundSoundClipStore = new Dictionary<BackgroundAudioDefine.AudioBackgroundSoundType, AudioClip>();
        m_UIAudioSourceStore = new List<AudioSource>();
        m_EffectAudioSourceStore = new List<AudioSource>();
        m_BattleBgMusicStore = new List<BackgroundAudioDefine.AudioBackgroundSoundType>();

        //set battle bg music
        m_BattleBgMusicStore.Add(BackgroundAudioDefine.AudioBackgroundSoundType.Battle_0);
        m_BattleBgMusicStore.Add(BackgroundAudioDefine.AudioBackgroundSoundType.Battle_1);
        m_BattleBgMusicStore.Add(BackgroundAudioDefine.AudioBackgroundSoundType.Battle_2);
        m_BattleBgMusicStore.Add(BackgroundAudioDefine.AudioBackgroundSoundType.Battle_3);
        m_BattleBgMusicStore.Add(BackgroundAudioDefine.AudioBackgroundSoundType.Battle_4);

        //add background audio source
        GameObject tmp = new GameObject();
        tmp.transform.parent = m_AudioSourceRoot.transform;
        tmp.name = "BackgroundAudioSource";
        tmp.transform.localPosition = Vector3.zero;
        m_BgAudioSource = tmp.AddComponent<AudioSource>();
        m_BgAudioSource.volume = m_fBgVolume;

        bool tmpIsMuteUI = PlayerPrefs.GetInt(m_strUIMuteTag, 1) == 0;
        bool tmpIsMuteBg = PlayerPrefs.GetInt(m_strBgMuteTag, 1) == 0;
        bool tmpIsMuteEffect = PlayerPrefs.GetInt(m_strEffectMuteTag, 1) == 0;

        //execution sound setting
        MuteUISound(tmpIsMuteUI);
        MuteEffectSound(tmpIsMuteEffect);
        MuteBackgroundSound(tmpIsMuteBg);

        m_bIsRandoming          = false;
        m_bIsLoadingNextBgMusic = false;
    }
    public void Destructor()
    {
        for (int i = 0; i < m_EffectAudioSourceStore.Count; ++i)
        {
            GameObject.Destroy(m_EffectAudioSourceStore[i]);
        }
        for (int i = 0; i < m_UIAudioSourceStore.Count; ++i)
        {
            GameObject.Destroy(m_UIAudioSourceStore[i]);
        }
        m_EffectAudioSourceStore.Clear();
        m_UIAudioSourceStore.Clear();
        m_BgAudioSource.Stop();
    }
    private void GetAudioClipByType(UISoundAudioDefine.AudioUISoundType type,Action<AudioClip> CallBack)
    {
        if (m_UISoundClipStore.ContainsKey(type))
        {
            CallBack(m_UISoundClipStore[type]);
        }
        else
        {
            LoadClip(GetAssetsIdByType(type),(UnityEngine.Object clip)=>
            {
                AudioClip tmpElem = clip as AudioClip;
                if (null != tmpElem)
                {
                    m_UISoundClipStore.Add(type, tmpElem);
                    CallBack(tmpElem);
                }
                else
                {
                    // can't load audio resource
                    CallBack(null);
                    Debug.LogError("Can't load audio clip named: " + type.ToString());
                }
            });
            
        }
    }
    private void GetAudioClipByType(BackgroundAudioDefine.AudioBackgroundSoundType type, Action<AudioClip> CallBack)
    {
        if (m_BackgroundSoundClipStore.ContainsKey(type))
        {
            CallBack(m_BackgroundSoundClipStore[type]);
        }
        else
        {
            LoadClip(GetAssetsIdByType(type), (UnityEngine.Object clip) => 
            {
                AudioClip tmpElem = clip as AudioClip; 
                if (null != tmpElem)
                {
                    m_BackgroundSoundClipStore.Add(type, tmpElem);
                    CallBack(tmpElem);
                }
                else
                {
                    // can't load audio resource
                    CallBack(null);
                    Debug.LogError("Can't load audio clip named: " + type.ToString());
                }
            });            
        }
    }
    private void GetAudioClipByType(EffectAudioDefine.AudioEffectSoundType type, Action<AudioClip> CallBack)
    {
        if (m_EffectSoundClipStore.ContainsKey(type))
        {
            CallBack(m_EffectSoundClipStore[type]);
        }
        else
        {
            LoadClip(GetAssetsIdByType(type),(UnityEngine.Object clip)=>
            {
                AudioClip tmpElem = clip as AudioClip;
                if (null != tmpElem)
                {
                    m_EffectSoundClipStore.Add(type, tmpElem);
                    CallBack(tmpElem);
                }
                else
                {
                    // can't load audio resource
                    CallBack(null);
                    Debug.LogError("Can't load audio clip named: " + type.ToString());
                }
            });
            
        }
    }
    private void LoadClip(string assetsId,Action<UnityEngine.Object> callBack)
    {
        ResourceManager.LoadAssetsAsync(Convert.ToInt32(assetsId), callBack);
    }
    private void AudioPlayBg(AudioClip clip)
    {
        if (m_BgAudioSource.clip != clip)
        {
            m_BgAudioSource.clip = clip;
            m_BgAudioSource.loop = !m_bIsRandoming;
            m_BgAudioSource.Play();
        }
    }
    private void AudioPlayUISound(AudioClip clip)
    {
        if (m_UIAudioSourceStore.Count >= m_StoreSizeMax)
        {
            ClearAudioStore();
        }
        GameObject tmp = new GameObject();
        tmp.transform.parent = m_AudioSourceRoot.transform;
        tmp.name = "AudioSource";
        tmp.transform.localPosition = Vector3.zero;
        AudioSource tmpSource = tmp.AddComponent<AudioSource>();
        tmpSource.clip = clip;
        tmpSource.volume = m_fUIVolume;
        tmpSource.Play();
        m_UIAudioSourceStore.Add(tmpSource);
    }
    private void AudioPlayEffectSound(AudioClip clip, Vector3 targetRoot)
    {
        if (m_EffectAudioSourceStore.Count >= m_StoreSizeMax)
        {
            ClearAudioStore();
        }
        GameObject tmp = new GameObject();
        tmp.transform.position = targetRoot;
        tmp.name = "AudioSource";
        AudioSource tmpSource = tmp.AddComponent<AudioSource>();
        tmpSource.clip = clip;
        tmpSource.volume = m_fEffectVolume;
        tmpSource.Play();
        m_EffectAudioSourceStore.Add(tmpSource);
    }
    public string GetAssetsIdByType(BackgroundAudioDefine.AudioBackgroundSoundType type)
    {
        FieldInfo myFields = BackgroundAudioDefine.Singleton.GetType()
            .GetField(type.ToString(), BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance);
        return (string)myFields.GetValue(BackgroundAudioDefine.Singleton);
    }
    public string GetAssetsIdByType(EffectAudioDefine.AudioEffectSoundType type)
    {
        FieldInfo myFields = EffectAudioDefine.Singleton.GetType()
            .GetField(type.ToString(), BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance);
        return (string)myFields.GetValue(EffectAudioDefine.Singleton);
    }
    public string GetAssetsIdByType(UISoundAudioDefine.AudioUISoundType type)
    {
        FieldInfo myFields = UISoundAudioDefine.Singleton.GetType()
            .GetField(type.ToString(), BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance);
        return (string)myFields.GetValue(UISoundAudioDefine.Singleton);
    }
}
