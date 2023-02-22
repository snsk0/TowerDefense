using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Players
{
    public class PlayerBackpack
    {
        public int enhancementPoint { get; private set; }

        public void AddEnhancementPoint(int point)
        {
            enhancementPoint += point;
        }
    }
}

