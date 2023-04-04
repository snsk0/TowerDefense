using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTrees.Nodes.Composites
{
    public abstract class Composite : Node
    {
        protected List<Node> childNodes;
    }
}

