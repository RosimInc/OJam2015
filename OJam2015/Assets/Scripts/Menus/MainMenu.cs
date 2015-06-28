using UnityEngine;
using System.Collections;
using MenusHandler;

public class MainMenu : Menu
{
    public void OnPlayClick()
    {
        GameManager.Instance.LoadPlayableLevel(0);
    }

    public void OnAchievementsClick()
    {
        MenusManager.Instance.ShowMenu("AchievementsMenu");
    }

    public void OnExitClick()
    {
        Application.Quit();
    }
}
