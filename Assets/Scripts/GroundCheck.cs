using UnityEngine;
using UnityEngine.Events;

public class GroundCheck : MonoBehaviour
{
    private readonly float groundDistance = 0.3f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    public UnityEvent WhenGrounded;
    public UnityEvent WhenInAir;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (groundCheck == null)
        {
            groundCheck = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGrounded())
        {
            WhenGrounded.Invoke();
        }
        else
        {
            WhenInAir.Invoke();
        }
    }
    private bool IsGrounded()
    {
        return Physics2D.Raycast(groundCheck.position, Vector2.down, groundDistance,groundLayer);
    }
}
