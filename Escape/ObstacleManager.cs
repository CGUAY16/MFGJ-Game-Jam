using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    //The objects which spawn for the player to avoid
    public Transform[] obstacles;
    private Queue<Transform> obstacleQueue;

    //The variables to change the generation of the objects
    public int numberOfObjects;
    public Vector3 startPosition;
    [SerializeField]
    float recycleOffset;
    public Transform[] tracks;
    [SerializeField]
    private Vector3 nextPosition;

    //The variables which tweak the difficuly for the player
    public float runningSpeed;
    [SerializeField]
    float maxSpeed;
    [SerializeField]
    float gapSize = 30;
    [SerializeField]
    float startDistance;
    [SerializeField]
    float timeToSpeedUp;
    [SerializeField]
    float rateOfAcceleration;

    // Start is called before the first frame update
    void Start()
    {
        //Sets first item spawn location
        nextPosition = new Vector3(startDistance, tracks[Random.Range(0, tracks.Length)].position.y);

        //Populates the obstacles for the player to avoid
        obstacleQueue = new Queue<Transform>(numberOfObjects);
        for (int i = 0; i < numberOfObjects; i++)
        {
            obstacleQueue.Enqueue((Transform) Instantiate(obstacles[Random.Range(0, obstacles.Length)]));
        }
        for (int i = 0; i < numberOfObjects; i++)
        {
            //Spawns all objects off screen
            SetInitalPositions();
        }
        recycleOffset = -1 * ((numberOfObjects * gapSize + gapSize) - startDistance);
    }

    // Update is called once per frame
    void Update()
    {
        //When the object has moved far enough off the left side of the screen
        if (obstacleQueue.Peek().position.x < recycleOffset)
        {
            //Resets the item on the right side off-screen
            Recycle();
        }

        foreach (var item in obstacleQueue)
        {
            //Moves the items to the left to simulate movement
            item.transform.Translate(-runningSpeed * Time.deltaTime, 0f, 0f);
        }
        if (timeToSpeedUp > 0)
        {
            timeToSpeedUp -= .5f * Time.deltaTime;
        }

        if (timeToSpeedUp < 0 && runningSpeed !< maxSpeed)
        {
            runningSpeed += rateOfAcceleration * 0.1f * Time.deltaTime;
        }
    }


    private void SetInitalPositions()
    {
        //Creates local variable for spawning
        Vector3 position = nextPosition;

        //Moves the item out of the queue and moves it to the inital position on the right
        Transform o = obstacleQueue.Dequeue();
        o.position = nextPosition;

        //Places it back in the queue at the end
        obstacleQueue.Enqueue(o);

        //Sets the next item spawn location, at a specific distance from the last item
        nextPosition.x += gapSize;
        nextPosition.y = tracks[Random.Range(0, tracks.Length)].position.y;
    }

    private void Recycle()
    {
        //Moves an item out of the queue, resets its position on the right off-screen
        Transform o = obstacleQueue.Dequeue();
        o.position = new Vector3(CalculateRecycleX(), tracks[Random.Range(0, tracks.Length)].position.y);

        //Places it back in the queue
        obstacleQueue.Enqueue(o);


    }

    private float CalculateRecycleX()
    {
        float xValue = (gapSize * numberOfObjects) + recycleOffset;
        return xValue;
    }
}

