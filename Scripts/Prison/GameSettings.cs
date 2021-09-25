using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public bool cameraLeftB3;
    public bool cameraRightB3;

    public bool computer1C6;
    public bool computer2C6;
    public bool computer3C6;
    public bool computer4C6;

    public bool cellOpenedA7;

    public static GameSettings Instance { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        //Ensures there is only one Game Setting to handle saved states, etc.
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

    }


}
