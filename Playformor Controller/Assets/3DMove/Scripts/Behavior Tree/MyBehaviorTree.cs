using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;
using System;
using UnityEditor;

[CreateAssetMenu()]
public class MyBehaviorTree : ScriptableObject
{
    public BaseNode rootNode;
    public NodeState treeState = NodeState.Running;
    public List<BaseNode> nodes = new List<BaseNode>();
    public Rigidbody body;
    public BlackBorad blackBorad = new BlackBorad();

    public NodeState Update()
    {
        if (rootNode.state == NodeState.Running)
        {
            treeState = rootNode.Update();
        }
        return treeState;
    }


#if UNITY_EDITOR
    public BaseNode CreateNode(Type type)
    {
        BaseNode node = ScriptableObject.CreateInstance(type) as BaseNode;
        node.name = type.Name;
        node.guid = GUID.Generate().ToString();
        Undo.RecordObject(this, "Behavior Tree (CreateNode)");
        nodes.Add(node);

        if (!Application.isPlaying)
        {
            AssetDatabase.AddObjectToAsset(node, this);
        }
        Undo.RegisterCreatedObjectUndo(node, "Behavior Tree (CreateNode)");
        AssetDatabase.SaveAssets();
        return node;
    }


    public void DeletNode(BaseNode node)
    {
        Undo.RecordObject(this, "Behavior Tree (DeleteNode)");
        nodes.Remove(node);
        // AssetDatabase.RemoveObjectFromAsset(node);
        Undo.DestroyObjectImmediate(node);
        AssetDatabase.SaveAssets();
    }

    public void AddChildNode(BaseNode parent, BaseNode child)
    {
        DecoratorNode decorator = parent as DecoratorNode;
        if (decorator)
        {
            Undo.RecordObject(decorator, "Behavior Tree (AddChild)");
            decorator.childNode = child;
            EditorUtility.SetDirty(decorator);
        }
        RootNode rootNode = parent as RootNode;
        if (rootNode)
        {
            Undo.RecordObject(rootNode, "Behavior Tree (AddChild)");
            rootNode.childNode = child;
            EditorUtility.SetDirty(rootNode);
        }

        CompositeNode composite = parent as CompositeNode;
        if (composite)
        {
            Undo.RecordObject(composite, "Behavior Tree (AddChild)");
            composite.childNodes.Add(child);
            EditorUtility.SetDirty(composite);
        }
    }

    public void RemoveChildNode(BaseNode parent, BaseNode child)
    {
        DecoratorNode decorator = parent as DecoratorNode;
        if (decorator)
        {
            Undo.RecordObject(decorator, "Behavior Tree (RemoveChild)");
            decorator.childNode = null;
            EditorUtility.SetDirty(decorator);
        }

        RootNode rootNode = parent as RootNode;
        if (rootNode)
        {
            Undo.RecordObject(rootNode, "Behavior Tree (RemoveChild)");
            rootNode.childNode = null;
            EditorUtility.SetDirty(rootNode);
        }

        CompositeNode composite = parent as CompositeNode;
        if (composite)
        {
            Undo.RecordObject(composite, "Behavior Tree (RemoveChild)");
            composite.childNodes.Remove(child);
            EditorUtility.SetDirty(composite);
        }
    }

    public List<BaseNode> GetNodes(BaseNode parent)
    {
        List<BaseNode> children = new List<BaseNode>();
        DecoratorNode decorator = parent as DecoratorNode;
        if (decorator && decorator.childNode != null)
        {
            children.Add(decorator.childNode);
        }

        RootNode rootNode = parent as RootNode;
        if (rootNode && rootNode.childNode != null)
        {
            children.Add(rootNode.childNode);
        }

        CompositeNode composite = parent as CompositeNode;
        if (composite)
        {
            return composite.childNodes;
        }
        return children;
    }
#endif

    public void Traverse(BaseNode node, Action<BaseNode> visiter)
    {
        if (node)
        {
            visiter.Invoke(node);
            var children = GetNodes(node);
            children.ForEach((x) => Traverse(x, visiter));
        }
    }

    public MyBehaviorTree Clone()
    {
        MyBehaviorTree tree = Instantiate(this);
        tree.rootNode = tree.rootNode.Clone();
        
        tree.nodes = new List<BaseNode>();
        Traverse(tree.rootNode, (n) =>
        {
            tree.nodes.Add(n);
        });
        return tree;
    }

    public void BindBlackBoard() {
        Traverse(rootNode, (n) => {
            n.blackborad = blackBorad;
        });
    }

}
