﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using MenusHandler;
using InputHandler;

public class WinMenu : Menu {
    public void Restart() {
        MenusManager.Instance.RequestClose();
        GameManager.Instance.RestartLevel();
    }

    public void GoToMainMenu() {
        MenusManager.Instance.RequestClose();
        GameManager.Instance.LoadMainMenu();
    }
}
