using System.Diagnostics;
using Scripts.Cannon;
using Scripts.HordeSystem;
using Scripts.PlayerSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.GameUI
{
    public class HudController : MonoBehaviour
    {
        [SerializeField] Slider _playerLifeSlider;
        [SerializeField] Slider _wallLifeSlider;
        [SerializeField] Text​Mesh​Pro​UGUI _organicAmmoCount;
        [SerializeField] Text​Mesh​Pro​UGUI _recycledAmmoCount;
        [SerializeField] GameObject _organicAmmoIndicator;
        [SerializeField] GameObject _recycleAmmoIndicator;
        [SerializeField] Text​Mesh​Pro​UGUI _hordeText;
        [SerializeField] Text​Mesh​Pro​UGUI _hordeTimerText;

        const string HORDE_INTERVAL = "NEXT IN:";
        const string HORDE_RUNNING = "HORDE ";

        Stopwatch hordeTimer = new Stopwatch();
        int currentHordeTime = -1;

        void Start()
        {
            PlayerController.onLifeUpdate += UpdatePlayerLife;
            CityWallController.onLifeUpdate += UpdateWallLife;
            AmmoController.onAmmoUpdateAmount += UpdateAmmo;
            AmmoController.onAmmoTypeChange += UpdateSelectedAmmo;
            HordeController.onHordeStart += UpdateHorde;
        }

        private void Update()
        {
            if (hordeTimer.IsRunning && currentHordeTime > -1)
            {
                _hordeTimerText.text = RemainingHordeTime(currentHordeTime).ToString();
            }
        }

        void OnDestroy()
        {
            PlayerController.onLifeUpdate -= UpdatePlayerLife;
            CityWallController.onLifeUpdate -= UpdateWallLife;
            AmmoController.onAmmoUpdateAmount -= UpdateAmmo;
            AmmoController.onAmmoTypeChange -= UpdateSelectedAmmo;
            HordeController.onHordeStart -= UpdateHorde;
        }

        void UpdatePlayerLife(float percentage)
        {
            _playerLifeSlider.value = percentage;
        }

        void UpdateWallLife(float percentage)
        {
            _wallLifeSlider.value = percentage;
        }

        void UpdateSelectedAmmo(AmmoType ammoType)
        {
            _organicAmmoIndicator.SetActive(ammoType == AmmoType.Organic);
            _recycleAmmoIndicator.SetActive(ammoType == AmmoType.Recycle);
        }

        void UpdateAmmo(AmmoType ammoType, int amount)
        {
            switch (ammoType)
            {
                case AmmoType.Organic:
                    UpdateOrganicAmmo(amount);
                    break;
                case AmmoType.Recycle:
                    UpdateRecycledAmmo(amount);
                    break;
            }
        }

        void UpdateOrganicAmmo(int amount)
        {
            _organicAmmoCount.text = amount.ToString();
        }

        void UpdateRecycledAmmo(int amount)
        {
            _recycledAmmoCount.text = amount.ToString();
        }

        void UpdateHorde(bool isInterval, int duration, int horde)
        {
            _hordeText.text = isInterval ? HORDE_INTERVAL : HORDE_RUNNING + horde;
            currentHordeTime = duration;
            hordeTimer.Reset();
            hordeTimer.Start();
        }

        int RemainingHordeTime(int maxTime)
        {
            return Mathf.FloorToInt(maxTime - (hordeTimer.ElapsedMilliseconds / 1000));
        }
    }
}
