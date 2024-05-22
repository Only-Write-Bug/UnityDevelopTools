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

        curNode.isLeaftNode = true;
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
        var leaftNode = Search(value);
        if (leaftNode == null)
            return;

        while (leaftNode.parent != null)
        {
            if (leaftNode.childen.Count > 0)
                leaftNode = leaftNode.parent;
            else
            {
                var tmpNode = leaftNode;
                leaftNode = leaftNode.parent;
                leaftNode.childen.Remove(tmpNode);
            }
        }
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

    public bool isLeaftNode = false;
    public string value;
}
