using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    public float knockbackSpeed;
    public bool canTriggerWhilePlayerIsInvincible = true;
    public UnityEvent TriggerEnter;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (canTriggerWhilePlayerIsInvincible || PlayerManager.Instance.health.isInvincible != true)
            {
                Vector3 knockbackDirection = transform.position;
                collision.GetComponent<Movement>().AddForce(knockbackDirection, knockbackSpeed);
                TriggerEnter.Invoke();
            }
        }
    }

    public void ChangePlayerHealth(float AddedHealth)
    {
        PlayerManager.Instance.health.AddHealth(AddedHealth);
        PlayerManager.Instance.UI.UpdateHealth();
    }
    public void ChangePlayerGears(int AddedGears)
    {
        PlayerManager.Instance.health.AddGears(AddedGears);
        PlayerManager.Instance.UI.UpdateGears();
    }
    public void StartTimeStop(float length)
    {
        StartCoroutine(TimeStop(length));
    }
    public IEnumerator TimeStop(float length)
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(length);
        Time.timeScale = 1f;
    }
    public void SetPlayerCheckpoint(GameObject CheckpointPosition)
    {
        PlayerManager.Instance.SetCheckpoint(CheckpointPosition.transform.position);
    }
    public void SendToCheckpoint()
    {
        PlayerManager.Instance.ResetToCheckpoint();
    }

    public void MovementActive(bool value)
    {
        PlayerManager.Instance.movement.enabled = value;
    }
}
