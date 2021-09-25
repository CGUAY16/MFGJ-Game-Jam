using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSettingsB7 : MonoBehaviour
{
    [SerializeField]
    Transform[] gate;

    // Start is called before the first frame update
    void Awake()
    {
        if (GameManager.Instance.checkpointIndex >= 7)
        {
            foreach (var item in gate)
            {
                item.gameObject.SetActive(false);
            }
        }
    }

}
