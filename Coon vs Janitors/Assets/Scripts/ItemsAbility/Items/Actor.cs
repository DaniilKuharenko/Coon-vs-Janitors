using UnityEngine;
using UnityEngine.AI;
using Utilites.Health;

namespace Items
{
    public class Actor : MonoBehaviour
    {
        [field: SerializeField] public Transform SelfTansform { get; private set; }
        [field: SerializeField] public NavMeshAgent Movement { get; private set; }
        [field: SerializeField] public CHealth Health { get; private set; }

        public Vector3 GetLocation() => SelfTansform != null ? SelfTansform.position : transform.position;

        public virtual void ApplayDamage(float amount)
        {
            if(Health != null && Health.IsAlive)
            {
                Health.SubtractHealth(amount);
            }
            else
            {
                OnDied();
            }
        }

        protected virtual void OnDied()
        {
            Destroy(gameObject);
        }
    }
}