using Scripts.LevelSelection;
using UnityEngine;

namespace Scripts.Menu
{
    public class LevelScreenController : AMenuScreenController
    {
        [SerializeField] GameObject _levelGrid;
        [SerializeField] LevelButtonController _levelButton;
        [SerializeField] LevelDataList _levelList;

        private void Start()
        {
            foreach (LevelData level in _levelList.LevelList) {

                _levelButton.SetupLevelButton(level, _levelGrid);
            }
        }
    }
}
