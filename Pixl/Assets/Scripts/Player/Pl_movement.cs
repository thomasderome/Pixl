using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pl_movement : MonoBehaviour
{
    private Vector2 moveInput;
    private FixedJoint2D joint;
    private bool glue;
    
    public float speed;
    public float jump_power;
    public Rigidbody2D rb;
    public SpriteRenderer spr;
    public LayerMask ground_layer;
    public Transform ground_check;
    
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
    
    void OnJump(InputValue value)
    {
        if (value.isPressed && IsGrounded())
        {
            if (glue)
            {
                glue = false;   
                transform.up = new Vector3(0, 1, 0);
                rb.gravityScale = 1.5f;
            }
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jump_power);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (glue) return;
        
        Debug.Log("Enter");
        rb.gravityScale = 0;
        
        transform.up = collision.contacts[0].normal;
        rb.AddForce(collision.contacts[0].normal * -20);
        
        glue = true;
    }   

    // Update is called once per frame
    void Update()
    {
        if (moveInput.x > 0) spr.flipX = false;
        else if (moveInput.x < 0) spr.flipX = true;
    }

    private void FixedUpdate()
    {
        Vector2 targetWalkVelocity = transform.right * moveInput.x * speed;
        float currentVerticalSpeed = Vector2.Dot(rb.linearVelocity, transform.up);
        Vector2 preservedVerticalVelocity = transform.up * currentVerticalSpeed;

        rb.linearVelocity = targetWalkVelocity + preservedVerticalVelocity;

        if (!glue) return;
    
        RaycastHit2D ray = Physics2D.Raycast(transform.position, -transform.up, 5f, ground_layer);
        if(ray.collider != null) 
        {
            transform.up = Vector3.Slerp(transform.up, ray.normal, 10 * Time.fixedDeltaTime);
            rb.AddForce(ray.normal * -50); 
        }
    }  
    
    bool IsGrounded()
    {
        return Physics2D.OverlapCapsule(ground_check.position, new Vector2(1f, 0.1f), CapsuleDirection2D.Horizontal, 0, ground_layer);
    }
}