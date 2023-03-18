using System.Collections.Generic;
using Scripts.HordeSystem;
using UnityEngine;

namespace Scripts.LevelSelection
{
    [CreateAssetMenu(menuName = "RecycleCannon/Level Data")]
    public class LevelData : ScriptableObject
    {
        [SerializeField] int _levelNumber;

        [Header("Enemies")]
        [SerializeField] List<Vector3> _spawnPositions;
        [SerializeField] GameObject _spawnPrefab;

        [Header("Start Trash")]
        [SerializeField] List<Vector3> _trashPositions;
        [SerializeField] List<GameObject> _startTrashBags;

        [Header("Hordes")]
        [SerializeField] int _hordeDurationSeconds;
        [SerializeField] int _hordeIntervalSeconds;
        [SerializeField] int _hordeAmount;

        const string LOCKED_BASE_KEY = "level_locked_key";
        public string LockedKey => _levelNumber + LOCKED_BASE_KEY;
        public string NextLevelLockedKey => (_levelNumber+1) + LOCKED_BASE_KEY;

        const string COMPLETE_BASE_KEY = "level_completed_key";
        public string CompletedKey => _levelNumber + COMPLETE_BASE_KEY;

        public int LevelNumber => _levelNumber;

        public int HordeDurationSeconds => _hordeDurationSeconds;
        public int HordeAmount => _hordeAmount;
        public int HordeIntervalSeconds => _hordeIntervalSeconds;

        public bool IsLevelUnlocked()
        {
            return SaveSystem.SaveSystem.GetSavedBool(LockedKey);
        }

        public void UnlockLevel()
        {
            SaveSystem.SaveSystem.SaveBool(LockedKey, true);
        }

        public void UnlockNextLevel()
        {
            SaveSystem.SaveSystem.SaveBool(NextLevelLockedKey, true);
        }

        public bool IsLevelCompleted()
        {
            return SaveSystem.SaveSystem.GetSavedBool(CompletedKey);
        }

        public void CompleteLevel()
        {
            SaveSystem.SaveSystem.SaveBool(CompletedKey, true);
        }

        public void SetupSpawners()
        {
            foreach (Vector3 position in _spawnPositions)
            {
                Instantiate(_spawnPrefab, position, Quaternion.identity);
            }
        }

        public void SetupTrashBags()
        {
            for (int i = 0; i < _startTrashBags.Count; i++)
            {
                Instantiate(_startTrashBags[i],
                    _trashPositions[i],
                    Quaternion.identity);
            }
        }
    }
}
