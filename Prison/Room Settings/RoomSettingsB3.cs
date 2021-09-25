using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSettingsB3 : MonoBehaviour
{
    [SerializeField]
    Computer computer1;
    [SerializeField]
    Computer computer2;

    // Start is called before the first frame update
    void Awake()
    {
        if (!GameSettings.Instance.cameraLeftB3)
        {
            computer1.computerStatus = false;
        }

        if (!GameSettings.Instance.cameraRightB3)
        {
            computer2.computerStatus = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameSettings.Instance.cameraLeftB3 = computer1.computerStatus;
        GameSettings.Instance.cameraRightB3 = computer2.computerStatus;
    }
}
