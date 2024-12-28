using UnityEngine;

namespace Raccons_House_Games
{
    public class SoundTrap : Items
    {
        [SerializeField] private float attractionRadius = 5.0f;
        [SerializeField] private float activeDuration = 5.0f;

        private bool isActive = false;
        private float activeTimer;

        private void OnTriggerEnter(Collider other)
        {
            if (!isActive && other.CompareTag("Enemy"))
            {
                ActivateTrap();
                AttractEnemies();
            }
        }

        private void ActivateTrap()
        {
            isActive = true;
            activeTimer = activeDuration;
            Debug.Log("Trap Activated");
        }

        private void AttractEnemies()
        {
            EnemyControll[] enemies = UnityEngine.Object.FindObjectsByType<EnemyControll>(UnityEngine.FindObjectsSortMode.None);
            foreach (var enemy in enemies)
            {
                enemy.EnemyAlert();
            }
        }

        private void Update()
        {
            if (isActive)
            {
                SoundPlay();
                activeTimer -= Time.deltaTime;
                if (activeTimer <= 0)
                {
                    DeactivateTrap();
                }
            }
        }

        private void DeactivateTrap()
        {
            isActive = false;
            Debug.Log("Trap Deactivated");
        }

    }
}
