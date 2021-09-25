using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeControls : MonoBehaviour
{
    /// <summary>
    /// Controls player control for final chase sequence.
    /// </summary>
    Rigidbody2D body;

    bool up;
    bool down;
    /// TBC if using
    //bool jump;

    //Contains track locations and player location
    [SerializeField]
    Transform[] playerPositions;
    [SerializeField]
    int currentPosition = 1;

    //Bool to record if player is shifting tracks
    bool moving;

    //Movement speed of objects (player is stationary, despite running animation)
    public float moveSpeed = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        //Sets player on middle track
        transform.position = playerPositions[1].position;
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            //moves player between tracks
            transform.position = Vector3.MoveTowards(transform.position, playerPositions[currentPosition].position, moveSpeed * Time.deltaTime);
            if (transform.position == playerPositions[currentPosition].position)
            {
                //Stops further imputs
                moving = false;
            }
        }

        else
        {
            //Allows player input
            up = Input.GetKeyDown(KeyCode.UpArrow);
            down = Input.GetKeyDown(KeyCode.DownArrow);
            //jump = Input.GetKeyDown(KeyCode.Space);

            //Move up one track, if available
            if (up && currentPosition != playerPositions.Length - 1)
            {
                currentPosition += 1;
                //Stops further inputs
                moving = true;
            }

            //Move down one track, if available
            if (down && currentPosition != 0)
            {
                currentPosition -= 1;
                //Stops further inputs
                moving = true;
            }
        }



    }

}
