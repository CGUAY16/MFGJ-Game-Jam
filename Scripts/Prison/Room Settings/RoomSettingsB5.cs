using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSettingsB5 : MonoBehaviour
{
    [SerializeField]
    Transform[] AdditionalGuards;
    [SerializeField]
    Transform[] FinalGuards;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance.checkpointIndex < 3)
        {
            GameManager.Instance.checkpointIndex = 3;
        }

        if (GameManager.Instance.checkpointIndex >= 4)
        {
            foreach (var item in AdditionalGuards)
            {
                item.gameObject.SetActive(true);
            }
        }

        if (GameManager.Instance.checkpointIndex > 5)
        {

            foreach (var item in FinalGuards)
            {
                item.gameObject.SetActive(true);
            }
        }
    }
}
