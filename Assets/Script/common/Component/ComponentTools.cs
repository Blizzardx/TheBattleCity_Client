using UnityEngine;
using System.Collections;

public class ComponentTool 
{
    public static readonly ComponentTool Instance = new ComponentTool();
    private ComponentTool()
    {

    }
    public static void Attach(Transform root,Transform child)
    {
        child.transform.parent = root;
        child.localPosition = new Vector3(0, 0, 0);
        child.localScale = new Vector3(1, 1, 1);
        child.localEulerAngles = new Vector3(0, 0, 0);
    }
    public static void SetSubPkgVector3(SubPkg_Vector3 target,Vector3 source)
    {
        target.X = source.x;
        target.Y = source.y;
        target.Z = source.z;
    }
    public static Vector3 ConvertSubVec3ToVector3(SubPkg_Vector3 target)
    {
        return new Vector3((float)(target.X), (float)(target.Y), (float)(target.Z));
    }
    public static SubPkg_Vector3 ConvertVec3ToSubVec3(Vector3 target)
    {
        SubPkg_Vector3 res = new SubPkg_Vector3();
        SetSubPkgVector3(res, target);
        return res;
    }
    public static T FindChildComponent<T>(string objName, GameObject fromParent) where T : Component
    {
        GameObject obj = FindChild(objName, fromParent);
        if (null != obj)
        {
            return obj.GetComponent<T>();
        }
        else
        {
            return null;
        }
    }
    public static GameObject FindChild(string objName, GameObject fromParent)
    {
        if( null == fromParent)
        {
            return null;
        }
        GameObject parent = fromParent;
        Transform child = FindChild(parent.transform, objName);
        if (null != child)
        {
            return child.gameObject;
        }
        else
        {
            return null;
        }
    }
    private static Transform FindChild(Transform parent, string objName)
    {
        if (parent.name == objName)
        {
            return parent;
        }
        else
        {
            foreach (Transform item in parent)
            {
                Transform child = FindChild(item, objName);
                if (null != child)
                {
                    return child;
                }
            }
            return null;
        }
    } 
}
