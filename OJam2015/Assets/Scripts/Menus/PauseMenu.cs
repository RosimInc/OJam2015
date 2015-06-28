using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using MenusHandler;
using InputHandler;

public class PauseMenu : Menu
{
    public Text LevelName;

    protected override void Start()
    {
        InputManager.Instance.AddCallback(0, HandlePauseMenuInput);
    }

    public override void Open()
    {
        GameManager.Instance.Pause();
        LevelName.text = "Level " + (GameManager.Instance.LevelIndex + 1).ToString();
    }

    private void HandlePauseMenuInput(InputHandler.MappedInput input)
    {
        if (!gameObject.activeSelf) return;

        if (input.Actions.Contains("Start"))
        {
            Resume();
        }
    }

    public override void Close()
    {
        GameManager.Instance.Resume();
    }

    public void Resume()
    {
        MenusManager.Instance.RequestClose();
    }

    public void Restart()
    {
        MenusManager.Instance.RequestClose();
        GameManager.Instance.RestartLevel();
    }

    public void GoToMainMenu()
    {
        MenusManager.Instance.RequestClose();
        GameManager.Instance.LoadMainMenu();
    }
}
