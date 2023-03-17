using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.LevelSelection
{
    public class LevelButtonView : MonoBehaviour
    {
        [SerializeField] Button _selectionButton;
        [SerializeField] GameObject _completedIcon;
        [SerializeField] Text​Mesh​Pro​UGUI _buttonText;

        LevelData levelData;

        public void SetupView(LevelData data)
        {
            levelData = data;
            SetupText();
            SetupUnlockedLevel();
            SetupCompletedIcon();
        }

        private void Start()
        {
            _selectionButton.onClick.AddListener(StartLevel);
        }

        void SetupText()
        {
            _buttonText.text = levelData.LevelNumber < 10 ?
                "0" + levelData.LevelNumber : levelData.LevelNumber.ToString();
        }

        void SetupUnlockedLevel()
        {
            if (levelData.LevelNumber == 1) levelData.UnlockLevel();

            _selectionButton.interactable = levelData.IsLevelUnlocked();
        }

        void SetupCompletedIcon()
        {
            _completedIcon.SetActive(levelData.IsLevelCompleted());
        }

        void StartLevel()
        {
            LevelBuilder.Instance.BuildLevel(levelData);
        }
    }
}
