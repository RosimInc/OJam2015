﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using MenusHandler;
using InputHandler;

public class GameManager : MonoBehaviour
{
    public MonoBehaviour[] PermanentManagers;

    public Image LevelTransitionImage;

    private static GameManager _instance;

    private int _levelIndex = 0;

    private const int FIRST_PLAYABLE_LEVEL_INDEX = 1;
    private const float FADE_DURATION = 1.5f;

    private bool _isLoadingScene = false;

    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }

    public bool IsLoadingScene
    {
        get { return _isLoadingScene; }
    }

    public int LevelIndex
    {
        get { return _levelIndex - FIRST_PLAYABLE_LEVEL_INDEX; }
    }

    private bool _isPaused;

    public bool IsPaused
    {
        get { return _isPaused; }
    }

    void Awake()
    {
        if (_instance)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);

            for (int i = 0; i < PermanentManagers.Length; i++)
            {
                MonoBehaviour permanentManager = PermanentManagers[i];

                string name = permanentManager.name;

                permanentManager = Instantiate(permanentManager) as MonoBehaviour;
                permanentManager.name = name;

                DontDestroyOnLoad(permanentManager.gameObject);
            }
        }

        Application.runInBackground = true;

        Cursor.visible = false;
        Application.targetFrameRate = 60;
    }

    void Start()
    {
        MenusManager.Instance.OnMenusOpened += OnMenusOpened;
        MenusManager.Instance.OnMenusClosed += OnMenusClosed;
        InputManager.Instance.AddCallback(0, HandeMenuInput);
        InputManager.Instance.AddCallback(0, HandleGameplayInput);

        if (_levelIndex == 0)
        {
            MenusManager.Instance.ShowMenu("MainMenu");
        }
    }

    private void HandeMenuInput(MappedInput input)
    {
        bool acceptButtonPressed = input.Actions.Contains("Confirm");
        bool backButonPressed = input.Actions.Contains("Cancel");
        float horizontalAxis = !input.Ranges.ContainsKey("MoveHorizontal") ? 0f : input.Ranges["MoveHorizontal"];
        float verticalAxis = !input.Ranges.ContainsKey("MoveVertical") ? 0f : input.Ranges["MoveVertical"];

        MenusManager.Instance.SetInputValues(acceptButtonPressed, backButonPressed, horizontalAxis, verticalAxis);
    }

    private void HandleGameplayInput(MappedInput input)
    {
        if (_levelIndex >= FIRST_PLAYABLE_LEVEL_INDEX && input.Actions.Contains("Start") && !_isPaused)
        {
            MenusManager.Instance.ShowMenu("PauseMenu");

            // TODO: Maybe put it in a "eat input" method in the InputMapper???
            input.Actions.Remove("Start");
        }
    }

    public void RestartLevel()
    {
        StartCoroutine(LoadLevelCoroutine(_levelIndex));
    }

    public void LoadPlayableLevel(int levelIndex)
    {
        StartCoroutine(LoadLevelCoroutine(FIRST_PLAYABLE_LEVEL_INDEX + levelIndex));
    }
    
    public void LoadNextLevel()
    {
        if (_levelIndex < Application.levelCount - 1)
        {
            StartCoroutine(LoadLevelCoroutine(_levelIndex + 1));
        }
        else
        {
            StartCoroutine(LoadLevelCoroutine(1));
        }
    }

    public void LoadMainMenu()
    {
        StartCoroutine(LoadLevelCoroutine(0));
    }

    private IEnumerator LoadLevelCoroutine(int levelIndex)
    {
        if (_isLoadingScene) yield return null;

        // We don't want any inputs to be registered when loading a scene
        InputManager.Instance.ClearContexts();

        _isLoadingScene = true;

        yield return StartCoroutine("FadeIn");

        Application.LoadLevel(levelIndex);
    }

    void OnLevelWasLoaded(int levelIndex)
    {
        InputManager.Instance.ClearContexts();

        if (levelIndex >= FIRST_PLAYABLE_LEVEL_INDEX)
        {
            InputManager.Instance.PushActiveContext("Gameplay");
        }

        _levelIndex = levelIndex;
        _isLoadingScene = false;

        // The first level is the main menu scene, so we need to show the menu
        if (_levelIndex == 0)
        {
            MenusManager.Instance.ShowMenu("MainMenu");
        }
        
        StartCoroutine("FadeOut");
    }

    private IEnumerator FadeIn()
    {
        StopCoroutine("FadeOut");

        Color transitionColor = LevelTransitionImage.color;

        float ratio = transitionColor.a;

        Color initialColor = new Color(transitionColor.r, transitionColor.g, transitionColor.b, 0f);
        Color finalColor = new Color(transitionColor.r, transitionColor.g, transitionColor.b, 1f);

        LevelTransitionImage.gameObject.SetActive(true);

        while (ratio < 1f)
        {
            ratio += Time.deltaTime / FADE_DURATION;

            LevelTransitionImage.color = Color.Lerp(initialColor, finalColor, ratio);

            yield return null;
        }
    }

    private IEnumerator FadeOut()
    {
        StopCoroutine("FadeIn");

        Color transitionColor = LevelTransitionImage.color;

        float ratio = 1f - transitionColor.a;

        Color initialColor = new Color(transitionColor.r, transitionColor.g, transitionColor.b, 1f);
        Color finalColor = new Color(transitionColor.r, transitionColor.g, transitionColor.b, 0f);

        LevelTransitionImage.gameObject.SetActive(true);

        while (ratio < 1f)
        {
            ratio += Time.deltaTime / FADE_DURATION;

            LevelTransitionImage.color = Color.Lerp(initialColor, finalColor, ratio);

            yield return null;
        }

        LevelTransitionImage.gameObject.SetActive(false);
    }

    public void Pause()
    {
        _isPaused = true;
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        _isPaused = false;
        Time.timeScale = 1f;
    }

    private void OnMenusOpened()
    {
        InputManager.Instance.PushActiveContext("Menu");
    }

    private void OnMenusClosed()
    {
        InputManager.Instance.PopActiveContext();
    }
}
