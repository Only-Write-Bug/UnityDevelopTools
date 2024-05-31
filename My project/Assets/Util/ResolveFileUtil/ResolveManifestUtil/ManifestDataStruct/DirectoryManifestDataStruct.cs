using System.Collections.Generic;

namespace Util.ResolveFileUtil.ResolveManifestUtil
{
    /// <summary>
    /// 目录Manifest数据结构
    /// </summary>
    public class DirectoryManifestDataStruct
    {
        public uint ManifestFileVersion;
        public uint CRC;
        public List<(string name, List<string> Dependencies)> AssetBundleManifest =
            new List<(string name, List<string> Dependencies)>();
    }
}