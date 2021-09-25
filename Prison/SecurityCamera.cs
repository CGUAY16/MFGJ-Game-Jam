using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCamera : MonoBehaviour
{
    public float speed = 3f;
    public float rotationRange;
    [Range(0, 90)]
    [SerializeField]
    float initalRotation;
    [SerializeField]
    //reverses the movement of the camera on its rotation
    bool reverse = false;
    bool negativeEuler = false;


    [SerializeField]
    FieldOfView FOV;

    // Start is called before the first frame update
    void Start()
    {
        //Sets centre of rotation path
        initalRotation = transform.eulerAngles.z;
        if (initalRotation < 120)
        {
            negativeEuler = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.paused)
        {
            float currentAngle = transform.eulerAngles.z;
            if ((negativeEuler && currentAngle > 300))
            {
                currentAngle -= 360;
            }


            if (!(reverse))
            {
                //Rotate the camera anticlockwise 
                transform.Rotate(0, 0, speed * Time.deltaTime);
                if (currentAngle >= initalRotation + rotationRange)
                {
                    reverse = true;
                }
            }
            else
            {
                //Rotate the camera clockwise
                transform.Rotate(0, 0, -speed * Time.deltaTime);
                if (currentAngle <= initalRotation - rotationRange)
                {
                    reverse = false;
                }
            }
        }


        //Sets start of field of view mask and the aim angle of the camera
        FOV.SetOrigin(transform.position );
        FOV.SetAimDirection(-transform.up);
    }
}
