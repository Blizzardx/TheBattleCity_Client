using System;
using UnityEngine;
using System.Collections;

public class TipManager : Singleton<TipManager>
{
    private UIWindowAlert m_AlertWindow;

    #region system function

    private void TryInitAlert()
    {
        WindowManager.Instance.OpenWindow(WindowID.Alert);
        m_AlertWindow = WindowManager.Instance.GetWindow(WindowID.Alert) as UIWindowAlert;
    }

    #endregion

    #region alert
    public void Alert(string content)
    {
        TryInitAlert();
        m_AlertWindow.Alert(content);
    }
    public void Alert(string title, string content)
    {
        TryInitAlert();
        m_AlertWindow.Alert(title,content);
    }
    public void Alert(string title, string content, string correct, Action<bool> callBack)
    {
        TryInitAlert();
        m_AlertWindow.Alert(title,content,correct,callBack);
    }
    public void Alert(string title, string content, string correct, string cancle, Action<bool> callBack)
    {
        TryInitAlert();
        m_AlertWindow.Alert(title,content,correct,cancle,callBack);
    }
    public void CloseAlert()
    {
        if (null != m_AlertWindow)
        {
            m_AlertWindow.Close();
        }
    }
    #endregion

    #region text tip
    public void Tip(string content,LabelFontSizeDefine fontSize = LabelFontSizeDefine.LABEL_SIZE_25,CSS.Styles fontStyle = CSS.Styles.Style_Green_LineBlack)
    {
        
    }
    #endregion

    #region roll tip
    public void RollTip(string content)
    {
        
    }
    #endregion

    #region tip
    #endregion
}
