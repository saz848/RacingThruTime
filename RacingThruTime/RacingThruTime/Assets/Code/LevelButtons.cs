using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButtons : MonoBehaviour {
	// Use this for initialization
	public void LevelLoad()
    {
        SceneManager.LoadScene(name);
    }
}
