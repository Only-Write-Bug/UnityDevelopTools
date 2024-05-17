using System;
using System.Collections.Generic;
using JetBrains.Annotations;

/// <summary>
/// 双向链表
/// 具有头尾节点，模拟队列
/// 外部只关注值，不需要关注节点状态
/// </summary>
/// <typeparam name="T"></typeparam>
public class BilateralLinkedList<T>
{
    private BilateralLinkedListNode<T> _headNode = null;
    private BilateralLinkedListNode<T> _tailNode = null;

    private int _count = 0;
    public int count => _count;

    private LinkedList<BilateralLinkedListNode<T>> _nodePool = new LinkedList<BilateralLinkedListNode<T>>();

    public void Add(T go)
    {
        BilateralLinkedListNode<T> tmpNode;
        if (_nodePool.Count > 0)
        {
            tmpNode = _nodePool.Last.Value;
            _nodePool.RemoveLast();
            tmpNode.value = go;
            tmpNode.ClearLinks();
        }
        else
        {
            tmpNode = new BilateralLinkedListNode<T>(go);
        }

        if (_headNode == null)
        {
            _headNode = _tailNode = tmpNode;
        }
        else
        {
            tmpNode.preNode = _tailNode;
            _tailNode = tmpNode;
        }

        _count++;
    }

    public bool TryPop(out T value)
    {
        if (_count <= 0)
        {
            value = default(T);
            return false;
        }

        value = _headNode.value;

        if (_headNode.nextNode != null)
        {
            var oldHead = _headNode;
            _headNode = _headNode.nextNode;
            _headNode.preNode = null;
            oldHead.Clear();
            _nodePool.AddFirst(oldHead);
            _count--;
        }
        else
        {
            _headNode = _tailNode = null;
            _count = 0;
        }
        
        return true;
    }

    public T[] ToArray()
    {
        if (_count <= 0)
            return null;
        
        var tmpArray = new T[_count];

        var curNode = _headNode;
        var flag = 0;
        while (curNode != null)
        {
            tmpArray[flag++] = curNode.value;
            curNode = curNode.nextNode;
        }

        return tmpArray;
    }
}

public class BilateralLinkedListNode<T>
{
    public BilateralLinkedListNode(T go)
    {
        value = go;
    }
    
    private BilateralLinkedListNode<T> _preNode = null;
    public BilateralLinkedListNode<T> preNode
    {
        get => _preNode;
        set
        {
            _preNode = value;
            value?.AddPostNode(this);
        }
    }

    private BilateralLinkedListNode<T> _nextNode = null;
    public BilateralLinkedListNode<T> nextNode
    {
        get => _nextNode;
        set
        {
            _nextNode = value;
            value?.AddPreNode(this);
        }
    }
    
    public T value;

    public void AddPreNode(BilateralLinkedListNode<T> node) => _preNode = node;

    public void AddPostNode(BilateralLinkedListNode<T> node) => _nextNode = node;

    public void Clear()
    {
        ClearLinks();
        value = default(T);
    }

    public void ClearLinks()
    {
        _preNode = null;
        _nextNode = null;
    }
}