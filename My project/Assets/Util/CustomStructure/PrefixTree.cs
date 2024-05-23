using System.Collections.Generic;
using System.Text;

/// <summary>
/// 前缀树
/// </summary>
public class PrefixTree
{
    private readonly PrefixTreeNode _root = new();
    public PrefixTreeNode root => _root;

    /// <summary>
    /// 插入字符串
    /// </summary>
    /// <param name="value"></param>
    public void Insert(string value)
    {
        var curNode = root;
        foreach (var character in value)
        {
            if (curNode.childen.Count <= 0)
            {
                var tmpNode = new PrefixTreeNode
                {
                    parent = curNode,
                    character = character
                };
                curNode = tmpNode;
                continue;
            }

            foreach (var node in curNode.childen)
            {
                if (node.character.Equals(character))
                {
                    curNode = node;
                    break;
                }
            }

            if (!curNode.character.Equals(character))
            {
                var tmpNode = new PrefixTreeNode
                {
                    parent = curNode,
                    character = character
                };
                curNode = tmpNode;
            }
        }

        curNode.isLeafNode = true;
    }

    /// <summary>
    /// 查找字符串，返回叶子节点
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public PrefixTreeNode Search(string value)
    {
        if (value.Length <= 0)
            return null;
        
        var curNode = root;
        var tmpSB = new StringBuilder();
        foreach (var character in value)
        {
            foreach (var node in curNode.childen)
            {
                if (node.character.Equals(character))
                {
                    curNode = node;
                    tmpSB.Append(node.character);
                    continue;
                }
            }
        }

        if (tmpSB.ToString().Equals(value))
            return curNode;

        return null;
    }

    /// <summary>
    /// 移除字符串
    /// </summary>
    /// <param name="value"></param>
    public void Remove(string value)
    {
        var leafNode = Search(value);
        if (leafNode == null)
            return;

        while (leafNode.parent != null)
        {
            if (leafNode.childen.Count > 0)
                leafNode = leafNode.parent;
            else
            {
                var tmpNode = leafNode;
                leafNode = leafNode.parent;
                leafNode.childen.Remove(tmpNode);
            }
        }
    }

    /// <summary>
    /// 获取指定前缀的字符串集合
    /// </summary>
    /// <param name="prefix"></param>
    /// <returns></returns>
    public string[] GetPreFixStrings(string prefix)
    {
        var prefixNode = Search(prefix);
        if (prefixNode == null)
            return null;

        var tmpList = new List<string>();
        IterateChildenLeafNode(prefixNode, tmpList);
        
        return tmpList.ToArray();
    }

    /// <summary>
    /// 查找此时树中的共同前缀
    /// </summary>
    /// <returns></returns>
    public string GetPrefix()
    {
        var curNode = root;
        while (curNode.childen.Count == 1)
        {
            curNode = curNode.childen[0];
        }

        return GetNodePath(curNode);
    }

    /// <summary>
    /// 迭代当前节点下的所有叶子节点，并输入完整路径
    /// </summary>
    /// <param name="curNode"></param>
    /// <param name="tmpList"></param>
    private void IterateChildenLeafNode(PrefixTreeNode curNode, ICollection<string> tmpList)
    {
        if (curNode.isLeafNode)
        {
            tmpList.Add(GetNodePath(curNode));
        }

        if (curNode.childen.Count <= 0)
        {
            return;
        }

        foreach (var node in curNode.childen)
        {
            IterateChildenLeafNode(node, tmpList);
        }
    }

    /// <summary>
    /// 获取节点路径
    /// </summary>
    /// <param name="curNode"></param>
    /// <returns></returns>
    private string GetNodePath(PrefixTreeNode curNode)
    {
        var tmpSB = new StringBuilder();

        while (curNode != root)
        {
            tmpSB.Insert(0, curNode.character);
            curNode = curNode.parent;
        }

        return tmpSB.ToString();
    }
}

public class PrefixTreeNode
{
    private PrefixTreeNode _parent;
    public PrefixTreeNode parent
    {
        get => _parent;
        set
        {
            _parent = value;
            value?.childen.Add(this);
        }
    }

    public List<PrefixTreeNode> childen = new List<PrefixTreeNode>();

    public char character;

    public bool isLeafNode = false;
    public string value;
}
