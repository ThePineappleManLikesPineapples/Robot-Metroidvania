using Newtonsoft.Json.Linq;
using System.Collections;
using UnityEngine;

public class AreaExit : MonoBehaviour
{
    public string Destination;
    public int EntranceInt;
    public Animator Animator;

    public void ExitArea()
    {
        Animator.SetTrigger("Exit");
        PlayerManager.Instance.movement.enabled = false;
        StartCoroutine(Transition());
    }
    public IEnumerator Transition()
    {
        yield return new WaitForSeconds(1f);
        AreaManager.Instance.ChangeScene(Destination, EntranceInt);
        PlayerManager.Instance.movement.enabled = true;
    }
}
