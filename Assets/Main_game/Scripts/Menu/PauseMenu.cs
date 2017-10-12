using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    private bool isPaused;
    [SerializeField]
    private GameObject pauseMenuCanvas;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isPaused)
        {
            pauseMenuCanvas.SetActive(true);
            Time.timeScale = 0f;
            GameObject.Find("HERO").GetComponent<PlayerControl>().enabled = false;

        }
        else
        {
            pauseMenuCanvas.SetActive(false);
            Time.timeScale = 1f;
            GameObject.Find("HERO").GetComponent<PlayerControl>().enabled = true;

        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            

        }

	}

    public void Resume()
    {
        isPaused = false;
        GameObject.Find("HERO").GetComponent<PlayerControl>().enabled = true;
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
