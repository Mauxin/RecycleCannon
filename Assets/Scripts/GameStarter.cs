using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene(UtilsConstants.MENU_SCENE, LoadSceneMode.Single);
    }
}
