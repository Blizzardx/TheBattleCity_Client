using UnityEngine;
using System;
using System.Collections.Generic;

public class MultiTex : MonoBehaviour
{
    public int TexLength
    {
        get { return (int)(num.x * num.y); }
    }
    [HeaderAttribute("XY为切分数量")]
    [SerializeField]
    Vector2 num;
    [SerializeField]
    bool isShare = false;
    [SerializeField]
    protected Material _mat;
    void Start()
    {
        Init(this.gameObject);
    }
   protected void Init(GameObject go)
    {
        if (_mat != null)
            return;
        if (go.GetComponent<MeshRenderer>())
            _mat = isShare ? go.GetComponent<MeshRenderer>().sharedMaterial : go.GetComponent<MeshRenderer>().material;
        else
            _mat = isShare ? go.GetComponent<SkinnedMeshRenderer>().sharedMaterial : go.GetComponent<SkinnedMeshRenderer>().material;
        _mat.mainTextureScale = new Vector2(1 / num.x, 1 / num.y);
    }
    public void SetTex(int id)
    {
        if (id > TexLength)
            return;
        _mat.mainTextureOffset = new Vector2((1 / num.x) * (int)(id % num.x), (1 / num.y) * (int)(id / num.x));
    }
}