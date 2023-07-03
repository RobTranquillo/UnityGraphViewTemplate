using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTreeRunner : MonoBehaviour
{
    BehaviourTree tree;
    private void Start()
    {
        tree = ScriptableObject.CreateInstance<BehaviourTree>();

        // ein  Behaviour Tree von Hand zusammenbauen
        // Video:Unity | Create Behaviour Trees using UI Builder, GraphView, and Scriptable Objects[AI #11] 
        // https://youtu.be/nKpM98I7PeM?t=600
        var log1 = ScriptableObject.CreateInstance<DebugLogNode>();
        log1.message = "111 from BehaviourTreeRunner";        

        var wait = ScriptableObject.CreateInstance<WaitNode>();
        wait.duration = 1f;

        var log2 = ScriptableObject.CreateInstance<DebugLogNode>();
        log2.message = "222 from BehaviourTreeRunner";        

        var log3 = ScriptableObject.CreateInstance<DebugLogNode>();
        log3.message = "333 from BehaviourTreeRunner";
        
        var sequence = ScriptableObject.CreateInstance<SequencerNode>();
        sequence.children.Add(log1);
        sequence.children.Add(wait);
        sequence.children.Add(log2);
        sequence.children.Add(wait);
        sequence.children.Add(log3);

        var loop = ScriptableObject.CreateInstance<RepeatNode>();
        loop.child = sequence;
        // behaviour tree ende



        tree.rootNode = loop;


    }

    private void Update()
    {   
        tree.Update();
    }
}
