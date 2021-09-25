using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorwayManager : MonoBehaviour
{
    /// <summary>
    /// Manages transitions between rooms, carrying spawn information and next room names between scenes.
    /// </summary>

    //Recording door location in new rooms
    private Vector2 spawnPosition;
    private bool spawnPrepared;
    public static DoorwayManager Instance { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        //Ensures there is only one instance of this manager
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

        SceneManager.sceneLoaded += SceneLoaded;
    }


    //Triggers after loading of the next scene, through the Scene Manager
    private void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (spawnPrepared)
        {
            MovePosition();
        }
    }

    //Ensures the spawn location is recorded for that door before loads
    public void SetSpawnPosition(Vector2 spawnPosition)
    {
        spawnPrepared = true;
        this.spawnPosition = spawnPosition;
        Scene scene = SceneManager.GetActiveScene();
        GameManager.Instance.currentRoom = scene.name;
    }

    //Moves the Player character once new room is loaded
    private void MovePosition()
    {
        FindObjectOfType<Player>().TeleportTo(spawnPosition);
        spawnPrepared = false;
    }

}
