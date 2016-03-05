using Common.Auto;
using UnityEngine;
using System.Collections.Generic;

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
    public static T FindChildComponent<T>(string objName, GameObject fromParent) where T : Component
    {
        GameObject obj = FindChild(objName, fromParent);
        if (null != obj)
        {
            return obj.GetComponent<T>();
        }
        else
        {
            Debuger.LogWarning("can't load component : " + objName);
            return null;
        }
    }
    public static GameObject FindChild(string objName, GameObject fromParent)
    {
        if( null == fromParent)
        {
            return GameObject.Find(objName);
        }
        GameObject parent = fromParent;
        Transform child = FindChild(parent.transform, objName);
        if (null != child)
        {
            return child.gameObject;
        }
        else
        {
            Debuger.LogWarning("can't load gameObject : " + objName);
            return null;
        }
    }
    public static void FindAllChildComponents<T>(Transform parent, ref List<T> result) where T : Component
    {
        T elem = parent.GetComponent<T>();
        if (null != elem)
        {
            result.Add(elem);
        }
        for (int i = 0; i < parent.childCount; ++i)
        {
            FindAllChildComponents<T>(parent.GetChild(i), ref result);
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
    public static bool IsInRect(UIWidget des, UIWidget source,float scale = 1.0f)
    {
        float width = source.width * scale;
        float height = source.height * scale;

        if (des.transform.position.x > source.transform.position.x - width / 2 &&
            des.transform.position.x < source.transform.position.x + width / 2 &&
            des.transform.position.y > source.transform.position.y - height / 2 &&
            des.transform.position.y < source.transform.position.y + height / 2)
        {
            return true;
        }
        return false;
    }
    public static bool IsRectCross(UIWidget des, UIWidget source, float scale = 1.0f)
    {
        float width = source.width * scale;
        float height = source.height * scale;
        float desWidth = des.width * scale;
        float desHeight = des.height * scale;

        if (des.transform.position.x + desWidth / 2 > source.transform.position.x - width / 2 &&
            des.transform.position.x - desWidth / 2 < source.transform.position.x + width / 2 &&
            des.transform.position.y + desHeight / 2 > source.transform.position.y - height / 2 &&
            des.transform.position.y - desHeight / 2 < source.transform.position.y + height / 2)
        {
            return true;
        }
        return false;
    }
}
