using System.Collections.Generic;
using Scripts.Cannon;
using UnityEngine;

namespace Scripts.Enemies {

    [CreateAssetMenu(menuName = "RecycleCannon/Enemy Data")]
    public class EnemyData : ScriptableObject
    {
        [SerializeField] float _moveSpeed;
        [SerializeField] List<GameObject> _trashDropList;
        [SerializeField] AmmoType _deadlyAmmo;
        [SerializeField] GameObject _enemyPrefab;

        public float MoveSpeed => _moveSpeed;

        public AmmoType DeadlyAmmo => _deadlyAmmo;

        public GameObject EnemyPrefab => _enemyPrefab;

        public GameObject RandomTrashBag()
        {
            return _trashDropList[Random.Range(0, _trashDropList.Count)];
        }
    }
}
