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
        StartCoroutine(Transition());
    }
    public IEnumerator Transition()
    {
        yield return new WaitForSeconds(1f);
        AreaManager.Instance.ChangeScene(Destination, EntranceInt);
    }
}
