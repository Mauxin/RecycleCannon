using Scripts.HordeSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.LevelSelection
{
    public class LevelBuilder : MonoBehaviour
    {
        LevelData levelData;

        public static LevelBuilder Instance;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
            Instance = this;
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        public void BuildLevel(LevelData data)
        {
            levelData = data;

            SceneManager.LoadScene(UtilsConstants.GAME_SCENE, LoadSceneMode.Single);
        }

        public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name != UtilsConstants.GAME_SCENE) return;

            levelData.SetupSpawners();
            levelData.SetupTrashBags();

            var hordeController = GameObject.Find(UtilsConstants.HORDE_NAME).GetComponent<HordeController>();
            hordeController.Setup(levelData.HordeDurationSeconds,
                levelData.HordeIntervalSeconds, levelData.HordeAmount);
        }

        public void CompleteLevel()
        {
            levelData.CompleteLevel();
        }
    }
}