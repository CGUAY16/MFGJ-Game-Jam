using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSettingsE5 : MonoBehaviour
{
    [SerializeField]
    Player player;
    [SerializeField]
    Don don;
    [SerializeField]
    Transform[] cellDoor;

    bool go = false;


    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance.checkpointIndex < 5)
        {
            StartCoroutine(DonInstructions());
        }

        else if (GameManager.Instance.checkpointIndex >= 5)
        {
            foreach (var item in cellDoor)
            {
                item.gameObject.SetActive(!item.gameObject.activeSelf);
            }

            if (GameManager.Instance.checkpointIndex >= 6)
            {
                Destroy(don.gameObject);
            }

            else
            {
                StartCoroutine(DonConversation());
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x > -1.6f)
        {
            go = true;
        }
    }

    IEnumerator DonConversation()
    {

        while (!go)
        {
            yield return new WaitForSeconds(0.2f);

            yield return null;
        }

        GameManager.Instance.paused = true;

        yield return new WaitForSeconds(0.5f);

        GameManager.Instance.ActivateDialogueBox("Don: Freedom!!");

        yield return new WaitForSeconds(2);
        GameManager.Instance.DeactivateDialogueBox();
        yield return new WaitForSeconds(0.5f);

        GameManager.Instance.ActivateDialogueBox("Damian: Let's get out of here");

        yield return new WaitForSeconds(4);
        GameManager.Instance.DeactivateDialogueBox();
        yield return new WaitForSeconds(0.5f);
        don.following = true;
        don.cutscene = true;
        GameManager.Instance.checkpointIndex = 6;

        yield return new WaitForSeconds(1f);

        GameManager.Instance.paused = false;
        don.cutscene = false;


    }

    IEnumerator DonInstructions()
    {
        while (!go)
        {
            yield return new WaitForSeconds(0.2f);

            yield return null;
        }


        GameManager.Instance.paused = true;

        yield return new WaitForSeconds(0.5f);

        GameManager.Instance.ActivateDialogueBox("Don: Well look who finally showed up...");

        yield return new WaitForSeconds(4);
        GameManager.Instance.DeactivateDialogueBox();
        yield return new WaitForSeconds(0.5f);

        GameManager.Instance.ActivateDialogueBox("Damian: I can leave you in there if you like");

        yield return new WaitForSeconds(4);
        GameManager.Instance.DeactivateDialogueBox();
        yield return new WaitForSeconds(0.5f);

        GameManager.Instance.ActivateDialogueBox("Don: Just get this cell open for me");

        yield return new WaitForSeconds(4);
        GameManager.Instance.DeactivateDialogueBox();
        yield return new WaitForSeconds(0.5f);

        GameManager.Instance.ActivateDialogueBox("Don: You can open it from the security office");

        yield return new WaitForSeconds(4);
        GameManager.Instance.DeactivateDialogueBox();
        
        yield return new WaitForSeconds(0.5f);

        GameManager.Instance.ActivateDialogueBox("Don: It's on the other side of the prison");

        yield return new WaitForSeconds(4);
        GameManager.Instance.DeactivateDialogueBox();
        yield return new WaitForSeconds(0.5f);

        GameManager.Instance.ActivateDialogueBox("Damian: Be back soon!");

        yield return new WaitForSeconds(4);
        GameManager.Instance.DeactivateDialogueBox();

        GameManager.Instance.paused = false;
    }

}
