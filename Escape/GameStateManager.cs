using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    [SerializeField]
    GameObject caughtScreen;
    [SerializeField]
    GameObject winScreen;
    bool caught;
    bool win;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (caught)
            {
                SceneManager.LoadScene("EscapeTheCops");

            }
            if (win)
            {
                SceneManager.LoadScene("Main Menu");
            }
        }
    }

    public IEnumerator Caught()
    {
        caught = true;
        Time.timeScale = 0;
        caughtScreen.SetActive(true);
        while (caught)
        {
            yield return null;
        }
        Time.timeScale = 1f;
        SceneManager.LoadScene("EscapeTheCops");
    }

    public IEnumerator Win()
    {
        win = true;
        Time.timeScale = 0;
        winScreen.SetActive(true);
        while (win)
        {
            yield return null;
        }
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }
}
