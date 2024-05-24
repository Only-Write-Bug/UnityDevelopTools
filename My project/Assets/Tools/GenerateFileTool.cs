using System.IO;
using System.Text;
using UnityEngine;

/// <summary>
/// 生成文件工具
/// </summary>
public static class GenerateFileTool
{
    private const string _keyDirectoryPath = @"\Assets\Scripts\Constants\Key\";

    /// <summary>
    /// 生成资源键文件
    /// </summary>
    /// <param name="resourcePath"></param>
    /// <param name="types">需要生成键的文件类型</param>
    /// <param name="searchOption"></param>
    /// <returns></returns>
    public static string GenerateResourceKeyFile(string resourcePath, string[] types, SearchOption searchOption)
    {
        var keyFileName = StreamUtil.GetPathLeafNodeName(resourcePath) + "Key.cs";
        var filePath = Directory.GetCurrentDirectory() + _keyDirectoryPath + keyFileName;
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        StreamUtil.GetFilesByType(resourcePath, types, searchOption, out var allFiles);
        var tmpSB = new StringBuilder();
        tmpSB.Append(GenerateResourceKeyScriptStart(keyFileName));
        foreach (var file in allFiles)
        {
            tmpSB.Append(
                $"\tpublic static string {StreamUtil.GetFileName(StreamUtil.GetPathLeafNodeName(file))} = @\"{file}\";\n\n");
        }

        tmpSB.Append(GenerateResourceKeyScriptEnd());
        File.WriteAllText(filePath, tmpSB.ToString());
        return null;
    }

    /// <summary>
    /// 生成资源键文件脚本开头
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    private static string GenerateResourceKeyScriptStart(string fileName)
    {
        return $"public static class {StreamUtil.GetFileName(fileName)}\n" +
               "{\n";
    }

    /// <summary>
    /// 生成资源键文件脚本结尾
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    private static string GenerateResourceKeyScriptEnd()
    {
        return "}";
    }
}