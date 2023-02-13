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
            AssetsBundleMgr.DownLoadAssetsBundle(GetStreamingAssetsPath(), name.ToLower(), (go) =>
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
    public string GetStreamingAssetsPath()
    {
        string StreamingAssetsPath =
#if UNITY_EDITOR
        "file://" + Application.dataPath + "/StreamingAssets/";
#elif UNITY_ANDROID
        "jar:file://" + Application.dataPath + "!/assets/";
#elif UNITY_IPHONE
        Application.dataPath + "/Raw/";
#else
        string.Empty;
#endif
        return StreamingAssetsPath;
    }
}
