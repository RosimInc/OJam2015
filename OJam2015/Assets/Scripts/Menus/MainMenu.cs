using UnityEngine;
using System.Collections;
using MenusHandler;

public class MainMenu : Menu
{
    public void OnPlayClick()
    {
        GameManager.Instance.LoadPlayableLevel(0);
    }

    public void OnControlsClick()
    {

    }

    public void OnExitClick()
    {
        Application.Quit();
    }
}
