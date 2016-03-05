using UnityEngine;
using System.Collections;

public class UISoundPlus : MonoBehaviour
{
    //public AudioId m_AudioId;

    void Start()
    {
        UIEventListener.Get(gameObject).onClick += this.OnClick;
        Debuger.Log("initialize ui sound");
    }
    private void OnClick(GameObject go)
    {
        Debuger.Log("On trigger enter");
        //AudioPlayer.Instance.PlayUISound(m_AudioId);
    }

}
