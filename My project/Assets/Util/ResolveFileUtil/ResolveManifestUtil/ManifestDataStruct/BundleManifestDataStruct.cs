using System.Collections.Generic;

namespace Util.ResolveFileUtil.ResolveManifestUtil
{
    /// <summary>
    /// AB包Manifest数据结构
    /// </summary>
    public class BundleManifestDataStruct
    {
        public uint ManifestFileVersion;
        public uint CRC;

        public Dictionary<string, (uint serializedVersion, string Hash)> Hashes =
            new Dictionary<string, (uint serializedVersion, string Hash)>();

        public uint HashAppended;

        public Dictionary<(string className, uint id), (string script, List<(string name, uint id)>)> ClassTypes =
            new Dictionary<(string className, uint id), (string script, List<(string name, uint id)>)>();

        public List<string> SerializeReferenceClassIdentifiers = new List<string>();
        public List<string> Assets = new List<string>();
        public List<string> Dependencies = new List<string>();
    }
}