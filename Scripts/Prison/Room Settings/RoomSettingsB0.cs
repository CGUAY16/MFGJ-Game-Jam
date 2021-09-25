using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSettingsB0 : MonoBehaviour
{
    [SerializeField]
    Transform[] guard;

    // Start is called before the first frame update
    void Awake()
    {
        if (GameManager.Instance.checkpointIndex >= 6)
        {
            foreach (var item in guard)
            {
                item.gameObject.SetActive(false);
            }
        }
    }

}
