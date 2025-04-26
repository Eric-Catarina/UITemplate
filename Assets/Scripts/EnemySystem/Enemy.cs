using UnityEngine;

namespace EnemySystem
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private EnemyBehaviorStep[] steps;

        private void Start()
        {
            steps[0].Perform(this);
        }
    }
}