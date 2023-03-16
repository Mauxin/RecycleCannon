using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] GameObject _enemyPrefab;

        List<GameObject> enemiesAlive = new List<GameObject>();

        private void Start()
        {
            enemiesAlive.Clear();
            EnemyController.onDie += onEnemyDead;
            StartCoroutine(SpawnEnemies());
        }

        void OnDestroy()
        {
            StopAllCoroutines();
            EnemyController.onDie += onEnemyDead;
        }

        IEnumerator SpawnEnemies()
        {
            while (true)
            {
                if (enemiesAlive.Count < 4)
                {

                    enemiesAlive.Add(Instantiate(_enemyPrefab, transform.position, Quaternion.identity));
                    yield return new WaitForSeconds(5);
                }

                yield return null;
            }
        }

        void onEnemyDead(GameObject dead)
        {
            if (enemiesAlive.Contains(dead))
            {
                enemiesAlive.Remove(dead);
                Destroy(dead);
            }
        }
    }
}
