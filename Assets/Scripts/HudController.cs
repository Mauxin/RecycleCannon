using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HudController : MonoBehaviour
{
    [SerializeField] Slider _playerLifeSlider;
    [SerializeField] Slider _wallLifeSlider;
    [SerializeField] Text​Mesh​Pro​UGUI _organicAmmoCount;
    [SerializeField] Text​Mesh​Pro​UGUI _recycledAmmoCount;
    [SerializeField] GameObject _organicAmmoIndicator;
    [SerializeField] GameObject _recycleAmmoIndicator;

    void Start()
    {
        PlayerController.onLifeUpdate += UpdatePlayerLife;
        CityWallController.onLifeUpdate += UpdateWallLife;
        AmmoController.onAmmoUpdateAmount += UpdateAmmo;
        AmmoController.onAmmoTypeChange += UpdateSelectedAmmo;
    }

    void OnDestroy()
    {
        PlayerController.onLifeUpdate -= UpdatePlayerLife;
        CityWallController.onLifeUpdate -= UpdateWallLife;
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
}
