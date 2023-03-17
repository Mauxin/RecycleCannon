using System.Collections;
using System.Collections.Generic;
using Scripts.HordeSystem;
using UnityEngine;

namespace Scripts.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] EnemyDataList _enemyDataList;

        List<GameObject> enemiesAlive = new List<GameObject>();

        bool isHordeInterval = true;

        private void Start()
        {
            enemiesAlive.Clear();
            EnemyController.onDie += onEnemyDead;
            HordeController.onHordeStart += onHordeStart;
            StartCoroutine(SpawnEnemies());
        }

        void OnDestroy()
        {
            StopAllCoroutines();
            EnemyController.onDie += onEnemyDead;
            HordeController.onHordeStart -= onHordeStart;
        }

        IEnumerator SpawnEnemies()
        {
            while (true)
            {
                if (isHordeInterval)
                {
                    yield return null;
                    continue;
                }

                if (enemiesAlive.Count < 4)
                {

                    enemiesAlive.Add(Instantiate(
                        _enemyDataList.RandomEnemy(),
                        transform.position,
                        Quaternion.identity));

                    yield return new WaitForSeconds(25);
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

        void onHordeStart(bool isInterval, int duration, int horde)
        {
            isHordeInterval = isInterval;
        }
    }
}
