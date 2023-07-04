using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootNode : Node
{
    [HideInInspector] public Node child;

    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        return child.Update();
    }

    public override Node Clone()
    {
        if (child == null)
        {
            Debug.LogError($"({nameof(RootNode)} has no children connected.");
            DebugLogNode errorNode = ScriptableObject.CreateInstance<DebugLogNode>() as DebugLogNode;
            errorNode.name = "ErrorNode";
            errorNode.message = "Error: RootNode has no children connected.";
            child = errorNode;
        }

        RootNode node = Instantiate(this);
        node.child = child.Clone();
        return node;
    }
}
