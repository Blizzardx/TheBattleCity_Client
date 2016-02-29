using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class BattleScene : SceneBase 
{
    public enum BattleSceneStatus
    {
        Waitting,
        Ready,
        Battle,
        Finished,
    }
    private BattleSceneStatus m_SceneStatus;
    private List<TankHandler>   m_PlayerTank;
    private List<GameObject>    m_PlayerObj;
    private GameObject          m_Scene;
    private Action              m_OnFinishedCallBack;
    private bool                m_bIsBulletReady;
    private bool                m_bIsLoadingScene;
    private static BattleScene  m_Instance;
    private AudioListener       m_UIAudioListener;

    static public BattleScene GetInstance
    {
        get
        {
            return m_Instance;
        }
    }
    override public void OpenScene(Action OnFinishedCallBack)
    {
        m_OnFinishedCallBack = OnFinishedCallBack;
        LoadResource();
    }
    override public void CloseScene()
    {
        for(int i=0;i<m_PlayerTank.Count;++i)
        {
            m_PlayerTank[i].Destructor();
        }
        BulletManager.Destructor();
        ExplosionManager.Destructor();

        //Reactive ui sound listener
        m_UIAudioListener.enabled = false;
    }
    public override void BasicUpdate()
    {
        if (m_bIsLoadingScene)
        {
            //wait for loading finished
            CheckLoading();
        }
        else
        {
            switch (m_SceneStatus)
            {
                case BattleSceneStatus.Waitting:
                    break;
                case BattleSceneStatus.Ready:
                    break;
                case BattleSceneStatus.Battle:
                    BattleUpdate();
                    break;
                case BattleSceneStatus.Finished:
                    break;
            }
        }
    }
    public void ChangeBattleSceneStatusTo(BattleSceneStatus newStatus)
    {
        if (newStatus == m_SceneStatus)
        {
            return;
        }
        m_SceneStatus = newStatus;
        switch (newStatus)
        {
            case BattleSceneStatus.Waitting:
                break;
            case BattleSceneStatus.Ready:
                break;
            case BattleSceneStatus.Battle:
                //set background music
                AudioManager.GetInstance.PlayBackgroundSound(BackgroundAudioDefine.AudioBackgroundSoundType.Battle_0);
                break;
            case BattleSceneStatus.Finished:
                break;
        }
    }
    private void LoadResource()
    {
        m_bIsLoadingScene = true;
        m_bIsBulletReady = false;

        //initialize scene
        BulletManager.GetInstance.Initialize();
        ExplosionManager.GetInstance.Initialize();

        m_PlayerObj = new List<GameObject>();
        m_PlayerTank = new List<TankHandler>();

        //load scene
        ResourceManager.LoadAssetsAsync(ResourceManager.GetSceneIdByType(0), (UnityEngine.Object source) =>
        {
            m_Scene = GameObject.Instantiate(source) as GameObject;
        });

        //load bullet
        List<int> BulletPrepareStore = new List<int>();
        BulletPrepareStore.Add(ResourceManager.GetBulletAssetsIdByType(0));
        BulletPrepareStore.Add(ResourceManager.GetBulletLightAssetsIdByType(0));
        for (int i = 0; i < 5; ++i)
        {
            BulletPrepareStore.Add(ResourceManager.GetBulletTextureAssetsIdByBulletTextureType(i));
            BulletPrepareStore.Add(ResourceManager.GetBulletLightTextureAssetsIdByBulletTextureType(i));
        }
        BulletManager.GetInstance.PreLoadAssets(BulletPrepareStore, () =>
        {
            m_bIsBulletReady = true;
        });
    }
    private void InitializeScene()
    {
        //initialize scene
        MapManager.GetInstance.ResetSceneRoot(m_Scene);

        //create handle icon
        GameObject handleIcon = GameObject.Instantiate(ResourceManager.LoadBuildInUIPrefab("Common/Handler")) as GameObject;
        ComponentTool.Attach(WindowManager.GetInstance.GetUIWindowRoot().transform, handleIcon.transform);

        //disable ui sound listener
        m_UIAudioListener = GameObject.Find("AudioRoot").GetComponent<AudioListener>();
        m_UIAudioListener.enabled = false;

        //set background music
        AudioManager.GetInstance.PlayBackgroundSound(BackgroundAudioDefine.AudioBackgroundSoundType.WaitForBattle);
        
        //open battle window
        WindowManager.GetInstance.OpenWindow(WindowType.Battle);

        ClientMessageCenter.GetInstance.RegistMessage(MessageID.STC_RoomDetail, OnUpdatePlayerPos);
        //
        ResetPlayerPosData();

        //finished call back
        m_OnFinishedCallBack();
    }
    private void CheckLoading()
    {
        if (m_Scene != null && m_bIsBulletReady)
        {
            //initialize scene
            InitializeScene();

            m_bIsLoadingScene = false;
        }
        else
        {
            m_bIsLoadingScene = true;
        }
    }
    private void Awake()
    {
        m_Instance = this;
    }
    private void BattleUpdate()
    {
        foreach( TankHandler elem in m_PlayerTank)
        {
            elem.FixedUpdate();
        }
        BulletManager.GetInstance.FixedUpdate();
    }
    private void OnUpdatePlayerPos(MessageObject eb)
    {
        PlayerData.GetInstance.BattleRoomDetail = (eb.msgValue as PkgStoC_RoomDetail).RoomDetailList;
        for (int i = 0; i < m_PlayerObj.Count; ++i)
        {
            GameObject.Destroy(m_PlayerObj[i]);
        }
        m_PlayerObj.Clear();
        m_PlayerTank.Clear();

        ResetPlayerPosData();
    }
    private void ResetPlayerPosData()
    {
        for (int i = 0; i < PlayerData.GetInstance.BattleRoomDetail.Count; ++i)
        {
            SubPkg_RoomDetail elem = PlayerData.GetInstance.BattleRoomDetail[i];
            //load player
            UnityEngine.Object tank = ResourceManager.LoadBuildInAsset(ResourceManager.GetTankByType(elem.MeshId));
            GameObject tmpPlayer = GameObject.Instantiate(tank) as GameObject;
            TankHandler tmpHandler = new TankHandler();

            ComponentTool.Attach(
                ComponentTool.FindChildComponent<Transform>("PlayerPos_" + elem.Position.ToString(), m_Scene),
                tmpPlayer.transform);


            if (PlayerData.GetInstance.PlayerName == elem.PlayerName)
            {
                tmpHandler.Initialize(elem.PlayerName,tmpPlayer, InputType.Device, elem.MeshTextureId);
            }
            else
            {
                tmpHandler.Initialize(elem.PlayerName,tmpPlayer, InputType.NetWork, elem.MeshTextureId);
            }
            m_PlayerObj.Add(tmpPlayer);
            m_PlayerTank.Add(tmpHandler);
        }
    }
}
