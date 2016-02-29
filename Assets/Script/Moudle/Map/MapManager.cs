using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapManager 
{
    private GameObject m_SceneRoot;
    private GameObject m_WallRoot;
    private List<Wall> m_WallStore;

    static private MapManager m_Instance;
    static public MapManager GetInstance
    {
        get
        {
            if (null == m_Instance)
            {
                m_Instance = new MapManager();
            }
            return m_Instance;
        }
    }
    public void ResetSceneRoot(GameObject sceneRoot)
    {
        if (null != m_SceneRoot)
        {
            //destructor
            Destructor();
        }
        if (null != sceneRoot)
        {
            m_SceneRoot = sceneRoot;
            //initialize scene
            Initialize();
        }
    }
    public void Destructor()
    {
    }
    public List<SubPkg_BulletTrajectoryElement> CaculateBullet(Vector3 firePos, Vector3 fireSpeed, float fireTime, int countLimit)
    {
        List<SubPkg_BulletTrajectoryElement> resultList = new List<SubPkg_BulletTrajectoryElement>();
        
        Vector3 lastPos = firePos;
        Vector3 lastSpeed = fireSpeed.normalized;
        float lastTime = fireTime;
        float speedMagnitude = fireSpeed.magnitude;

        //init first element
        SubPkg_BulletTrajectoryElement elemFirst = new SubPkg_BulletTrajectoryElement();
        elemFirst.StartTime = fireTime;
        elemFirst.Speed = ComponentTool.ConvertVec3ToSubVec3(fireSpeed);
        elemFirst.Position = ComponentTool.ConvertVec3ToSubVec3(firePos);
        resultList.Add(elemFirst);

        //hide all other collider such as tank and reactive after caculate
        for(int i=0;i<countLimit;++i)
        {
            SubPkg_BulletTrajectoryElement elem = new SubPkg_BulletTrajectoryElement();
            RaycastHit hitInfo;
            Ray tmpRay = new Ray(lastPos, lastSpeed);
            if(Physics.Raycast(tmpRay,out hitInfo))
            {
                //hitInfo.point
                elem.Position = ComponentTool.ConvertVec3ToSubVec3(hitInfo.point);
                elem.StartTime = lastTime + (hitInfo.point - lastPos).magnitude / speedMagnitude;
                Vector3 newSpeed = lastSpeed.normalized + hitInfo.normal * 2.0f;

                //update last speed and last position
                lastPos = hitInfo.point;
                lastSpeed = newSpeed;
                lastTime = (float)(elem.StartTime);

                newSpeed = newSpeed.normalized * speedMagnitude;
                elem.Speed = ComponentTool.ConvertVec3ToSubVec3(newSpeed);

                resultList.Add(elem);
            }
            else
            {
                return null;
            }
        }
        return resultList;
    }
    private void Initialize()
    {
        //create wall
        m_WallRoot = ComponentTool.FindChild("WallRoot", m_SceneRoot);
        m_WallStore = new List<Wall>();
        if (m_WallRoot != null)
        {
            for (int i = 0; i < m_WallRoot.transform.childCount; ++i)
            {
                Wall elem = m_WallRoot.transform.GetChild(i).GetComponent<Wall>();
                if (elem != null)
                {
                    GameObject elemWall = GameObject.Instantiate(ResourceManager.LoadBuildInAsset(ResourceManager.GetWallIdByType(elem.WallType))) as GameObject;
                    ComponentTool.Attach(m_WallRoot.transform.GetChild(i), elemWall.transform);
                }
                else
                {
                    //
                    DebugLog.Log("Waring >>> scene wall need wall script !!!");
                }
            }
        }
        else
        {
            //
            DebugLog.Log("Waring >>> scene wall need wall root !!!");
        }
    }
}
