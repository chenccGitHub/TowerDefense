using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class Booster : MonoBehaviour
{
    public List<string> abNameList;
    void Awake()
    {
        //DirectoryInfo directory = new DirectoryInfo(Application.dataPath + "/Prefab");
        //var files = directory.GetFiles("*", SearchOption.AllDirectories);
        //foreach (var item in files)
        //{
        //    if (item.Name.EndsWith(".meta"))
        //    {
        //        continue;
        //    }
        //    string name = Path.GetFileNameWithoutExtension(item.Name);
        //    AssetsBundleMgr.DownLoadAssetsBundle(Application.dataPath + "/StreamingAssets/", name.ToLower(), (go) =>
        //    {
        //        var prefab = go as GameObject;
        //        prefab.name = name;
        //        FixShaders(prefab);
        //    });
        //}
        foreach (var name in abNameList)
        {
            AssetsBundleMgr.DownLoadAssetsBundle(Application.dataPath + "/StreamingAssets/", name.ToLower(), (go) =>
            {
                var prefab = go as GameObject;
                prefab.name = name;
                FixShaders(prefab);
            });
        }
        
    }
    void FixShaders(GameObject obj)
    {
    }
}
