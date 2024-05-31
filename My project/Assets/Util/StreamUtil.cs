using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public static class StreamUtil
{
    /// <summary>
    /// 目标文件夹是否存在
    /// </summary>
    /// <param name="folderPath"></param>
    /// <returns></returns>
    public static bool IsFolderExists(string folderPath)
    {
        try
        {
            return Directory.Exists(folderPath);
        }
        catch (IOException ex)
        {
            Debug.LogError($"Target path is not found :: {ex}");
            return false;
        }
    }

    public static bool IsFileExists(string filePath)
    {
        try
        {
            return File.Exists(filePath);
        }
        catch (IOException ex)
        {
            Debug.LogError($"Target path is not found :: {ex}");
            throw;
        }
    }

    /// <summary>
    /// 获取路径叶子节点名称
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string GetPathLeafNodeName(string path)
    {
        var tmpSB = new StringBuilder();
        for (int i = path.Length - 1; i >= 0; i--)
        {
            if (path[i] == '\\')
                break;
            tmpSB.Insert(0, path[i]);
        }

        return tmpSB.ToString();
    }

    /// <summary>
    /// 获取文件名（移除类型后缀）
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    public static string GetFileName(string file)
    {
        for (int i = 0; i < file.Length; i++)
        {
            if (file[i] != '.')
                continue;
            return file.Substring(0, i);
        }

        return file;
    }

    /// <summary>
    /// 获取文件类型
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    public static string GetFileType(string file)
    {
        for (int i = file.Length - 1; i >= 0; i--)
        {
            if (file[i] != '.')
                continue;
            return file.Substring(i + 1);
        }

        return file;
    }

    /// <summary>
    /// 去除父级路径
    /// </summary>
    /// <param name="pathA"></param>
    /// <param name="child"></param>
    /// <returns></returns>
    public static string RemoveParentPath(string parent, string child)
    {
        var rootPtr = 0;
        var pathPtr = 0;

        for (int i = 0; i < child.Length; i++)
        {
            if (rootPtr == parent.Length)
                return child.Substring(pathPtr);
            if (parent[rootPtr].Equals(child[pathPtr]))
            {
                rootPtr++;
                pathPtr++;
            }
            else
            {
                return child.Substring(pathPtr);
            }
        }

        return null;
    }

    /// <summary>
    /// 获取该目录下指定类型的文件
    /// </summary>
    /// <param name="root"></param>
    /// <param name="types">所有就用*</param>
    /// <param name="searchOption"></param>
    /// <param name="allFiles"></param>
    public static void GetFilesByType(string root, string[] types, SearchOption searchOption, out List<string> allFiles)
    {
        allFiles = new List<string>();
        
        foreach (var type in types)
        {
            var files = Directory.GetFiles(root, $"*.{type}", searchOption);
            allFiles.AddRange(files.ToList());
        }
    }

    /// <summary>
    /// 获取指定目录下指定类型以外的文件
    /// </summary>
    /// <param name="root"></param>
    /// <param name="exclusionType"></param>
    /// <param name="searchOption"></param>
    /// <param name="allFiles"></param>
    public static void GetFilesByOtherType(string root, string[] exclusionType, SearchOption searchOption,
        out List<string> allFiles)
    {
        allFiles = new List<string>();

        foreach (var file in Directory.GetFiles(root, "*", searchOption))
        {
            if(HasSpecifyType(file, exclusionType))
                continue;
            allFiles.Add(file);
        }
    }

    /// <summary>
    /// 文件是否有指定类型
    /// </summary>
    /// <param name="file"></param>
    /// <param name="types"></param>
    /// <returns></returns>
    public static bool HasSpecifyType(string file, string[] types)
    {
        foreach (var type in types)
        {
            if (StreamUtil.GetFileType(file).Equals(type))
                return true;
        }

        return false;
    }
}