using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float acceleration = 10;
    public float jumpTime = .75f;
    public float jumpHeight = 3;

    float gravity;
    float jumpImpulse;

    Vector3 velocity = new Vector3();
    PawnAABB pawn;

    bool isGrounded = false;

    void Start()
    {
        pawn = GetComponent<PawnAABB>();
        DeriveJumpValues();
    }

    void DeriveJumpValues()
    {
        gravity = (jumpHeight * 2) / (jumpTime * jumpTime);
        jumpImpulse = gravity * jumpTime;
    }

    void Update()
    {
        HandleInput();

        // Do the move:
        PawnAABB.CollisionResults results = pawn.Move(velocity * Time.deltaTime);
        if (results.hitTop || results.hitBottom) velocity.y = 0;
        if (results.hitLeft || results.hitRight) velocity.x = 0;

        isGrounded = results.hitBottom;


        transform.position += results.distance;

    }

    private void HandleInput()
    {
        //Gravity
        velocity.y -= gravity * Time.deltaTime;

        //jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = jumpImpulse;
        }

        //sidways movement
        float axisH = Input.GetAxisRaw("Horizontal");

        if (axisH == 0)
        {
            //slow down the player
            Decelerate(acceleration);
        }
        else
        {
            AccelerateX(axisH * acceleration);
        }
            
        
    }

    private void Decelerate(float amount)
    {
        if (velocity.x > 0) //moving right
        {
            AccelerateX(-amount);
            if (velocity.x < 0) velocity.x = 0;
        }
        if (velocity.x < 0) //moving left
        {
            AccelerateX(amount);
            if (velocity.x > 0) velocity.x = 0;
        }
    }

    private void AccelerateX(float amount)
    {
        velocity.x += amount * Time.deltaTime;
    }
}
