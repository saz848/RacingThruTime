using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public GameObject menu;
    private bool isShowing;

    // Use this for initialization
    void Start ()
    {
        
    }

    void Update()
    {

    }

    public void LoadLevelSelect()
    {
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        //SceneManager.LoadScene("Menu");
        Application.Quit();
        
    }


}
