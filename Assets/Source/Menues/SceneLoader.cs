using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public const string MAIN_MENU_SCENE = "MainMenu";

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
