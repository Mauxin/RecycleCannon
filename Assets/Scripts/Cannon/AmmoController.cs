using Scripts.PlayerSystem;
using Scripts.RecycleSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Cannon
{
    public enum AmmoType
    {
        Organic = 0,
        Recycle = 1
    }

    public class AmmoController : MonoBehaviour
    {
        [SerializeField] Button _swapButton;
        [SerializeField] Image _buttonBg;

        static Color ORGANIC_GREEN = new Color(0.1176470588f, 0.6039215686f, 0.2078431373f);
        static Color RECYCLE_ORANGE = new Color(0.7921568627f, 0.3725490196f, 0f);

        int organicAmount = -1;
        int recycleAmount = -1;

        public static AmmoType SelectedType = AmmoType.Organic;
        public static bool IsOutOfAmmo = true;


        public delegate void OnAmmoTypeSelectedChange(AmmoType type);
        public static event OnAmmoTypeSelectedChange onAmmoTypeChange;
        public delegate void OnAmmoUpdateAmount(AmmoType type, int amount);
        public static event OnAmmoUpdateAmount onAmmoUpdateAmount;

        void Start()
        {
            organicAmount = 0;
            recycleAmount = 0;
            _swapButton.onClick.AddListener(SwapAmmoType);
            _buttonBg.color = ORGANIC_GREEN;

            CannonController.onCannonFired += spentAmmo;
            PlayerController.onTashDelivered += earnAmmo;
        }

        void OnDestroy()
        {
            CannonController.onCannonFired -= spentAmmo;
            PlayerController.onTashDelivered -= earnAmmo;
        }

        void SwapAmmoType()
        {
            switch (SelectedType)
            {
                case AmmoType.Organic:
                    SelectedType = AmmoType.Recycle;
                    _buttonBg.color = RECYCLE_ORANGE;
                    onAmmoTypeChange(AmmoType.Recycle);
                    break;
                case AmmoType.Recycle:
                    SelectedType = AmmoType.Organic;
                    _buttonBg.color = ORGANIC_GREEN;
                    onAmmoTypeChange(AmmoType.Organic);
                    break;
            }

            UpdateOutOfAmmo();
        }

        void spentAmmo()
        {
            switch (SelectedType)
            {
                case AmmoType.Organic:
                    organicAmount--;
                    onAmmoUpdateAmount(AmmoType.Organic, organicAmount);
                    break;
                case AmmoType.Recycle:
                    recycleAmount--;
                    onAmmoUpdateAmount(AmmoType.Recycle, recycleAmount);
                    break;
            }

            UpdateOutOfAmmo();
        }

        void earnAmmo(TrashType type)
        {
            switch (type)
            {
                case TrashType.Organic:
                    organicAmount += 20;
                    onAmmoUpdateAmount(AmmoType.Organic, organicAmount);
                    break;
                case TrashType.Plastic:
                case TrashType.Metal:
                    recycleAmount += 20;
                    onAmmoUpdateAmount(AmmoType.Recycle, recycleAmount);
                    break;
            }

            UpdateOutOfAmmo();
        }

        void UpdateOutOfAmmo()
        {
            IsOutOfAmmo = SelectedType == AmmoType.Organic ?
                        organicAmount <= 0 : recycleAmount <= 0;
        }
    }
}
