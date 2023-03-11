using UnityEngine;


namespace Scripts.LevelSelection
{
    [CreateAssetMenu(menuName = "RecycleCannon/Level Data")]
    public class LevelData : ScriptableObject
    {
        [SerializeField] int levelNumber;

        const string SAVE_STATE_BASE_KEY = "level_save_state_key";
        const string COMPLETE_BASE_KEY = "level_completed_key";

        public int LevelNumber => levelNumber;

        public string SaveKey => levelNumber + SAVE_STATE_BASE_KEY;

        public string CompletedKey => levelNumber + COMPLETE_BASE_KEY;
    }
}
