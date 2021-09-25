using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSettingsC6 : MonoBehaviour
{
    [SerializeField]
    Computer computer1;
    [SerializeField]
    Computer computer2;
    [SerializeField]
    Computer computer3;
    [SerializeField]
    Computer computer4;

    // Start is called before the first frame update
    void Awake()
    {
        if (!GameSettings.Instance.computer1C6)
        {
            computer1.computerStatus = false;
        }

        if (!GameSettings.Instance.computer2C6)
        {
            computer2.computerStatus = false;
        }

        if (!GameSettings.Instance.computer3C6)
        {
            computer3.computerStatus = false;
        }

        if (!GameSettings.Instance.computer4C6)
        {
            computer4.computerStatus = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameSettings.Instance.computer1C6 = computer1.computerStatus;
        GameSettings.Instance.computer2C6 = computer2.computerStatus;
        GameSettings.Instance.computer3C6 = computer3.computerStatus;
        GameSettings.Instance.computer4C6 = computer4.computerStatus;
    }
}
