using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    private Rigidbody2D rb;

    //Player
    float walkSpeed = 4f;
    float speedLimiter = 0.7f;
    float inputHorizontal;
    float inputVertical;

    //Animation Changer / States
    Animator animator;
    string currentState;
    const string PLAYER_IDLE = "idel";
    const string PLAYER_RIGHT = "walk-right";
    const string PLAYER_LEFT = "walk-left";
    const string PLAYER_UP = "walk-front";
    const string PLAYER_DOWN = "walk-back";

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
       if(inputHorizontal != 0 || inputVertical != 0)
        {
            if(inputHorizontal != 0 && inputVertical != 0)
            {
                inputHorizontal *= speedLimiter;
                inputVertical *= speedLimiter;
            }
            rb.velocity = new Vector2(inputHorizontal * walkSpeed, inputVertical * walkSpeed);

            //Right
            if (inputHorizontal > 0)
            {
                ChangeAnimationState(PLAYER_RIGHT);
            }
            //Left
            else if (inputHorizontal < 0)
            {
                ChangeAnimationState(PLAYER_LEFT);
            }
            //Play up animation if not going right or left
            else if (inputVertical < 0)
            {
                ChangeAnimationState(PLAYER_UP);
            }
            //Same for down
            else if (inputVertical > 0)
            {
                ChangeAnimationState(PLAYER_DOWN);
            }

        }
       else
        {
            rb.velocity = new Vector2(0f, 0f);
            ChangeAnimationState(PLAYER_IDLE);
        }


    }

    void ChangeAnimationState(string newState)
    {
        //stop animatinon from interupting itself
        if(currentState == newState) return;

        //Play new animation
        animator.Play(newState);

        //update currentState
        currentState = newState;

    }
  
}
