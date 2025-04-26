using System;
using UnityEngine;

namespace EnemySystem
{
    public abstract class EnemyBehaviorStep : ScriptableObject
    {
        public virtual void Perform(Enemy enemy)
        {
            
        }
    }
}