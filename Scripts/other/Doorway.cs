using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Doorway : MonoBehaviour
{
    /// <summary>
    /// Doorways hold the information for loading the next room. These can be done in any order,
    /// as long as the spawn location and name is correct.
    /// </summary>

    //Holds the name of the next room, and the Vector2 of where the player should enter
    [SerializeField]
    string sceneName;
    [SerializeField]
    Vector2 spawnLocation;

    //When running into a door
    void OnCollisionEnter2D(Collision2D collision)
    {
        //Checking it is only the player who can spawn
        if (collision.gameObject.CompareTag("Player"))
        {
            //Moves the spawn location from the doorway to the Manager Instance
            DoorwayManager.Instance.SetSpawnPosition(spawnLocation);
            //Loads the next room
            SceneManager.LoadScene(sceneName);
        }
    }
}
