using UnityEngine;

public class BasicEnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed = 8f;


    private readonly float groundRadius = 0.2f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask groundLayer;


    private void FixedUpdate()
    {
        if (CanMoveForward())
        {
            if (transform.eulerAngles.y is 180 or -180)
            {
                rb.linearVelocityX = -speed;
            }
            else
            {
                rb.linearVelocityX = speed;
            }
        }
        else
        {
            rb.linearVelocityX = 0f;
            TurnAround();
        }
    }

    private bool CanMoveForward()
    {
        return (Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer)) && !(Physics2D.OverlapCircle(wallCheck.position, groundRadius, groundLayer));
    }

    private void TurnAround()
    {
        if (transform.eulerAngles.y is 180 or -180)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
    public void KnockbackFromPlayer( float forcespeed)
    {
        Vector3 direction = (transform.position - PlayerManager.Instance.player.position).normalized;
        rb.AddForce((direction) * forcespeed);
    }
}
