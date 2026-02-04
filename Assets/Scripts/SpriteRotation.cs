using UnityEngine;

public class SpriteRotation : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rb;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z - (rb.linearVelocityX*2));
    }
    public void ResetRotation()
    {
        transform.eulerAngles = new Vector3(0, 0, 0);
    }
}
