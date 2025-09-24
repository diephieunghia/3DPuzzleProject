using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BuildAssetBundles
{ 
    [MenuItem("Assets/Build AssetBundles")]
    static void BuildAllAssetBundles()
    {
        string path = "Assets/AssetBundles";
        BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
    }
}
