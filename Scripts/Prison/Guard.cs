using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
    //Variables to control the guards movement
    public float speed = 1f;
    public float waitTime = .3f;
    public bool moving = true;

    //Values for the path of the guard
    public Transform pathHolder;
    Vector3[] waypoints;
    Vector3 targetWaypoint;
    Vector3 aimDirection;

    //The script holding the field of view code
    [SerializeField]
    FieldOfView FOV;

    Animator gAnimator;

    SpriteRenderer spr;
    bool flipped;

    // Start is called before the first frame update
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        gAnimator = GetComponent<Animator>();

        //Build the waypoints for the guard
        if (pathHolder != null)
        {
            waypoints = new Vector3[pathHolder.childCount];
            for (int i = 0; i < waypoints.Length; i++)
            {
                waypoints[i] = pathHolder.GetChild(i).position;
            }
            transform.position = waypoints[0];
            //Starts their movement
            StartCoroutine(FollowPath(waypoints));
        }

    }

    IEnumerator FollowPath(Vector3[] waypoints)
    {
        //Sets the guard at its inital position
        transform.position = waypoints[0];

        //Moves the position in the waypoint list
        int targetWaypointIndex = 1;
        targetWaypoint = waypoints[targetWaypointIndex];

        if (moving)
        {
            bool spawnWait = true;
            if (spawnWait)
            {
                spawnWait = false;
                yield return new WaitForSeconds(1);
            }
        }

        //To be replaced with a pause movement bool in the Game Manager script
        while (true)
        {
            if (!GameManager.Instance.paused && moving)
            {
                //Moves the guard
                transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, speed * Time.deltaTime);

                if (transform.position.y - targetWaypoint.y < 0)
                {
                    gAnimator.SetBool("WalkU", true);
                }
                else
                {
                    gAnimator.SetBool("WalkU", false);
                }

                if (transform.position.y - targetWaypoint.y > 0)
                {
                    gAnimator.SetBool("Walking", true);
                }
                else
                {
                    gAnimator.SetBool("Walking", false);
                }

                if (transform.position.x - targetWaypoint.x < 0)
                {
                    gAnimator.SetBool("WalkR", true);
                }
                else
                {
                    gAnimator.SetBool("WalkR", false);
                }

                if (transform.position.x - targetWaypoint.x > 0)
                {
                    gAnimator.SetBool("WalkL", true);
                }
                else
                {
                    gAnimator.SetBool("WalkL", false);
                }




                if (transform.position == targetWaypoint)
                {
                    //Changes the waypoint when reached
                    gAnimator.SetBool("Walking", false);
                    targetWaypointIndex = (targetWaypointIndex + 1) % waypoints.Length;


                    yield return new WaitForSeconds(waitTime);

                    //Placed after the wait to pause the field of view shifting
                    targetWaypoint = waypoints[targetWaypointIndex];
                }
            }
            else
            {
                gAnimator.SetBool("Walking", false);
            }

            yield return null;
        }

    }

    // Update is called once per frame
    void Update()
    {
        //Positions the field of view based on the guards position
        FOV.SetOrigin(transform.position);

        //If still moving, it will update the direction of the viewpoint
        if (transform.position - targetWaypoint != Vector3.zero)
        {
            aimDirection = (transform.position - targetWaypoint).normalized;
            //Not sure why this is needed. It keeps ofsetting the rotation by 90 degrees otherwise
            aimDirection.x *= -1;
        }

        //Sitting outside the if so the aimDirection doesn't shift when waiting to move again
        FOV.SetGuardAimDirection(aimDirection);

        SetFlipX();


    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Checking it is only the player who can spawn
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(FOV.PlayerSpotted());
        }
    }

        //This draws a visual line of the waypoints of a guard in the editor
        private void OnDrawGizmos()
    {
        Vector3 startPosition = pathHolder.GetChild(0).position;
        Vector3 previousPosition = startPosition;

        foreach (Transform waypoint in pathHolder)
        {
            Gizmos.DrawSphere(waypoint.position, 0.3f);
            Gizmos.DrawLine(previousPosition, waypoint.position);
            previousPosition = waypoint.position;
        }
        Gizmos.DrawLine(previousPosition, startPosition);
    }

    //Function to calculate easily
    public Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    void UpdateGuardSprite()
    {

    }

    void SetFlipX()
    {
        if (flipped && transform.position - targetWaypoint == Vector3.zero)
        {
            spr.flipX = true;
        }
        else
        {
            spr.flipX = (targetWaypoint.x > transform.position.x);
            if (spr.flipX)
            {
                flipped = true;
            }
            else
            {
                flipped = false;
            }
        }
    }

}
