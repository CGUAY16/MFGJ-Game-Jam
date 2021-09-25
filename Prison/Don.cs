using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Don : MonoBehaviour
{
    public bool following = false;
    public Player player;
    [SerializeField]
    float speed = 2;

    Animator dAnimator;

    public bool cutscene;


    // Start is called before the first frame update
    void Start()
    {
        dAnimator = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (following)
        {
            if (player.transform.position != transform.position)
            {
                if (player.pAnimator.GetBool("WalkU"))
                {
                    dAnimator.SetBool("WalkU", true);
                }
                else
                {
                    if ((dAnimator.GetBool("WalkU") && transform.position.y < player.transform.position.y))
                    {
                        dAnimator.SetBool("WalkU", true);
                    }
                    else
                    {
                        dAnimator.SetBool("WalkU", false);
                    }
                }

                if (player.pAnimator.GetBool("WalkD") || cutscene)
                {
                    dAnimator.SetBool("WalkD", true);
                }
                else
                {
                    if ((dAnimator.GetBool("WalkD") && transform.position.y < player.transform.position.y))
                    {
                        dAnimator.SetBool("WalkD", true);
                    }
                    else
                    {
                        dAnimator.SetBool("WalkD", false);
                    }
                }

                if(player.pAnimator.GetBool("WalkR"))
                {
                    dAnimator.SetBool("WalkR", true);
                }
                else
                {
                    if ((dAnimator.GetBool("WalkR") && transform.position.x > player.transform.position.x))
                    {
                        dAnimator.SetBool("WalkR", true);
                    }
                    else
                    {
                        dAnimator.SetBool("WalkR", false);
                    }
                }

                if (player.pAnimator.GetBool("WalkL"))
                {
                    dAnimator.SetBool("WalkL", true);
                }
                else
                {
                    if ((dAnimator.GetBool("WalkL") && transform.position.x < player.transform.position.x))
                    {
                        dAnimator.SetBool("WalkL", true);
                    }
                    else
                    {
                        dAnimator.SetBool("WalkL", false);
                    }
                }

                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
                //trigger animation for move
            }
            else
            {
                dAnimator.SetBool("WalkU", false);
                dAnimator.SetBool("WalkD", false);
                dAnimator.SetBool("WalkL", false);
                dAnimator.SetBool("WalkR", false);
            }
            
        }

    }
}
