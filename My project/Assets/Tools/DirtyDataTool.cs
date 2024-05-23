using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public static class DirtyDataTool
{
    /// <summary>
    /// 更新指定目录中指定类型脏数据文件
    /// </summary>
    /// <param name="root"></param>
    /// <param name="checkType"></param>
    /// <returns>key :: 文件名， value ：：完整路径</returns>
    public static Dictionary<string, string> UpdateDirtyFile(string root, string[] checkType)
    {
        var dic = new Dictionary<string, string>();

        var allFiles = new List<string>();
        foreach (var type in checkType)
        {
            var files = Directory.GetFiles(root, $"*.{type}", SearchOption.AllDirectories);
            allFiles.AddRange(files.ToList());
        }

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
        
        return dic;
    }
}