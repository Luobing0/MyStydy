using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public abstract class DecoratorNode : BaseNode
    {
        public BaseNode childNode;

         public override BaseNode Clone()
        {
            DecoratorNode node = Instantiate(this);
            node.childNode = childNode.Clone();
            return node;
        }
    }
}