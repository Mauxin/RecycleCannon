using Scripts.PlayerSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scripts.GameUI
{
    public class GameOverPopupController : MonoBehaviour
    {
        [SerializeField] GameObject _popup;
        [SerializeField] Button _tryAgainButton;
        [SerializeField] Button _goToMenuButton;

        private void Start()
        {
            _popup.SetActive(false);
            _tryAgainButton.onClick.AddListener(TryAgain);
            _goToMenuButton.onClick.AddListener(GoToMenu);

            PlayerController.onDied += OnGameOver;
            CityWallController.onDied += OnGameOver;
        }

        private void OnDestroy()
        {
            PlayerController.onDied -= OnGameOver;
            CityWallController.onDied -= OnGameOver;
        }

        void TryAgain()
        {
            SceneManager.LoadScene(UtilsConstants.GAME_SCENE, LoadSceneMode.Single);
        }

        void GoToMenu()
        {
            SceneManager.LoadScene(UtilsConstants.MENU_SCENE, LoadSceneMode.Single);
        }

        void OnGameOver()
        {
            _popup.SetActive(true);
        }
    }
}
