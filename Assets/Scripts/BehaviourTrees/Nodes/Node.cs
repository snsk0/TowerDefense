using BehaviourTrees.Decolators;
using BehaviourTrees.Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTrees.Nodes
{
    public abstract class Node
    {
        public int priority { get; protected set; }
        private List<Service> services;
        private List<Decorator> decolators;

        public abstract void Excute();
    }
}

