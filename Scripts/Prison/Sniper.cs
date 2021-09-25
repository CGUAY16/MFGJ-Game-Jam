using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : MonoBehaviour
{
    //Variables to control the guards movement
    public float lookSpeed = 1f;
    public float waitTime = .3f;
    public bool moving = true;

    //Values for the path of the guard
    public Transform viewHolder;
    Vector3[] aimpoints;
    Vector3 targetAimpoint;
    Vector3 aimDirection;

    //The script holding the field of view code
    [SerializeField]
    FieldOfView FOV;

    // Start is called before the first frame update
    void Start()
    {
        //Build the waypoints for the sniper
        aimpoints = new Vector3[viewHolder.childCount];
        for (int i = 0; i < aimpoints.Length; i++)
        {
            aimpoints[i] = viewHolder.GetChild(i).position;
        }

        targetAimpoint = aimpoints[0];
        StartCoroutine(TargetingLoop());

    }


    IEnumerator TargetingLoop()
    {
        //Sets the sniper aim at its inital position
        targetAimpoint = aimpoints[0];

        //Moves the position in the waypoint list
        int targetWaypointIndex = 1;
        Vector3 nextAimpoint = aimpoints[targetWaypointIndex];

        //To be replaced with a pause movement bool in the Game Manager script
        while (true)
        {
            if(!GameManager.Instance.paused && moving)
            {
                //Moves the snipers aim
                targetAimpoint = Vector3.MoveTowards(targetAimpoint, nextAimpoint, lookSpeed * Time.deltaTime);

                if (nextAimpoint == targetAimpoint)
                {
                    //Changes the waypoint when reached
                    targetWaypointIndex = (targetWaypointIndex + 1) % aimpoints.Length;


                    yield return new WaitForSeconds(waitTime);

                    //Placed after the wait to pause the field of view shifting
                    nextAimpoint = aimpoints[targetWaypointIndex];
                }

            }
                yield return null;
        }


    }

        // Update is called once per frame
        void Update()
    {
        //Positions the field of view based on the snipers position
        FOV.SetOrigin(transform.position);

        aimDirection = (targetAimpoint - transform.position);
        //Not sure why this is needed. It keeps ofsetting the rotation by 90 degrees otherwise
        //aimDirection.x *= -1;


        FOV.SetAimDirection(aimDirection);
    }

    //This draws a visual line of the waypoints of a guard in the editor
    private void OnDrawGizmos()
    {
        Vector3 startPosition = viewHolder.GetChild(0).position;
        Vector3 previousPosition = startPosition;

        foreach (Transform waypoint in viewHolder)
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
}
