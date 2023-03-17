using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Enemies {

    [CreateAssetMenu(menuName = "RecycleCannon/Enemy Data List")]
    public class EnemyDataList : ScriptableObject
    {
        [SerializeField] List<EnemyData> _enemies;

        public List<EnemyData> Enemies => _enemies;

        public GameObject RandomEnemy()
        {
            return _enemies[Random.Range(0, _enemies.Count)].EnemyPrefab;
        }
    }
}
