using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class AssetBundlesPackEditorTool : EditorWindow
{
    private const string _packageSuffix = ".bundle";

    private List<(string path, BuildAssetBundleOptions buildOptions, BuildTarget target)>
        _targetDirectoriesConfig =
            new List<(string path, BuildAssetBundleOptions buildOptions, BuildTarget target)>();

    private string _curDirectory = "";
    private BuildAssetBundleOptions _curBundleOptions = BuildAssetBundleOptions.UncompressedAssetBundle;
    private BuildTarget _curBuildTarget = BuildTarget.StandaloneWindows;

    [MenuItem("Resources Tools/AssetBundles Pack Tool")]
    public static void Pack()
    {
        var window = GetWindow<AssetBundlesPackEditorTool>("AssetBundles Pack Tool");
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("New Directory Options",
            new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter });
        _curDirectory = EditorGUILayout.TextField("Directory", _curDirectory);
        _curBundleOptions =
            (BuildAssetBundleOptions)EditorGUILayout.EnumPopup("Compression Type", _curBundleOptions);
        _curBuildTarget = (BuildTarget)EditorGUILayout.EnumPopup("Build Target", BuildTarget.StandaloneWindows);
        EditorGUILayout.Space();

        if (GUILayout.Button("Add Directory"))
        {
            _targetDirectoriesConfig.Add((_curDirectory, _curBundleOptions, _curBuildTarget));
        }

        for (int i = 0; i < _targetDirectoriesConfig.Count; i++)
        {
            AddDirectoryOptions(_targetDirectoriesConfig[i]);
        }

        if (GUILayout.Button("Build AssetBundles"))
        {
            Build();
            Close();
        }
    }

    private void AddDirectoryOptions((string path, BuildAssetBundleOptions buildOptions, BuildTarget target) options)
    {
        EditorGUILayout.LabelField("Directory Options",
            new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter });
        EditorGUILayout.TextField("Directory Path :: ", options.path);
        EditorGUILayout.EnumPopup("Compression Type", options.buildOptions);
        EditorGUILayout.EnumPopup("Build Target", options.target);

        if (GUILayout.Button("Remove Directory"))
        {
            _targetDirectoriesConfig.Remove(options);
        }

        EditorGUILayout.Space();
    }

    private void Build()
    {
        EditorPrefs.DeleteKey("AssetBundleTabData");
        AssetDatabase.Refresh();
        
        foreach (var config in _targetDirectoriesConfig)
        {
            if (!StreamUtil.IsFolderExists(config.path))
            {
                Debug.LogError($"AB Pack Error :: {config.path} is not found");
                continue;
            }

            var targetPath = AssetBundlesTool.Instance.assetBundlesDirectoryPath + $"\\{config.target.ToString()}" +
                             $"\\{StreamUtil.GetPathLeafNodeName(config.path)}";
            if (!StreamUtil.IsFolderExists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
            }

            var curDirectories = Directory.GetDirectories(config.path, "*", SearchOption.AllDirectories);
            var sourceDirectories = new string[] { config.path };
            StructureUtil.SpliceArray(ref sourceDirectories, curDirectories);
            var builds = new List<AssetBundleBuild>();

            foreach (var directory in sourceDirectories)
            {
                StreamUtil.GetFilesByOtherType(directory, new[] { "meta", "JSON" }, SearchOption.TopDirectoryOnly,
                    out var files);
                for (int i = 0; i < files.Count; i++)
                {
                    files[i] = StreamUtil.RemoveParentPath(Directory.GetCurrentDirectory() + '\\', files[i]);
                }
                builds.Add(new AssetBundleBuild()
                {
                    assetBundleName = StreamUtil.GetPathLeafNodeName(directory) + _packageSuffix,
                    assetNames = files.ToArray()
                });
            }

            if (builds.Count <= 0)
                return;

            BuildPipeline.BuildAssetBundles(targetPath, builds.ToArray(), config.buildOptions, config.target);
            AssetBundlesTool.Instance.UpdateManifest(targetPath);
        }
    }
}