using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    private float horizontal;
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpingPower = 16f;
    [SerializeField] private float deacceleration = 0.5f;
    [SerializeField] private float fallSpeed = 0.5f;

    private bool isJumping;

    [SerializeField] private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    [SerializeField] private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;


     private readonly float groundRadius = 0.2f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Z))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f && !isJumping)
        {
            rb.linearVelocityY += jumpingPower;

            jumpBufferCounter = 0f;

            StartCoroutine(JumpCooldown());
        }


        if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Z))
        {
            if (rb.linearVelocity.y > 0f)
            {
                rb.linearVelocityY = rb.linearVelocity.y * fallSpeed;

                coyoteTimeCounter = 0f;
            }
        }
    }

    private void FixedUpdate()
    {
        if ( !(Mathf.Abs(rb.linearVelocityX) > Mathf.Abs(horizontal * speed)))
        {
            rb.linearVelocityX = horizontal * speed;
        }
        if (horizontal * speed == 0)
        {
            rb.linearVelocityX *= deacceleration;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
    }

    private IEnumerator JumpCooldown()
    {
        isJumping = true;
        yield return new WaitForSeconds(0.4f);
        isJumping = false;
    }

    public void AddForce(Vector3 knockbackOrigin , float forcespeed)
    {
        Vector3 direction = (transform.position - knockbackOrigin).normalized;
        rb.AddForce((direction) * forcespeed);
    }
}
