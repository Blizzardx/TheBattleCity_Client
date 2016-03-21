using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ExplosionManager : Singleton<ExplosionManager>
{
    public void CreateExplosion(string name, Vector3 pos)
    {
        ResourceManager.Instance.LoadBuildInAssetsAsync("Explosion/" + name, AssetType.Moudle, (obj) =>
        {
            if(null == obj)
            {
                Debuger.LogWarning("can't load explosion " + name);
            }
            //load res
            GameObject instance = GameObject.Instantiate(obj) as GameObject;
            //play audio

        });

    }
}
