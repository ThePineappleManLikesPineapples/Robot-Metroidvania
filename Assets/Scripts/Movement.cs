using UnityEngine;
using System.Collections;
using JetBrains.Annotations;

public class Movement : MonoBehaviour
{
    private float moveDirection;
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpingPower = 16f;
    [SerializeField] private float fallLerp = 0.8f;

    private bool isJumping;

    [SerializeField] private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    [SerializeField] private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;

    [SerializeField] private float knockbackBufferTime = 0.2f;
    private float knockbackBufferCounter;

    private readonly float groundRadius = 0.2f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private void Update()
    {
        moveDirection = Input.GetAxisRaw("Horizontal");

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

        knockbackBufferCounter -= Time.deltaTime;

        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f && !isJumping)
        {
            rb.linearVelocityY = Mathf.Lerp(rb.linearVelocityY, jumpingPower, 0.5f);

            jumpBufferCounter = 0f;

            StartCoroutine(JumpCooldown());
        }


        if (Input.GetButtonUp("Jump") || Input.GetKeyUp(KeyCode.Z))
        {
            if (rb.linearVelocity.y > 0f)
            {
                rb.linearVelocityY = Mathf.Lerp(rb.linearVelocityY, 0, fallLerp);
                coyoteTimeCounter = 0f;
            }
        }
    }

    private void FixedUpdate()
    {
        if (knockbackBufferCounter < 0f)
        {
            rb.linearVelocityX = Mathf.Lerp(rb.linearVelocityX, moveDirection * speed, 0.5f);
        }
    }

    public bool IsGrounded()
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
        Vector2 TotalForce = ((direction) * forcespeed);
        rb.linearVelocity += TotalForce;
        knockbackBufferCounter = knockbackBufferTime;
    }
}
