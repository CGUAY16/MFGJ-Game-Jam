using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneD6 : MonoBehaviour
{
    [SerializeField]
    Sprite activatedComputer;
    [SerializeField]
    AudioSource soundEffect;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance.checkpointIndex >= 7)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = activatedComputer;
        }
    }


    public void StartCutscene()
    {
        StartCoroutine(Cutscene());

    }

    IEnumerator Cutscene()
    {
        GameManager.Instance.paused = true;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = activatedComputer;
        soundEffect.Play();

        yield return new WaitForSeconds(1f);

        GameManager.Instance.SetDialogueBoxCentered();
        GameManager.Instance.ActivateDialogueBox("Entrance gate unlocked");

        yield return new WaitForSeconds(4);
        GameManager.Instance.DeactivateDialogueBox();
        GameManager.Instance.ResetDialogueBoxAlignment();

        yield return new WaitForSeconds(.5f);

        GameManager.Instance.paused = false;
    }
}
