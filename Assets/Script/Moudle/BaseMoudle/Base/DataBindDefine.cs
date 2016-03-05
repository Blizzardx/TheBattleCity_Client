using UnityEngine;
using System.Collections;

public enum DataBindKey
{
    Player_Exp,
}
public enum DataBindType
{
    Other,
    Label,
}
public class DataBindDefine : Singleton<DataBindDefine>
{
    public void RegisterBindMessageHandler()
    {
        //DataBindManager.Instance.RegisterMsgHandler(MessageTypeConstants.SC_CHARACTER_INFO,CharacterInfoHandler);
    }
    public void RegisterBindTypeHandler()
    {
        DataBindManager.Instance.RegisterTypeHandler(DataBindType.Label,LabelHandler);
    }
    #region message handler

    private void CharacterInfoHandler(object msgValue)
    {/*
        SCCharacterInfoMsg msg = msgValue as SCCharacterInfoMsg;
        DataBindManager.Instance.BindExcution(DataBindKey.Player_Exp,msg.Exp);*/
    }
    #endregion

    #region type handler

    public void LabelHandler(object listener,object value)
    {
        UILabel label = listener as UILabel;
        label.text = value.ToString();
    }
    #endregion
}
