using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonLoader : MonoBehaviour
{
    [SerializeField]
    Don donPrefab;
    Player player;


    void Update ()
    {
        if (GameManager.Instance.checkpointIndex >= 6 && FindObjectOfType<Don>() == null)
        {
            Debug.Log("No Don");
            player = FindObjectOfType<Player>();
            Don newDon = Instantiate(donPrefab, player.transform.position, Quaternion.identity);
            newDon.player = player;
            newDon.following = true;
            Debug.Log("Spawned?");

        }
    }
}
