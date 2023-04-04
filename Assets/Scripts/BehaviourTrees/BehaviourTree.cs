using BehaviourTrees.Nodes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTrees
{
    public class BehaviourTree
    {
        private Root root;

        public void RunTree()
        {
            root.Excute();
        }
    }
}

