using UnityEngine;
using System.Collections;
using System;

public class Button : MonoBehaviour
{
    public enum ButtonType
    {
        Normal,
        Press,
    }
    public ButtonType   m_ButtonType;
    public string       m_LabelText;
    public string       m_NormalSkin = "Button_Normal";
    public string       m_PressedSkin = "Button_HighLight";
    public string       m_DisableSkin = "Button_Normal";
    public UILabel      m_Label;
    public UISprite     m_SpriteBg;
    private bool        m_bIsEnable;
    private bool        m_bIsSelected;
    private Action      m_CallBack;

    // Use this for initialization
    void Start()
    {
        if (m_ButtonType == ButtonType.Normal)
        {
            if (null == m_Label)
            {
                m_Label = ComponentTool.FindChildComponent<UILabel>("Label", gameObject);
            }
            if (null == m_SpriteBg)
            {
                m_SpriteBg = ComponentTool.FindChildComponent<UISprite>("Background", gameObject);
            }
        }
        else
        {
            // do nothing
        }
        m_bIsEnable = true;
        Refresh();
    }
    void OnEnable()
    {
        Refresh();
    }
    void OnPress(bool pressed)
    {
        if (m_bIsEnable)
        {
            m_bIsSelected = pressed;
        }
        Refresh();
    }
    void OnClick()
    {
        DoCallBack();
    }
    void DoCallBack()
    {
        if (m_CallBack != null)
        {
            m_CallBack();
        }
    }
    public void SetEnable(bool statue)
    {
        m_bIsEnable = statue;
        Refresh();
    }
    public void AddCallBack(Action callBack)
    {
        m_CallBack += callBack;
    }
    public void RemoveCallBack(Action callBack)
    {
        m_CallBack -= callBack;
    }
    public void Refresh()
    {
        if (m_ButtonType == ButtonType.Normal)
        {
            m_Label.text = m_LabelText;
            if (m_bIsSelected)
            {
                m_SpriteBg.spriteName = m_PressedSkin;
            }
            else if (m_bIsEnable)
            {
                m_SpriteBg.spriteName = m_NormalSkin;
            }
            else
            {
                m_SpriteBg.spriteName = m_DisableSkin;
            }
        }
    }
}
