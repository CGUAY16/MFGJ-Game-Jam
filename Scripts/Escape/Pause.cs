using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public static bool gamePaused = false;

    [SerializeField] AudioSource inGameMusic;
    [SerializeField] GameObject pauseElement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
            {
                ResumeGame();
            }
            else {
                PauseGame();
            }
        }

        if (gamePaused)
        {
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                SceneManager.LoadScene("Main Menu");
            }
        }
    }

    private void PauseGame()
    {
        inGameMusic.volume = 0.4f;
        pauseElement.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
    }

    private void ResumeGame()
    {
        inGameMusic.volume = 1.0f;
        pauseElement.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
    }
}
