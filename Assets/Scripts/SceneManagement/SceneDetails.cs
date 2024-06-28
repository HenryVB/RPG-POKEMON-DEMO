using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDetails : MonoBehaviour
{
    [SerializeField] private List<SceneDetails> connectedScenes;
    [SerializeField] private AudioClip sceneMusic;

    private bool isLoaded;

    public bool IsLoaded { get => isLoaded; set => isLoaded = value; }
    public AudioClip SceneMusic { get => sceneMusic; set => sceneMusic = value; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {

            LoadScene();
            GameManager.Instance.SetCurrentScene(this);

            if (SceneMusic != null)
                AudioManager.instance.PlayMusic(SceneMusic, fade: true);

            foreach (var scene in connectedScenes)
            {
                scene.LoadScene();
            }

            
            // Quitar escenas que ya no estan conectadas y no son necesarias
            if (GameManager.Instance.PrevScene != null)
            {
                var previoslyLoadedScenes = GameManager.Instance.PrevScene.connectedScenes;
                foreach (var scene in previoslyLoadedScenes)
                {
                    if (!connectedScenes.Contains(scene) && scene != this)
                        scene.UnloadScene();
                }
            }
        }
    }

    public void LoadScene()
    {
        if (!IsLoaded)
        {
            SceneManager.LoadSceneAsync(gameObject.name, LoadSceneMode.Additive);
            IsLoaded = true;
        }
    }

    public void UnloadScene()
    {
        if (IsLoaded)
        {
            SceneManager.UnloadSceneAsync(gameObject.name);
            IsLoaded = false;
        }
    }
}
