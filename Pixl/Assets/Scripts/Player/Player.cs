using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class Player : MonoBehaviour
{
    public int WIN = 0;
    public bool die = false;
    public GameManager GameManager;
    
    public Vector2 moveInput;
    private FixedJoint2D joint;
    public bool glue;

    public bool stop_animation = false;
    
    public float speed; 
    public float jump_power;
    public Rigidbody2D rb;
    public SpriteRenderer spr;
    public LayerMask ground_layer;
    public Transform ground_check;
    
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && IsGrounded() && !die)
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
        rb.gravityScale = 0;
        
        transform.up = collision.contacts[0].normal;
        rb.AddForce(collision.contacts[0].normal * -50);
        
        glue = true;
    }   

    // Update is called once per frame
    void Update()
    {
        if (moveInput.x > 0 && !stop_animation) spr.flipX = false;
        else if (moveInput.x < 0 && !stop_animation) spr.flipX = true;
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

    private void OnTriggerEnter2D(Collider2D zone)
    {
        GameManager.Death(this.gameObject);
    }
    
    public void Die()
    {
        GetComponent<Gadget_Manager>().Set_gadget(true);
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        rb.gravityScale = 0;
        spr.enabled = false;
        die = true;
        glue = false;
    }

    public void Revive()
    {
        rb.gravityScale = 1.5f;
        GetComponent<Gadget_Manager>().Set_gadget(false);
        spr.enabled = true;
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        die = false;
    }
    
    bool IsGrounded()
    {
        return Physics2D.OverlapCapsule(ground_check.position, new Vector2(1f, 0.1f), CapsuleDirection2D.Horizontal, 0, ground_layer);
    }
}