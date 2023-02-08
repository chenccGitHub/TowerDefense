using UnityEngine;
using UnityEditor;
using System.IO;

public class AssetsBundleEditor : MonoBehaviour
{
    [MenuItem("AssetsBundle/Build_Win64")]
    public static void BuildAssetBundle_Win64()
    {
        BuildAssetsBundle(BuildTarget.StandaloneWindows64);
    }
    [MenuItem("AssetsBundle/Build_Android")]
    public static void BuildAssetBundle_Android()
    {
        BuildAssetsBundle(BuildTarget.Android);
    }
    private static void BuildAssetsBundle(BuildTarget target)
    {
        string packagePath = Application.streamingAssetsPath;
        if (packagePath.Length <= 0 || !Directory.Exists(packagePath))
        {
            return;
        }
        BuildPipeline.BuildAssetBundles(packagePath, BuildAssetBundleOptions.UncompressedAssetBundle, target);
        AssetDatabase.Refresh();
        Debug.Log("package is done!");
    }
}
