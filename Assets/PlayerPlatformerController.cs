﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformerController : PhysicsObject
{

    public float jumpTakeOffSpeed = 7f;
    public float maxSpeed = 7f;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");

        if(Input.GetButtonDown("Jump") && isGrounded) 
        {
            velocity.y = jumpTakeOffSpeed;
        }
        else if(Input.GetButtonUp("Jump"))
        {
            if(velocity.y > 0)
                velocity.y = velocity.y * 0.5f;
        }

        bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < 0.01f));

        if(flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        animator.SetBool("grounded", isGrounded);
        animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

        targetVelocity = move * maxSpeed;
    }
}
