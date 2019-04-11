using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuscript : MonoBehaviour
{
    void LoadGame() {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }
    void NewGame() {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }
    void ExitGame() {
        Application.Quit();
    }
}

