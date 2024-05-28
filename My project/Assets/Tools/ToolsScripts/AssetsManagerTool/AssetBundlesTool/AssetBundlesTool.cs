using System.IO;
using UnityEditor;
using UnityEngine;

public class AssetBundlesTool : ToolBase<AssetBundlesTool>
{
    public string assetBundlesDirectoryPath { get; } = CheckABPackageDirectory();

    /// <summary>
    /// 打包指定目录的指定类型资源资源
    /// </summary>
    /// <param name="resourcesDirectory"></param>
    public void PackSpecifiedDirectoryResources(string resourcesDirectory, string[] resourcesType)
    {
        if (!StreamUtil.IsFolderExists(resourcesDirectory))
        {
            return;
        }

        var childrenDirectories = Directory.GetDirectories(resourcesDirectory);
        foreach (var directory in childrenDirectories)
        {
            
        }
    }

    /// <summary>
    /// 检查AssetBundles目录
    /// </summary>
    /// <returns></returns>
    private static string CheckABPackageDirectory()
    {
        var abPackageDirectory = Directory.GetCurrentDirectory() + @"\Assets\AssetBundlesPackages";
        if (!StreamUtil.IsFolderExists(abPackageDirectory))
        {
            Directory.CreateDirectory(abPackageDirectory);
        }

        return abPackageDirectory;
    }
}