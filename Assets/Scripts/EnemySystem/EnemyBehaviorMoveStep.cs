using UnityEngine;

namespace EnemySystem
{
    [CreateAssetMenu(menuName = nameof(EnemyBehaviorStep))]
    public class EnemyBehaviorMoveStep : EnemyBehaviorStep
    {
        [SerializeField] private Vector2 offset;
        public override void Perform(Enemy enemy)
        {
            enemy.transform.Translate(offset.x, offset.y, 0);
        }
    }
}