using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSettingsC5 : MonoBehaviour
{
    [SerializeField]
    Computer computer1;

    // Start is called before the first frame update
    void Awake()
    {
        if (GameManager.Instance.checkpointIndex >= 6)
        {
            computer1.computerStatus = false;
        }
    }

}
