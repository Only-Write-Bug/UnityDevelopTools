using System;
using System.IO;
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
            Debug.Log($"Target path is not found :: {ex}");
            return false;
        }
    }
}