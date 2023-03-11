using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Menu {

    public abstract class AMenuScreenController : MonoBehaviour
    {
        [SerializeField] GameObject _screen;
        [SerializeField] Button _backButton;

        private void Awake()
        {
            _screen.SetActive(false);
            _backButton.onClick.AddListener(CloseScreen);
        }

        public void ShowScreen() {
            _screen.SetActive(true);
        }

        public virtual void CloseScreen()
        {
            _screen.SetActive(false);
        }
    }
}
