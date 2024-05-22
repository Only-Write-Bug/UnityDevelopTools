using System;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class UGUIExport : EditorWindow
    {
        private static readonly string _prefabsPathKey = "UGUIExport_PrefabsPath";
        private static readonly string _exportPathKey = "UGUIExport_ExportPath";

        private string _prefabsPath;
        private string _exportPath;

        private bool _isDirty = false;

        [MenuItem("UGUI/Export")]
        private static void ShowExportWindow()
        {
            var window = GetWindow<UGUIExport>("UGUI Export Tool");
            window.LoadPreferences();
        }

        private void OnGUI()
        {
            _prefabsPath = EditorGUILayout.TextField("Sources Path :: ", _prefabsPath);
            _exportPath = EditorGUILayout.TextField("Export Path :: ", _exportPath);

            if (GUILayout.Button("Export"))
            {
                SavePreferences();
                if (StreamUtil.IsFolderExists(_prefabsPath) && StreamUtil.IsFolderExists(_exportPath))
                {
                    
                    this.Close();
                }
                else
                {
                    Debug.LogError("UI Export Error :: Please check folder path!");
                }
            }

            if (_isDirty)
            {
                SavePreferences();
                _isDirty = false;
            }
        }

        private void OnFocus()
        {
            LoadPreferences();
        }

        private void OnLostFocus()
        {
            SavePreferences();
        }

        private void LoadPreferences()
        {
            _prefabsPath = EditorPrefs.GetString(_prefabsPathKey, string.Empty);
            _exportPath = EditorPrefs.GetString(_exportPathKey, string.Empty);
        }

        private void SavePreferences()
        {
            EditorPrefs.SetString(_prefabsPathKey, _prefabsPath);
            EditorPrefs.SetString(_exportPathKey, _exportPath);
        }

        private void OnTextChanged(string value)
        {
            _isDirty = true;
        }

        /// <summary>
        /// 导出预制体的key及对应资源路径
        /// </summary>
        private void ExportPrefabKey()
        {
            
        }
    }
}