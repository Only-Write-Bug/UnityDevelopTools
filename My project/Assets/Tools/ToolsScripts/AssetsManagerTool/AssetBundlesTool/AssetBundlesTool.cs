using System.IO;
using UnityEditor;
using UnityEngine;
using Util.ResolveFileUtil.ResolveManifestUtil;

public class AssetBundlesTool : ToolBase<AssetBundlesTool>
{
    public string assetBundlesDirectoryPath { get; } = CheckABPackageDirectory();
    
    /// <summary>
    /// 清空AB包
    /// </summary>
    public void ClearAssetBundles()
    {
        var formerFiles = Directory.GetFiles(AssetBundlesTool.Instance.assetBundlesDirectoryPath);
        var formerDirectories = Directory.GetDirectories(AssetBundlesTool.Instance.assetBundlesDirectoryPath);
        for (int i = 0; i < formerFiles.Length; i++)
        {
            File.Delete(formerFiles[i]);
        }
        for (int i = 0; i < formerDirectories.Length; i++)
        {
            Directory.Delete(formerDirectories[i]);
        }
    }

    /// <summary>
    /// 更新Manifest文件
    /// </summary>
    /// <param name="rootPath"></param>
    public void UpdateManifest(string rootPath)
    {
        StreamUtil.GetFilesByType(rootPath, new []{ "manifest"}, SearchOption.AllDirectories, out var files);
        foreach (var file in files)
        {
            ResolveManifestUtil.ResolveDirectoryManifest(file);
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