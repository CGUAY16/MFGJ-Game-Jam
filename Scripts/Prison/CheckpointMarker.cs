using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointMarker : MonoBehaviour
{
    [SerializeField]
    int checkpoint;

    public void UpdateCheckpoint()
    {
        GameManager.Instance.checkpointIndex = checkpoint;
    } 
}
