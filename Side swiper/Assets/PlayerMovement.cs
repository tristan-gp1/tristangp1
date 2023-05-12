using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movespeed;
    public float jumpForce;
    private Rigidbody2D rb;
    private bool facingRight = true;
    private float moveDirection;
    private bool isjumping = false;
    // Awake is called after all objects are initilized. Called in random order
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); // Will look for a component on this game object (what the script is attached to) of type of rigidbody2D.
    }

   
    // Update is called once per frame
    void Update()
    {
        // Get Inputs 
        ProcessInputs();

        //Animate
        Animate();
    }

    private void Animate()
    {
        if (moveDirection > 0 && !facingRight)
        {
            FlipCharacter();
        }
        else if (moveDirection < 0 && facingRight)
        {
            FlipCharacter();
        } 
    }
    //Better for manulling Physics, can be called multiple times per update frame.  
    private void FixedUpdate()
    {
        //Move
        Move();
    }
    private void Move()
    {
        rb.velocity = new Vector2(moveDirection * movespeed, rb.velocity.y);
        if(isjumping)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
        }
        isjumping = false;
    }

    private void ProcessInputs()
    {
        moveDirection = Input.GetAxis("Horizontal"); //Scale pf -1 -> 1.
        if(Input.GetButtonDown("Jump"))
        {
            isjumping = true;
        }
    }

    private void FlipCharacter()
    {
        facingRight = !facingRight; //Inverse bool
        transform.Rotate(0f, 180f, 0f);
    }
}
