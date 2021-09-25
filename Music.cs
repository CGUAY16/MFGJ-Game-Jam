using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioSource player;
    [SerializeField]
    AudioClip[] tracks = new AudioClip[0];
    [SerializeField]
    int currentTrack = 1;

    public static Music Instance { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        //Ensures there is only one music player in the room at a time.
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

        player = GetComponent<AudioSource>();

    }


    public void PlayTrack(int index)
    {
        player.Stop();
        player.clip = tracks[index];
        player.Play();
    }

    public void PlayCurrentTrack()
    {
        player.Stop();
        player.clip = tracks[currentTrack];
        player.Play();
    }
}
