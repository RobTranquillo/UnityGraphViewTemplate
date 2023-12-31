using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "tools/New BehaviourTree", fileName = "BehaviourTreeData.asset")]
public class BehaviourTree : ScriptableObject
{
    public Node rootNode;
    public Node.State treeState = Node.State.Running;
    [HideInInspector] public List<Node> nodes = new List<Node>();

    public Node.State Update()
    {
        if (rootNode.state == Node.State.Running)
            return rootNode.Update();

        return treeState;
    }

    public Node CreateNode(System.Type type)
    {
        Node node = ScriptableObject.CreateInstance(type) as Node;
        node.name = type.Name;
        node.guid = System.Guid.NewGuid().ToString();
        nodes.Add(node);

        AssetDatabase.AddObjectToAsset(node, this);
        AssetDatabase.SaveAssets();

        return node;
    }   

    public void  DeleteNode(Node node)
    {
        nodes.Remove(node);
        AssetDatabase.RemoveObjectFromAsset(node);
        AssetDatabase.SaveAssets();
    }

    public void AddChild(Node parent, Node child)
    {
        DecoratorNode decorator = parent as DecoratorNode;
        if (decorator)
            decorator.child = child;

        RootNode rootNode = parent as RootNode;
        if (rootNode)
            rootNode.child = child;

        CompositeNode composite = parent as CompositeNode;
        if (composite)
            composite.children.Add(child);
    }

    public void RemoveChild(Node parent, Node child)
    {
        DecoratorNode decorator = parent as DecoratorNode;
        if (decorator)
            decorator.child = null;

        RootNode rootNode = parent as RootNode;
        if (rootNode)
            rootNode.child = null;

        CompositeNode composite = parent as CompositeNode;
        if (composite)
            composite.children.Remove(child);
    }

    public List<Node> GetChildren(Node parent)
    {
        List<Node> children = new List<Node>();

        DecoratorNode decorator = parent as DecoratorNode;
        if (decorator && decorator.child != null)
            children.Add(decorator.child);

        RootNode rootNode = parent as RootNode;
        if (rootNode && rootNode.child != null)
            children.Add(rootNode.child);

        CompositeNode composite = parent as CompositeNode;
        if (composite)
            children = composite.children;

        return children;
    }

    public BehaviourTree Clone()
    {
        BehaviourTree tree = Instantiate(this);
        tree.rootNode = tree.rootNode.Clone();
        return tree;
    }
}
