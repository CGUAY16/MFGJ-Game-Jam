using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int checkpointIndex = 0;
    [SerializeField]
    string[] checkpointRooms = new string[0];
    [SerializeField]
    Vector3[] checkpointSpawns = new Vector3[0];
    public string currentRoom = "B1";

    int mode;
    public static GameManager Instance { get; set; }

    [SerializeField]
    public int difficulty = 0;
    [SerializeField]
    int resetsRemaining;

    public bool paused = false;
    public bool pauseControls = false;
    [SerializeField]
    Transform pauseMenu;
    [SerializeField]
    Transform spottedMenu;
    [SerializeField]
    Transform dialogueWindow;
    [SerializeField]
    Transform textField;
    Text dialogueText;

    public bool playerSpotted;
    [SerializeField]
    Canvas UIcanvas;





    void Awake()
    {
        //Ensures there is only one Game Manager to handle pausing, variables, etc.
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

        if (UIcanvas.worldCamera = null)
        {
            UIcanvas.worldCamera = FindObjectOfType<Camera>();
        } 

        dialogueText = textField.gameObject.GetComponent<Text>();

        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            if (pauseControls)
            {
                TogglePauseMenu();
                Debug.Log("Quit to menu");
                SceneManager.LoadScene(0);
                Destroy(this.gameObject);
            }
        }

        if (playerSpotted)
        {
            paused = true;
            spottedMenu.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Return))
            {


                //if (checkpointIndex >= 3 && checkpointIndex <= 7)
                //{
                //    DoorwayManager.Instance.SetSpawnPosition(checkpointSpawns[1]);
                //    SceneManager.LoadScene("B5");
                //}

                SceneManager.LoadScene(currentRoom);

                playerSpotted = false;
                spottedMenu.gameObject.SetActive(false);
                paused = false;
                Music.Instance.PlayTrack(1);


            }
        }

    }

    void TogglePauseMenu()
    {

        paused = !paused;
        if (paused)
        {
            Music.Instance.GetComponent<AudioSource>().volume -= 0.4f;
        }
        else
        {
            Music.Instance.GetComponent<AudioSource>().volume += 0.4f;
        }
        pauseControls = !pauseControls;
        pauseMenu.gameObject.SetActive(paused);
    }

    public void SetDialogueBoxCentered()
    {
        dialogueText.alignment = TextAnchor.MiddleCenter;
    }

    public void ResetDialogueBoxAlignment()
    {
        dialogueText.alignment = TextAnchor.UpperLeft;
    }

    public void ActivateDialogueBox(string dialogue)
    {
        dialogueText.text = dialogue;
        dialogueWindow.gameObject.SetActive(true);
    }

    public void DeactivateDialogueBox()
    {
        dialogueWindow.gameObject.SetActive(false);
        dialogueText.text = "";
    }

    public void SelfDestruct()
    {
        Destroy(this.gameObject);
    }

}
