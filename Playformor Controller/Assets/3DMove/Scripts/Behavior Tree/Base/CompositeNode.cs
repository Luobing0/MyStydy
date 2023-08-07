using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorTree
{
    public abstract class CompositeNode : BaseNode
    {
        public List<BaseNode> childNodes = new List<BaseNode>();

        public override BaseNode Clone()
        {
            CompositeNode node = Instantiate(this);
            node.childNodes = childNodes.ConvertAll(n => n.Clone());
            return node;
        }
    }
}