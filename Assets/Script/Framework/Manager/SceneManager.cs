using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum SceneType
{
    Initialize,
    MainMenu,
    Battle,
}
public class SceneManager : MonoBehaviour
{
    private static SceneManager m_Instance;
    public static SceneManager GetInstance
    {
        get
        {
            if( null == m_Instance )
            {
                m_Instance = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SceneManager>();
            }
            return m_Instance;
        }
    }

    private Dictionary<SceneType, string>   m_SceneNameIndexStore;
    private SceneType                       m_CurrentSceneStatus;
    private SceneBase                       m_CurrentScene;
    private AsyncOperation                  m_SceneAsync;
    private bool                            m_bIsShowLoadingPanel;
    private float                           m_fCurrentLoadingSpendTime;
    private readonly float                  m_fLoadSceneTimeOut         = 180.0f; //set 3 min to loading scene time out
    private GameObject                      m_LoadingPanel;
    public void Initialize()
    {
        m_SceneNameIndexStore = new Dictionary<SceneType, string>();
        m_SceneNameIndexStore.Add(SceneType.MainMenu, "MainMenu");
        m_SceneNameIndexStore.Add(SceneType.Battle, "Battle");
        //reset data
        ResetData();
    }
    public void ChangeSceneTo(SceneType newScene)
    {
        if( newScene == m_CurrentSceneStatus || !m_SceneNameIndexStore.ContainsKey(newScene))
        {
            return;
        }
        //reset scene tag
        m_CurrentSceneStatus = newScene;
        if (null != m_CurrentScene)
        {
            //close current scene
            m_CurrentScene.CloseScene();
        }
        //show loading panel
        ShowLoadPanel(true);
        SetLoadStatus(true);
        
        //clear data
        ClearData();

        //start loading new scene
        StartCoroutine(loadScene());
    }
    private IEnumerator loadScene()
    {
        m_SceneAsync = Application.LoadLevelAsync(m_SceneNameIndexStore[m_CurrentSceneStatus]);
        yield return m_SceneAsync;

        //reset current scene reference
        m_CurrentScene = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneBase>();

        //open scene 
        m_CurrentScene.OpenScene(() =>
        {
            StartCoroutine(OnCloseLoadingPanel());
        });
        SetLoadStatus(false);
    }
    private IEnumerator OnCloseLoadingPanel()
    {
        yield return new WaitForSeconds(1f);

        //close loading panel
        ShowLoadPanel(false);
    }
    private void ShowLoadPanel(bool status)
    {
        //show loading panel
        if (m_LoadingPanel == null)
        {
            m_LoadingPanel = GameObject.Instantiate(ResourceManager.LoadBuildInUIPrefab("Loading/LoadingPanel")) as GameObject;
            ComponentTool.Attach(WindowManager.GetInstance.GetUIWindowRoot().transform, m_LoadingPanel.transform);
        }
        m_LoadingPanel.SetActive(status);

        //reset loading time
        m_fCurrentLoadingSpendTime = 0.0f;
    }
    private void SetLoadStatus(bool status)
    {
        m_bIsShowLoadingPanel = status;
    }
    private void ResetData()
    {
        m_CurrentScene = null;
        m_CurrentSceneStatus = SceneType.Initialize;
    }
    private void RestoreScene()
    {
        Application.LoadLevel(m_SceneNameIndexStore[SceneType.MainMenu]);
    }
    public void BasicUpdate()
    {
        if (m_bIsShowLoadingPanel)
        {
            m_fCurrentLoadingSpendTime += Time.fixedDeltaTime;
            if( m_fCurrentLoadingSpendTime > m_fLoadSceneTimeOut )
            {
                //stop loading
                StopCoroutine(loadScene());
                //reset data
                ResetData();
                //restore scene to login
                RestoreScene();
                //set loading status to false
                ShowLoadPanel(false);
                SetLoadStatus(false);
                //tip message
            }
        }
        else
        {
            m_CurrentScene.BasicUpdate();
        }
    }   
    private void ClearData()
    {
        WindowManager.GetInstance.ClearUsingPool();
        //collecting
        Resources.UnloadUnusedAssets();
        System.GC.Collect();
    }
}
