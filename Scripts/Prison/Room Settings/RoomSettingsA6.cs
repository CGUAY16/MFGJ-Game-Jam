using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSettingsA6 : MonoBehaviour
{
    [SerializeField]
    Player player;
    [SerializeField]
    Guard rightGuard;
    [SerializeField]
    Transform rightGuardFOV;
    [SerializeField]
    Guard leftguard;

    bool go = false;


    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance.checkpointIndex > 3)
        {
            rightGuard.gameObject.SetActive(false);
            rightGuardFOV.gameObject.SetActive(false);
        }
        else
        {
            leftguard.moving = false;
            rightGuard.moving = false;
            StartCoroutine(GuardConversation());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y > 0f)
        {
            go = true;
        }
    }

    IEnumerator GuardConversation()
    {

        while (!go)
        {
            yield return new WaitForSeconds(0.2f);

            yield return null;
        }

        GameManager.Instance.checkpointIndex = 4;

        GameManager.Instance.paused = true;

        yield return new WaitForSeconds(0.5f);

        GameManager.Instance.ActivateDialogueBox("Guard 1: Did you hear we have a prisoner on the loose?");

        yield return new WaitForSeconds(4);
        GameManager.Instance.DeactivateDialogueBox();
        yield return new WaitForSeconds(0.5f);

        GameManager.Instance.ActivateDialogueBox("Guard 2: Sure, sure. Of course we do...");

        yield return new WaitForSeconds(4);
        GameManager.Instance.DeactivateDialogueBox();
        yield return new WaitForSeconds(0.5f);

        GameManager.Instance.ActivateDialogueBox("Guard 1: No, seriously!       Missing from Cell Block D");

        yield return new WaitForSeconds(4);
        GameManager.Instance.DeactivateDialogueBox();
        yield return new WaitForSeconds(0.5f);

        GameManager.Instance.ActivateDialogueBox("Guard 1: Shit! I better go help patrol then!!");

        yield return new WaitForSeconds(4);
        GameManager.Instance.DeactivateDialogueBox();
        yield return new WaitForSeconds(0.5f);

        GameManager.Instance.DeactivateDialogueBox();
        GameManager.Instance.paused = false;

        leftguard.moving = true;
        rightGuard.moving = true;

    }

}
