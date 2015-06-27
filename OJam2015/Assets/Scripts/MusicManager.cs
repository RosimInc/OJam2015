using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    public AudioSource MainMenu;
    public AudioSource Gameplay;
    public AudioSource FlappyWings;
    public AudioSource Candy;

    public static MusicManager Instance
    {
        get { return _instance; }
    }

    private static MusicManager _instance;

	void Awake()
    {
        if (_instance)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void PlayMainMenuMusic()
    {
        if (!MainMenu.isPlaying)
        {
            MainMenu.Play();
        }
    }

    public void PlayGameplayMusic()
    {
        if (!Gameplay.isPlaying)
        {
            Gameplay.Play();
        }
    }

    public void PlayCandySound()
    {
        if (!Candy.isPlaying)
        {
            Candy.Play();
        }
    }
}
