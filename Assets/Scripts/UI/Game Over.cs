using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public string mainMenu;
    [SerializeField] private AudioClip bgMusic;

    private void Awake()
    {
        AudioManager.instance.PlayMusic(bgMusic, fade: true);
    }

    private void Start()
    {
        //AudioManager.instance.PlayMusic(bgMusic, fade: true);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenu);
    }
}
