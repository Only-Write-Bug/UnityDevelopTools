using UnityEngine;

public class AssetsManager : ToolBase<AssetsManager>
{
    /// <summary>
    /// 将预制体打包为AB包
    /// </summary>
    public void PackPrefabsToAssetBundles()
    {
        AssetBundlesTool.Instance.PackSpecifiedDirectoryResources(
            "F:\\UnityProject\\UnityDevelopTools\\My project\\Assets\\Prefabs", new[] { "prefab" });
    }
}