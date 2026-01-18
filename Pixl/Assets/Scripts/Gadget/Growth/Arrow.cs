using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (rb.linearVelocity.magnitude > 1f)
        {
            transform.right = -rb.linearVelocity;
        }
    }

    private void OnCollisionEnter2D(Collision2D context)
    {
        Destroy(this.gameObject);
    }
}