using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionCutscene : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject don;
    [SerializeField]
    GameObject[] guards;
    [SerializeField]
    GameObject lastGuard;
    [SerializeField]
    float goal;
    [SerializeField]
    float speed;

    [SerializeField]
    GameObject finishText;

    // Start is called before the first frame update
    void Start()
    {
        player.GetComponent<Animator>().SetBool("WalkR", true);
        don.GetComponent<Animator>().SetBool("WalkR", true);
        foreach (var item in guards)
        {
            item.GetComponent<Animator>().SetBool("WalkR", true);
        }
        StartCoroutine(Cutscene());
    }

    IEnumerator Cutscene()
    {
        Vector3 finish = player.transform.position;
        finish.x += goal;
        while (finish != player.transform.position)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, finish, speed * Time.deltaTime);
            don.transform.position = Vector3.MoveTowards(don.transform.position, player.transform.position, speed * Time.deltaTime);
            foreach (var item in guards)
            {
                item.transform.position = Vector3.MoveTowards(item.transform.position, player.transform.position, speed * Time.deltaTime);
            }
            yield return null;
        }
        Debug.Log("Trigger Escape Scene");
        Music.Instance.player.Stop();

        //Destroy(GameManager.Instance.gameObject);
        SceneManager.LoadScene("EscapeTheCops");

        

        //finishText.SetActive(true);

        //while (true)
        //{
        //    lastGuard.transform.position = Vector3.MoveTowards(lastGuard.transform.position, player.transform.position, speed * Time.deltaTime);
        //
        //    if (Input.GetKeyDown(KeyCode.Space))
        //    {
        //        SceneManager.LoadScene("Main Menu");
        //
        //    }
        //    yield return null;
        //}

    }
}
