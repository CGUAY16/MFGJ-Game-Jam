using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneA7 : MonoBehaviour
{
    bool gateOpened;
    [SerializeField]
    Sprite activatedComputer;
    [SerializeField]
    AudioSource soundEffect;

    // Start is called before the first frame update
    void Start()
    {
       gateOpened = GameSettings.Instance.cellOpenedA7;

        if (gateOpened)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = activatedComputer;
        }
    }


    public void StartCutscene()
    {
        GameSettings.Instance.cellOpenedA7 = true;
        StartCoroutine(Cutscene());

    }

    IEnumerator Cutscene()
    {
        yield return new WaitForSeconds(.1f);

        GameManager.Instance.paused = true;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = activatedComputer;
        soundEffect.Play();

        yield return new WaitForSeconds(1f);

        GameManager.Instance.SetDialogueBoxCentered();
        GameManager.Instance.ActivateDialogueBox("Cell door unlocked");

        yield return new WaitForSeconds(4);
        GameManager.Instance.DeactivateDialogueBox();
        GameManager.Instance.ResetDialogueBoxAlignment();

        yield return new WaitForSeconds(.5f);

        GameManager.Instance.paused = false;
    }
}
