using UnityEngine;

namespace Scripts.LevelSelection
{
    public class LevelButtonController : MonoBehaviour
    {
        [SerializeField] LevelButtonView _buttonView;

        LevelData levelData;

        public void SetupLevelButton(LevelData level, GameObject parent)
        {
            levelData = level;
            var button = GameObject.Instantiate(_buttonView, parent.transform);
            button.SetupView(levelData);
        }
    }
}