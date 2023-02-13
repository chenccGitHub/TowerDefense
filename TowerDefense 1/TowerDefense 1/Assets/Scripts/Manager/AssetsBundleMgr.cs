using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Networking;

public class AssetsBundleMgr : MonoBehaviour
{
    public static void DownLoadAssetsBundle(string path, string name, Action<object> callBack)
    {
        new Task(GetAssetsBundle(path, name, callBack));
    }
    private static IEnumerator GetAssetsBundle(string path,string name,Action<object> callBack)
    {
        var uwr = UnityWebRequestAssetBundle.GetAssetBundle(path + name);
        yield return uwr.SendWebRequest();
        AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(uwr);
        var loadAsset = bundle.LoadAllAssetsAsync();
        yield return loadAsset;
        var prefab = loadAsset.asset;
        var go = Instantiate(prefab);
        callBack(go);
        bundle.Unload(false);
        Resources.UnloadUnusedAssets();
        
    }
}
