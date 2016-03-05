using System;
using UnityEngine;
using System.Collections.Generic;


public class StageManager : Singleton<StageManager>
{
    private StageBase                               m_CurrentStage;
    private Dictionary<GameStateType, string>       m_StageSceneStore;
    private Dictionary<GameStateType, StageBase>    m_StageHandlerStore;
    private Dictionary<GameStateType, Type>         m_StageHandlerFactoryStore;

 
    public void Initialize()
    {
        m_StageSceneStore = new Dictionary<GameStateType, string>();
        m_StageHandlerStore = new Dictionary<GameStateType, StageBase>();
        m_StageHandlerFactoryStore = new Dictionary<GameStateType, Type>();

        Definer.RegisterStage();
    }
    public void Destructor()
    {
        foreach (var elem in m_StageHandlerStore)
        {
            //elem.Value.EndStage();
        }
    }
    public void RegisterStage(GameStateType type, string sceneName,Type logicHandlerType)
    {
        m_StageSceneStore.Add(type, sceneName);
        m_StageHandlerFactoryStore.Add(type, logicHandlerType);
    }
    public void ChangeState(GameStateType pState)
    {
        if (SceneManager.Instance.IsSceneLoadiing())
        {
            Debuger.Log("System busy");
            return;
        }
        if (null != m_CurrentStage)
        {
            m_CurrentStage.EndStage();
        }
        m_CurrentStage = null;
        if (!m_StageHandlerStore.TryGetValue(pState, out m_CurrentStage))
        {
            m_CurrentStage = Activator.CreateInstance(m_StageHandlerFactoryStore[pState],pState) as StageBase;
            m_StageHandlerStore.Add(pState, m_CurrentStage);
        }
        Time.timeScale = 1.0f;

        //send msg
        MessageManager.Instance.AddToMessageQueue(new MessageObject(ClientCustomMessageDefine.C_CHANGE_SCENE, null));

        //load scene
        SceneManager.Instance.LoadScene(m_StageSceneStore[pState], m_CurrentStage.StartStage,m_CurrentStage.PreLoadScene,m_CurrentStage.InitStage);
    }
    public GameStateType GetCurrentGameStage()
    {
        if (null != m_CurrentStage)
        {
            return m_CurrentStage.StageType;
        }
        return GameStateType.none;
    }
}
