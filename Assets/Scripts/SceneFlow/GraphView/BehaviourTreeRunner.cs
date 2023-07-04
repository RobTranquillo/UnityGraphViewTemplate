using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTreeRunner : MonoBehaviour
{
    public BehaviourTree tree;

    private void Start()
    {
        if (tree == null)
        {
            Debug.LogError($"({nameof(BehaviourTreeRunner)}: tree is not set! Please drop in an {nameof(BehaviourTree)} ScriptableObject.");
            return;
        }
        tree = tree.Clone();
    }

    private void Update()
    {   
        if (tree == null)
            return;
        if (tree.rootNode == null)
            return;
        tree.Update();
    }
}
