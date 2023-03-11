using System.Collections.Generic;
using UnityEngine;


namespace Scripts.LevelSelection
{
    [CreateAssetMenu(menuName = "RecycleCannon/Level Data List")]
    public class LevelDataList : ScriptableObject
    {
        [SerializeField] List<LevelData> _levelDataList;

        public List<LevelData> LevelList => _levelDataList;
    }
}
