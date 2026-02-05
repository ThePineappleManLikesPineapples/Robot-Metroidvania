using UnityEngine;

public class SpriteRotation : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rb;
    public float groundRotation = 1f;
    public float airRotation = 0.5f;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (PlayerManager.Instance.movement.IsGrounded())
        {
            transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z - (rb.linearVelocityX * 2 * groundRotation));
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z - (rb.linearVelocityX * 2 * airRotation));
        }
    }
    public void ResetRotation()
    {
        transform.eulerAngles = new Vector3(0, 0, 0);
    }
}
