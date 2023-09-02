using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class BehaviorTreeRunner : MonoBehaviour
{
    public MyBehaviorTree tree;

    void Start()
    {
        tree = tree.Clone(); 
        tree.body =GetComponent<Rigidbody>();
        tree.nodes.ForEach(x => x.body = tree.body);
        tree.BindBlackBoard();
        tree.rootNode.blackborad.AddData("aa", 1);
        // tree = ScriptableObject.CreateInstance<MyBehaviorTree>();
        // var log1 = ScriptableObject.CreateInstance<DebugLogNode>();
        // log1.logStr = "Æô¶¯1£¡";

        // var pause1 = ScriptableObject.CreateInstance<WaitNode>();
        // pause1.duration = 10f;

        // var log2 = ScriptableObject.CreateInstance<DebugLogNode>();
        // log2.logStr = "Æô¶¯2£¡";
        // var log3 = ScriptableObject.CreateInstance<DebugLogNode>();
        // log3.logStr = "Æô¶¯3£¡";

        // var sequence = ScriptableObject.CreateInstance<SequenceNode>();

        // sequence.childNodes.Add(log1);
        // sequence.childNodes.Add(pause1);
        // sequence.childNodes.Add(log2);
        // sequence.childNodes.Add(log3);

        // var loop = ScriptableObject.CreateInstance<RepeatNode>();
        // loop.childNode = sequence;
        // tree.rootNode = loop;
    }
    void Update()
    {
        tree.Update();
    }
}
