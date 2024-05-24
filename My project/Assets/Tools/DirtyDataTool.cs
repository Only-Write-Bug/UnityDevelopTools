using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;
using Formatting = System.Xml.Formatting;

public static class DirtyDataTool
{
    private class dataModel
    {
        public string path;
        public DateTime lastWriteTime;
        public bool isDirty;
    }

    //脏数据文件名称，所有脏数据文件都叫该名字，只依赖路径辨认
    private const string _dirtyFileName = "DirtyData.JSON";

    /// <summary>
    /// 更新指定目录中指定类型脏数据文件
    /// </summary>
    /// <param name="root"></param>
    /// <param name="checkType"></param>
    /// <returns>key :: 文件名， value ：：完整路径</returns>
    public static string[] UpdateDirtyFile(string root, string[] checkType)
    {
        var dirtyFiles = new List<string>();
        
        var dirtyDataFilePath = CheckDirtyFile(root);

        //获取所有指定类型的文件，并存储文件名和路径到字典中
        var dic = new Dictionary<string, string>();
        StreamUtil.GetFilesByType(root, checkType, SearchOption.AllDirectories, out var allFiles);

        if (allFiles.Count == 0)
        {
            return null;
        }

        foreach (var file in allFiles)
        {
            var fileName = StreamUtil.GetFileName(StreamUtil.GetPathLeafNodeName(file));
            var localPath = StreamUtil.RemoveParentPath(root, file);

            dic[fileName] = localPath;
        }

        //反序列化脏数据文件
        var json = File.ReadAllText(dirtyDataFilePath);
        var lastDirtyData = JsonConvert.DeserializeObject<Dictionary<string, dataModel>>(json);

        //对比文件
        if (dic.Keys.Count > 0 && lastDirtyData.Keys.Count > 0)
        {
            var invalidData = StructureUtil.ClearInvalidData<string>(dic.Keys.ToArray(), lastDirtyData.Keys.ToArray());
            foreach (var data in invalidData)
            {
                lastDirtyData.Remove(data);
            }
        }
        foreach (var fileName in dic.Keys)
        {
            if (lastDirtyData.TryGetValue(fileName, out var file))
            {
                if (file.lastWriteTime < File.GetLastWriteTime(root + file.path))
                {
                    file.lastWriteTime = File.GetLastWriteTime(root + dic[fileName]);
                    file.isDirty = true;
                    dirtyFiles.Add(file.path);
                }
                else
                {
                    file.isDirty = false;
                }
            }
            else
            {
                var data = new dataModel
                {
                    path = dic[fileName],
                    lastWriteTime = File.GetLastWriteTime(root + dic[fileName]).Date,
                    isDirty = true
                };
                lastDirtyData.Add(fileName, data);
                dirtyFiles.Add(data.path);
            }
        }
        var jsonString = JsonConvert.SerializeObject(lastDirtyData, Newtonsoft.Json.Formatting.Indented);
        File.WriteAllText(root + $"\\{_dirtyFileName}", jsonString);
        
        Debug.Log($"{root} dirty file count :: {dirtyFiles.Count}");

        return dirtyFiles.ToArray();
    }

    /// <summary>
    /// 检查脏数据文件
    /// </summary>
    /// <param name="root"></param>
    private static string CheckDirtyFile(string root)
    {
        string jsonPath = null;
        var files = Directory.GetFiles(root, _dirtyFileName, SearchOption.TopDirectoryOnly);
        if (files.Length <= 0)
        {
            var element = new { };
            var jsonString = JsonConvert.SerializeObject(element, Newtonsoft.Json.Formatting.Indented);
            jsonPath = root + $"\\{_dirtyFileName}";
            File.WriteAllText(jsonPath, jsonString);
        }
        else
        {
            jsonPath = files[0];
        }

        return jsonPath;
    }
}