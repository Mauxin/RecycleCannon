using UnityEngine;
using System.Collections;

namespace Scripts.LevelSelection
{
    public class LevelButtonController : MonoBehaviour
    {
        [SerializeField] LevelButtonView _buttonView;

        LevelData levelData;

        public void SetupLevelButton(LevelData level, GameObject parent)
        {
            levelData = level;
            _buttonView.SetupView(levelData);
            GameObject.Instantiate(_buttonView, parent.transform);
        }
    }
}