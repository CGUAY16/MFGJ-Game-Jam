using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D body;
    public Animator pAnimator;

    public float horizontal;
    public float vertical;
    bool interact;
    float moveLimiter = 0.7f;

    public float moveSpeed = 20.0f;

    [SerializeField]
    Don donPrefab;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        pAnimator = GetComponent<Animator>();


    }

    

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.paused)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");

            if (vertical > 0)
            {

                pAnimator.SetBool("WalkU", true);
            }
            else
            {
                pAnimator.SetBool("WalkU", false);
            }

            if (vertical < 0)
            {
                pAnimator.SetBool("WalkD", true);
            }

            else
            {
                pAnimator.SetBool("WalkD", false);
            }

            if (horizontal > 0)
            {
                pAnimator.SetBool("WalkR", true);
            }
            else
            {
                pAnimator.SetBool("WalkR", false);
            }

            if (horizontal < 0)
            {
                pAnimator.SetBool("WalkL", true);
            }
            else
            {
                pAnimator.SetBool("WalkL", false);
            }


            interact = Input.GetKeyDown(KeyCode.Space);
            body.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            body.constraints = RigidbodyConstraints2D.FreezeAll;
            pAnimator.SetBool("WalkU", false);
            pAnimator.SetBool("WalkD", false);
            pAnimator.SetBool("WalkR", false);
            pAnimator.SetBool("WalkL", false);
        }

        if (interact)
        {
            Collider2D[] interactions = Physics2D.OverlapCircleAll(transform.position, .32f);
            foreach (var item in interactions)
            {
                Debug.Log(item.tag);
                if (item.gameObject.GetComponent<CutsceneA7>() != null && !GameSettings.Instance.cellOpenedA7)
                {
                    item.gameObject.SendMessage("StartCutscene");
                }
                if (item.gameObject.GetComponent<CutsceneD6>() != null && GameManager.Instance.checkpointIndex == 6)
                {
                    item.gameObject.SendMessage("StartCutscene");
                }
                if (item.CompareTag("Computer"))
                {
                    item.gameObject.SendMessage("Activate");
                }
                if (item.gameObject.GetComponent<CheckpointMarker>() != null)
                {
                    item.gameObject.SendMessage("UpdateCheckpoint");
                }


            }
        }
    }

    void FixedUpdate()
    {
        //Slows movement on the diagonal to improve the feel
        if (horizontal != 0 && vertical != 0)
        {
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }
        body.velocity = new Vector2(horizontal * moveSpeed, vertical * moveSpeed);
    }

    //Teleports the player to the correct door when loading the level
    public void TeleportTo(Vector2 targetPosition)
    {
        transform.position = targetPosition;
    }
}
