using Scripts.HordeSystem;
using Scripts.LevelSelection;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scripts.GameUI
{
    public class GameWinPopupController : MonoBehaviour
    {
        [SerializeField] GameObject _popup;
        [SerializeField] Button _goToMenuButton;
        [SerializeField] TextMeshProUGUI _coinsText;
        [SerializeField] TextMeshProUGUI _diamondsText;

        private void Start()
        {
            _popup.SetActive(false);
            _goToMenuButton.onClick.AddListener(GoToMenu);
            HordeController.onGameWin += OnGameOver;
        }

        private void OnDestroy()
        {
            HordeController.onGameWin -= OnGameOver;
        }

        void GoToMenu()
        {
            SceneManager.LoadScene(UtilsConstants.MENU_SCENE, LoadSceneMode.Single);
        }

        void OnGameOver()
        {
            LevelBuilder.Instance.CompleteLevel();
            _popup.SetActive(true);
        }
    }
}
