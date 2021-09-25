using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellDoor : MonoBehaviour
{
    [SerializeField]
    float timeToOpen;
    [SerializeField]
    Player player;
    static bool upTrigger = false, downTrigger = false, leftTrigger = false, rightTrigger = false;

    [SerializeField]
    Transform cellDoor;
    [SerializeField]
    GameObject background;

    [SerializeField]
    bool go = false;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance.checkpointIndex > 0)
        {
            cellDoor.gameObject.SetActive(true);
            Destroy(background);
            Destroy(this.gameObject);
        }
        else
        {
            StartCoroutine(FadeIn());
            StartCoroutine(OpenDoorCountdown());
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (player.horizontal > 0)
        {
            rightTrigger = true;
        }

        if (player.horizontal < 0)
        {
            leftTrigger = true;
        }

        if (player.vertical > 0)
        {
            upTrigger = true;
        }

        if (player.horizontal < 0)
        {
            downTrigger = true;
        }

        if (upTrigger && downTrigger && leftTrigger && rightTrigger)
        {
            go = true;
        }

    }

    IEnumerator OpenDoorCountdown()
    {

        while (!go)
        {
            yield return new WaitForSeconds(1f);

            yield return null;
        }

        GameManager.Instance.checkpointIndex = 1;

        yield return new WaitForSeconds(timeToOpen);

        cellDoor.gameObject.SetActive(true);

        GameManager.Instance.paused = true;

        yield return new WaitForSeconds(1);

        GameManager.Instance.ActivateDialogueBox("Damian: This is it.  It's time!");

        yield return new WaitForSeconds(4);

        GameManager.Instance.DeactivateDialogueBox();
        GameManager.Instance.paused = false;

        Music.Instance.PlayTrack(1);

        Destroy(this.gameObject);

    }

    IEnumerator FadeIn()
    {
        var backgroundColor = background.GetComponent<SpriteRenderer>().color;

        while (backgroundColor.a > 0)
        {
            backgroundColor.a -= Time.deltaTime / 3;
            background.GetComponent<SpriteRenderer>().color = backgroundColor;
            yield return null;
        }

        Destroy(background);

    }
}
