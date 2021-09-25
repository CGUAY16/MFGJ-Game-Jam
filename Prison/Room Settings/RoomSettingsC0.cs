using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSettingsC0 : MonoBehaviour
{
    [SerializeField]
    Transform door;
    [SerializeField]
    Computer computer;

    private void Start()
    {
        if (GameManager.Instance.checkpointIndex > 1)
        {
            UpdateRoom();
        }
    }


    public void UpdateRoom()
    {
        computer.Activate();
    }
}
