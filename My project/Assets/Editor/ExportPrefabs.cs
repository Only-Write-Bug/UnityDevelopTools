using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public static class ExportPrefabs
    {
        private const string _prefabsDirectoryName = "Prefabs";
        
        private static string _prefabsDirectoryPath;

        [MenuItem("UI Tools/Export Prefabs")]
        public static void Export()
        {
            _prefabsDirectoryPath ??= GetPrefabsDirectoryPath();
            if (_prefabsDirectoryPath == null)
            {
                Debug.LogError("UI Export Prefabs Error :: prefabs directory is not found");
                return;
            }

            var dirtyPrefabs = UpdateDirtyFile();
            GenerateFileTool.GenerateResourceKeyFile(_prefabsDirectoryPath, new []{"prefab"}, SearchOption.AllDirectories);
        }

        /// <summary>
        /// 获取预制体目录路径
        /// </summary>
        /// <returns></returns>
        private static string GetPrefabsDirectoryPath()
        {
            var assetsRoot = Directory.GetCurrentDirectory() + @"\Assets\";
            var prefabsDirectoryPath = Directory.GetDirectories(assetsRoot);
            foreach (var path in prefabsDirectoryPath)
            {
                if (StreamUtil.GetPathLeafNodeName(path) == _prefabsDirectoryName)
                    return path;
            }
            return null;
        }

        /// <summary>
        /// 更新脏数据文件
        /// </summary>
        private static string[] UpdateDirtyFile()
        {
            return DirtyDataTool.UpdateDirtyFile(_prefabsDirectoryPath, new []{"prefab"});
        }
    }
}