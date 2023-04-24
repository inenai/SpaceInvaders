using UnityEngine;

[RequireComponent(typeof(SceneLoader))]
public class PauseMenu : MonoBehaviour
{

    [SerializeField] private GameObject pauseMenu;

    private bool showing;
    private bool pressingMenuBtn;

    public void HideMenu()
    {
        showing = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        EventDispatcher.PauseMenuOpen(false);
    }

    public void ShowMenu()
    {
        showing = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f; //Stop time during pause menu.
        EventDispatcher.PauseMenuOpen(true);
    }

    public void ExitGame()
    {
        Time.timeScale = 1f;
        GetComponent<SceneLoader>().LoadScene(SceneLoader.MAIN_MENU_SCENE);
    }

    private void Update()
    {
        if (Input.GetButton("Cancel") && !pressingMenuBtn)
        {
            pressingMenuBtn = true;
            if (showing)
            {
                HideMenu();
            }
            else
            {
                ShowMenu();
            }
        }
        else if (!Input.GetButton("Cancel")) //Avoid menu from opening and closing each frame while key is pressed.
        { 
            pressingMenuBtn = false;
        }
    }

}
