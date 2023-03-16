using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

        PlayerController.onPlayerDied += OnGameOver;
        CityWallController.onWallDied += OnGameOver;
    }

    private void OnDestroy()
    {
        PlayerController.onPlayerDied -= OnGameOver;
        CityWallController.onWallDied -= OnGameOver;
    }

    void TryAgain()
    {
        SceneManager.LoadScene(UtilsConstants.GAME_SCENE, LoadSceneMode.Single);
    }

    void GoToMenu()
    {
        SceneManager.LoadScene(UtilsConstants.GAME_SCENE, LoadSceneMode.Single);
    }

    void OnGameOver()
    {
        _popup.SetActive(true);
    }
}
