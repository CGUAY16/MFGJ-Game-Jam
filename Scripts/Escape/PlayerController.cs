using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // References
    [SerializeField] Transform backTrackBegin;
    [SerializeField] Transform frontTrackBegin;
    [SerializeField] Transform backTrackLimit;
    [SerializeField] Transform frontTrackLimit;
    public static PlayerController Instance;

    // Variable
    [SerializeField] int[] diff; //difference Between Both Tracks
    private Vector3 currentLoc;
    public float playerCurrentX;
    private float playerCurrentY;
    [SerializeField] float speed;
    private trackID currentID;
    [SerializeField]
    Vector3 movePosition;
    [SerializeField]
    ChasingGuardBehavior guard;

    // Sounds
    [SerializeField] AudioSource audio;

    // Raycast setup for detecting player pos
    Vector3 originR;
    Vector3 originL;
    Vector3 direction;
    float raycastLength;
    Rigidbody pRigidbody;

    enum trackID
    {
        back,
        front
    }

    

    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;

        pRigidbody = GetComponent<Rigidbody>();


        gameObject.transform.position = backTrackBegin.position; // set player initial spot
        currentLoc = gameObject.transform.position;
        movePosition = currentLoc;
        playerCurrentX = gameObject.transform.position.x;
        playerCurrentY = gameObject.transform.position.y;

        // Raycast var setup
        direction = new Vector3(0, -180, 0);
        raycastLength = 10f;

    }

    // Update is called once per frame
    void Update() 
    {
        playerCurrentX = gameObject.transform.position.x;
        originR = this.transform.position + new Vector3(2, 5, 0); // calculates raycast position
        originL = this.transform.position + new Vector3(-2, 5, 0); // calculates raycast position

        Debug.DrawRay(originR, direction, Color.red);
        Debug.DrawRay(originL, direction, Color.green); 

        // Side to side Movement
        if ((Input.GetAxisRaw("Horizontal") > 0 && Physics.Raycast(originR, direction, raycastLength)))
        {
            gameObject.transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime, 0, 0));
        }

        if (Input.GetAxisRaw("Horizontal") < 0 && Physics.Raycast(originL, direction, raycastLength))
        {
            gameObject.transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime, 0, 0));
        }

        // Change lane
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow)) && transform.position.y == movePosition.y && Input.GetAxisRaw("Horizontal") ==0)
        {
            currentLoc = gameObject.transform.position;
            changeTrack();
        }
    }

    public void changeTrack()
    {
        switch (currentID)
        {
            case trackID.back:
                {
                    // add 4 to x axis
                    //gameObject.transform.position = new Vector3(currentLoc.x + diff[0], currentLoc.y + diff[1], currentLoc.z + diff[2]);
                    movePosition = new Vector3(currentLoc.x + diff[0], currentLoc.y + diff[1], currentLoc.z + diff[2]);
                    StartCoroutine(MovePlayer(new Vector3(currentLoc.x + diff[0], currentLoc.y + diff[1], currentLoc.z + diff[2])));
                    currentID = trackID.front;
                    break;
                }
            case trackID.front:
                {
                    // add -4 to x axis
                    //gameObject.transform.position = new Vector3(currentLoc.x - diff[0], currentLoc.y - diff[1], currentLoc.z - diff[2]);
                    movePosition = new Vector3(currentLoc.x - diff[0], currentLoc.y - diff[1], currentLoc.z - diff[2]);
                    StartCoroutine(MovePlayer(new Vector3(currentLoc.x - diff[0], currentLoc.y - diff[1], currentLoc.z - diff[2])));
                    currentID = trackID.back;
                    break;
                }
            default:
                {
                    Debug.Log("Invalid Enum Value");
                    break;
                }
        }
    }

    IEnumerator MovePlayer(Vector3 newPosition)
    {
        while (transform.position.x != newPosition.x)
        {
            transform.position = Vector3.MoveTowards(transform.position, newPosition, 20 * Time.deltaTime);
            yield return null;
        }
        pRigidbody.velocity = Vector3.zero;
        pRigidbody.angularVelocity = Vector3.zero;
        gameObject.transform.position = newPosition;
        yield return null;
    }

    
    // When Player gets hit by obstacle
    private void OnTriggerEnter(Collider col)
    {
        // play a sfx
        audio.Play();
        // get chasingGuard class to trigger guard advancement
        guard.GuardAdvances();
    }
}
