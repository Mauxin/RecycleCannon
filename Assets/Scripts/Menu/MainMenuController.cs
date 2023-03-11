using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Menu
{
    public class MainMenuController : MonoBehaviour
    {
        [Header("MenuButtons")]
        [SerializeField] Button _playButton;
        [SerializeField] Button _storeButton;
        [SerializeField] Button _settingsButton;

        [Header("MenuScreens")]
        [SerializeField] AMenuScreenController _levelSelectionScreen;
        [SerializeField] AMenuScreenController _storeScreen;
        [SerializeField] AMenuScreenController _settingsScreen;

        void Awake()
        {
            _playButton.onClick.AddListener(OnPlayClick);
            _storeButton.onClick.AddListener(OnStoreClick);
            _settingsButton.onClick.AddListener(OnSettingsClick);
        }

        void OnDestroy()
        {
            _playButton.onClick.RemoveListener(OnPlayClick);
            _storeButton.onClick.RemoveListener(OnStoreClick);
            _settingsButton.onClick.RemoveListener(OnSettingsClick);
        }

        void OnPlayClick()
        {
            _levelSelectionScreen.ShowScreen();
        }

        void OnStoreClick()
        {
            _storeScreen.ShowScreen();
        }

        void OnSettingsClick()
        {
            _settingsScreen.ShowScreen();
        }

    }
}
