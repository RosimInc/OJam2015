using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using MenusHandler;
using InputHandler;

public class AchievementsMenu : Menu
{
    public GameObject[] Bats;
    public GameObject[] LockIcons;

    public override void Open()
    {
        base.Open();

        BrainCloudManager.Instance.GetUnlockedAchievements(achievementsCallback);
    }

    private void achievementsCallback(bool[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            Bats[i].SetActive(array[i]);
            LockIcons[i].SetActive(!array[i]);
        }
    }

    public void GoToMainMenu()
    {
        MenusManager.Instance.ShowMenu("MainMenu");
    }
}
