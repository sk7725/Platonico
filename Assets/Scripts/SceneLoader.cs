using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
    public static SceneLoader main;

    public string prevScene;

    private void Awake() {
        Debug.Log("New LevelLoader awake!");
        if (main != null && main != this) Destroy(gameObject);
        main = this;
        DontDestroyOnLoad(this);
    }

    public static void LoadScene(string scene) {
        //new GameObject("LevelLoader", typeof(LevelLoader));
        Debug.Log("Set Level!");
        main.prevScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(scene);
        Debug.Log("Loaded Scene!");
    }
}