using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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

        void SetupCompletedIcon()
        {
            _completedIcon.SetActive(
                SaveSystem.SaveSystem.GetSavedBool(levelData.CompletedKey));
        }

        void StartLevel()
        {
            SceneManager.LoadScene(UtilsConstants.GAME_SCENE, LoadSceneMode.Single);
        }
    }
}
