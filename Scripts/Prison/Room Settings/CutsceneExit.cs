using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneExit : MonoBehaviour
{
    [SerializeField]
    Player player;
    AudioSource music;

    // Start is called before the first frame update
    void Start()
    {
        music = FindObjectOfType<AudioSource>();
    }

    // Update is called once per frame
    void OnTriggerEnter2D()
    {
        StartCoroutine(Cutscene());
    }

    IEnumerator Cutscene()
    {
        GameManager.Instance.paused = true;
        Music.Instance.PlayTrack(0);

        yield return new WaitForSeconds(1f);

        GameManager.Instance.ActivateDialogueBox("Don: Shit! RUN!!!");

        yield return new WaitForSeconds(4);
        GameManager.Instance.DeactivateDialogueBox();

        Vector3 offScreen = player.transform.position;
        offScreen.y += 2;
        GameManager.Instance.paused = false;

        while (player.transform.position != offScreen)
        {
            player.GetComponent<Animator>().SetBool("WalkU", true);
            player.transform.position = Vector3.MoveTowards(player.transform.position, offScreen, 2 * Time.deltaTime);
            yield return null;
        }


        SceneManager.LoadScene("Running");

    }
}
