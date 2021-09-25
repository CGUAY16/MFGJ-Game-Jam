using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingGuardBehavior : MonoBehaviour
{

    [SerializeField] Transform guardTrans;
    Vector3 newPosition;
    [SerializeField]
    GameStateManager winLoseController;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x >= PlayerController.Instance.transform.position.x - 1f)
        {
            Debug.Log("Caught");
            StartCoroutine(winLoseController.Caught());
        }
    }

    // Guard Moves Forward
    public void GuardAdvances()
    { 
        // if guard passes player, trigger game over
        newPosition = this.transform.position + new Vector3(2, 0, 0);
        StartCoroutine(Moving());
    }

    IEnumerator Moving()
    {
        while (transform.position != newPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, newPosition, 2 * Time.deltaTime);
            yield return null;
        }
        yield return null;
    }
}
