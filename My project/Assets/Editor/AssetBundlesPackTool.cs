using System.Collections.Generic;
using Constants.Enum;
using UnityEditor;
using UnityEngine;

public class AssetBundlesPackTool : EditorWindow
{
    private List<(string path, ABPACKAGE_COMPRESSIONTYPE compressionType)> _targetDirectoriesConfig =
        new List<(string path, ABPACKAGE_COMPRESSIONTYPE compressionType)>();

    private string _curDirectory = "";
    private ABPACKAGE_COMPRESSIONTYPE _curCompressionType = ABPACKAGE_COMPRESSIONTYPE.UNCOMPRESSION;

    [MenuItem("Resources Tools/AssetBundles Pack Tool")]
    public static void Pack()
    {
        var window = GetWindow<AssetBundlesPackTool>("AssetBundles Pack Tool");
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("New Directory Options",
            new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter });
        _curDirectory = EditorGUILayout.TextField("Directory :: ", _curDirectory);
        _curCompressionType =
            (ABPACKAGE_COMPRESSIONTYPE)EditorGUILayout.EnumPopup("Compression Type", _curCompressionType);
        EditorGUILayout.Space();
        
        if (GUILayout.Button("Add Directory"))
        {
            _targetDirectoriesConfig.Add((_curDirectory, _curCompressionType));
            _curDirectory = "";
            _curCompressionType = ABPACKAGE_COMPRESSIONTYPE.UNCOMPRESSION;
        }

        for (int i = 0; i < _targetDirectoriesConfig.Count; i++)
        {
            AddDirectoryOptions(_targetDirectoriesConfig[i]);
        }

        if (GUILayout.Button("Build AssetBundles"))
        {
            Debug.Log("构建AB包完成");
        }
    }

    private void AddDirectoryOptions((string path, ABPACKAGE_COMPRESSIONTYPE compressionType) options)
    {
        EditorGUILayout.LabelField("Directory Options",
            new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter });
        EditorGUILayout.TextField("Directory Path :: ", options.path);
        EditorGUILayout.EnumPopup("Compression Type", options.compressionType);
        
        if (GUILayout.Button("Remove Directory"))
        {
            _targetDirectoriesConfig.Remove(options);
        }

        EditorGUILayout.Space();
    }
}