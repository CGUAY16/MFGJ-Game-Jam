using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetScrolling : MonoBehaviour
{
    //The objects which spawn for the player to avoid
    [SerializeField] Transform[] images;
    private Queue<Transform> imageQueue;

    //The variables to change the generation of the objects
    [SerializeField] int numberOfImages; 
    [SerializeField] float recycleOffset;
    private Vector3 nextPosition;

    //The variables which tweak the difficuly for the player
    [SerializeField] float runSpeed;
    [SerializeField] float parallaxOffset;

    [SerializeField] Vector3 resetPosition;


    // Start is called before the first frame update
    void Start()
    {
        //Populates the obstacles for the player to avoid
        imageQueue = new Queue<Transform>(numberOfImages);
        for (int i = 0; i < numberOfImages; i++)
        {
            imageQueue.Enqueue(images[i]);
        }
        }
    

    // Update is called once per frame
    void Update()
    {
        float runningSpeed = runSpeed - parallaxOffset;
        //When the object has moved far enough off the left side of the screen
        if (imageQueue.Peek().position.x < recycleOffset)
        {
            //Resets the item on the right side off-screen
            Recycle();
        }

        foreach (var item in imageQueue)
        {
            //Moves the items to the left to simulate movement
            item.transform.Translate(-runningSpeed * Time.deltaTime, 0f, 0f);
        }
    }


    private void Recycle()
    {
        //Moves an item out of the queue, resets its position on the right off-screen
        Transform o = imageQueue.Dequeue();
        o.position = resetPosition;

        //Places it back in the queue
        imageQueue.Enqueue(o);


    }
}

