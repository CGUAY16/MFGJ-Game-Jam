using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveScript : MonoBehaviour
{
    [SerializeField]
    Vector3 upPosition;
    [SerializeField]
    Vector3 downPosition;
    [SerializeField]
    string direction;
    [SerializeField]
    Camera mainCamera;
    [SerializeField]
    float movePerFrame;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(MoveCamera());
        }
    }

    IEnumerator MoveCamera()
    {
        if (direction == "up" && mainCamera.transform.position != upPosition)
        {
            GameManager.Instance.paused = true;
            yield return new WaitForSeconds(.5f);

            while (mainCamera.transform.position != upPosition)
            {
                mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, upPosition, movePerFrame * Time.deltaTime);

                yield return null;
            }
            upPosition = mainCamera.transform.position;
            yield return new WaitForSeconds(.5f);
            GameManager.Instance.paused = false;
        }

        if (direction == "down" && mainCamera.transform.position != downPosition)
        {
            GameManager.Instance.paused = true;
            yield return new WaitForSeconds(.5f);

            while (mainCamera.transform.position != downPosition)
            {
                mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, downPosition, movePerFrame * Time.deltaTime);
                //Alternate move code:
                //mainCamera.transform.position -= new Vector3(0, movePerFrame * Time.deltaTime, 0f);
                yield return null;
            }

            yield return new WaitForSeconds(.5f);
            GameManager.Instance.paused = false;
        }
    }
}
