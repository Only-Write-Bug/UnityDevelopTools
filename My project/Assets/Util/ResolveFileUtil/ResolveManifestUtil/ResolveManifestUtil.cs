using System.IO;
using Unity.VisualScripting;
using UnityEngine;

namespace Util.ResolveFileUtil.ResolveManifestUtil
{
    public class ResolveManifestUtil
    {
        /// <summary>
        /// 解析目录manifest
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static DirectoryManifestDataStruct ResolveDirectoryManifest(string path)
        {
            if (!(StreamUtil.IsFileExists(path) && IsManifestFile(path)))
            {
                return null;
            }
            var data = new DirectoryManifestDataStruct();
            var text = File.ReadAllText(path);
            
            foreach (var fields in StructureUtil.CuttingStringByCharacter(text, '\n'))
            {
                Debug.Log(fields);
            }
            
            return data;
        }
        
        /// <summary>
        /// 解析Bundle Manifest
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static BundleManifestDataStruct ResolveBundleManifest(string path)
        {
            if (!(StreamUtil.IsFileExists(path) && IsBundleManifest(path)))
            {
                return null;
            }
            var data = new BundleManifestDataStruct();

            return data;
        }

        /// <summary>
        /// 是否为Manifest文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IsManifestFile(string path)
        {
            return StreamUtil.GetFileType(path).Equals("manifest");
        }

        /// <summary>
        /// 是否为Bundle Manifest文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IsBundleManifest(string path)
        {
            return path.Contains("bundle.manifest");
        }

        private static void ResolveText(string text)
        {
            
        }
    }
}