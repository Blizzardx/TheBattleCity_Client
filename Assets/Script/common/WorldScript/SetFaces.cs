using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SetFaces : MultiTex
{
    public static Dictionary<string, int> faceTable;
    public void SetFace(string faceName)
    {
        if (faceTable == null)
            faceTable = new Dictionary<string, int> {
            { "zhengchang",0 },
            { "jingya",1 },
            { "xiee",2},
            { "shengqi",3 },
            { "jifen",4 },
            { "haipa",5 },
            { "guilian",6 },
            { "congbai",7 } };
        if (faceTable.ContainsKey(faceName))
            SetTex(faceTable[faceName]);
    }
    void Start()
    {
        //base.Init(face);
    }
#if UNITY_EDITOR
    void Update()
    {
        if(Input.anyKey)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                SetFace("zhengchang");
            if (Input.GetKeyDown(KeyCode.Alpha2))
                SetFace("jingya");
            if (Input.GetKeyDown(KeyCode.Alpha3))
                SetFace("xiee");
            if (Input.GetKeyDown(KeyCode.Alpha4))
                SetFace("shengqi");
        }
    }
#endif
}
